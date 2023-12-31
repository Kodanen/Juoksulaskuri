using Juoksulaskuri.Core;

namespace Juoksulaskuri
{
    public class MemContainer
    {
        #region UI update
        public void Update()
        {
            NotifyStateChanged();
        }

        public event Action? OnChange;

        private void NotifyStateChanged() => OnChange?.Invoke();
        #endregion

        public string Name { get; set; }


        private int? location;
        public int LocationId
        {
            get
            {
                if (location == null)
                    return 0;
                return location.Value;
            }
            set
            {
                location = value;
            }
        }

    }
}
