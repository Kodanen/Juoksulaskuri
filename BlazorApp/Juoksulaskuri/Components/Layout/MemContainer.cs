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
    }
}
