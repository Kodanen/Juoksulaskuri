using Juoksulaskuri.Core.Models;
using Microsoft.AspNetCore.Components;

namespace WebUI.Shared
{
    public class MemContainer
    {
        #region UserInfo
        private UserInfo _userInfo;
        public UserInfo UserInfo 
        { 
            get{  return _userInfo != null ? _userInfo : new UserInfo(); }
            set {  _userInfo = value; } 
        }
        #endregion

        #region Races
        private List<RaceResult> races = new List<RaceResult>();

        public List<RaceResult> Races { get { return races; } }

        public void AddRace(RaceResult race)
        {
            if (race == null)
                throw new ArgumentNullException("Race is null");

            // Calculate ID
            if (races.Count == 0)
            {
                race.id = 1;
            }else
            {
                race.id = races.Max(r => r.id) + 1;
            }
            
            races.Add(race);
        }

        public void RemoveRace(RaceResult race) {  races.Remove(race); UpdatePerformance(); }
        public bool RemoveRace(int id)
        {
            var r = races.FirstOrDefault(i => i.id == id);
            if(r != null)
            {
                races.Remove(r);
                UpdatePerformance();
                return true;
            }

            UpdatePerformance();
            return false;
        }

        public void ClearRaces() { races.Clear(); UpdatePerformance(); }

        public void EditRace(int id, double Distance, TimeSpan Duration)
        {
            if (id < 1)
                return;

            var r = races.FirstOrDefault(i => i.id == id);
            if (r != null)
            {
                r.Modified = DateTime.Now;
                r.Distance = Distance;
                r.Duration = Duration;
            }

            UpdatePerformance();
        }

        public void EditRace(RaceResult race)
        {
            if (race.id < 1)
                race.id = races.Max(i => i.id) + 1;

            var r = races.FirstOrDefault(i => i.id == race.id);
            if (r != null)
            {
                r.Modified = DateTime.Now;
                r.Distance = race.Distance;
                r.Duration = race.Duration;
            }

            UpdatePerformance();
        }

        public void SetRaces(List<RaceResult> r)
        {
            races = r ?? new List<RaceResult>();

            UpdatePerformance();
        }

        public bool IsValidRace()
        {
            if(races.Count == 0) return false;

            return true;
        }
        #endregion

        #region Performance calculations

        /// <summary>
        /// Update user performance data
        /// </summary>
        public void UpdatePerformance()
        {
            if(UserInfo == null)
                return;

            UserInfo.VO2max = 0;
            UserInfo.Vdot = 0;

            if (IsValidRace())
            {

                // Check all races
                foreach (var race in races)
                {
                    // Jack Daniels
                    var percentMax = 0.8 + 0.1894393 * Math.Exp(-0.012778 * race.Duration.TotalSeconds / 60) + 0.2989558 * Math.Exp(-0.1932605 * race.Duration.TotalSeconds / 60);
                    var VO2 = -4.6 + 0.182258 * (race.Distance / (race.Duration.TotalSeconds / (24 * 60 * 60)) / 1440) + 0.000104 * Math.Pow((race.Distance / (race.Duration.TotalSeconds / (24 * 60 * 60)) / 1440), 2);
                    var vdot = VO2 / percentMax;

                    UserInfo.Vdot = Math.Max(UserInfo.Vdot, vdot);
                }

                // Jack Daniels - Calculate LT Pace 
                var ltPace = 1 / (29.54 + 5.000663 * (UserInfo.Vdot * 0.88) - 0.007546 * Math.Pow(UserInfo.Vdot * 0.88, 2)) * 1609.344 / 1440 * 0.621371192 * (24 * 60 * 60);
                // Convert [seconds / km] to [meters / second]
                UserInfo.LTSpeed = 3600.0 / ltPace / 3.6;

            }

            // Heart rate calculation
            if (UserInfo.MaxHeartrate > 100)
            {
                UserInfo.LTHeartrate = Convert.ToInt16(UserInfo.MaxHeartrate * 0.9);
            }

            int i = 1 + 2;
        }

        #endregion


        #region UI updates
        public async void Update()
        {
            NotifyStateChanged();
        }

        public event Action? OnChange;

        private void NotifyStateChanged()
        {
            if(OnChange != null)
            {
                Dispatcher.CreateDefault().InvokeAsync(() =>
                {
                    //OnChange.Invoke();
                });
            }
            
        }
        #endregion

        // ~ctor
        public MemContainer()
        {
            // Initialize
            _userInfo = new UserInfo();            
            races = new List<RaceResult>();
        }
    }
}
