﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juoksulaskuri.Core.Models
{
    public class Result
    {
        public double Distance { get; set; } // [m]
        public TimeSpan Duration {  get; set; } // Time it took to travel the distance

        #region UI update
        public void Update()
        {
            NotifyStateChanged();
        }

        public event Action? OnChange;

        private void NotifyStateChanged() => OnChange?.Invoke();
        #endregion
    }
}
