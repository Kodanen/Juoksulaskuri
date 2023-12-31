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
            get{  return _userInfo; }
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

        public void RemoveRace(RaceResult race) {  races.Remove(race); }
        public bool RemoveRace(int id)
        {
            var r = races.FirstOrDefault(i => i.id == id);
            if(r != null)
            {
                races.Remove(r);
                return true;
            }

            return false;
        }

        public void ClearRaces() { races.Clear();}

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
        }

        public void EditRace(RaceResult race)
        {
            if (race.id < 1)
                return;

            var r = races.FirstOrDefault(i => i.id == race.id);
            if (r != null)
            {
                r.Modified = DateTime.Now;
                r.Distance = race.Distance;
                r.Duration = race.Duration;
            }
        }

        public void SetRaces(List<RaceResult> r)
        {
            races = r ?? new List<RaceResult>();
        }

        public bool IsValidRace()
        {
            if(races.Count == 0) return false;

            return true;
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
