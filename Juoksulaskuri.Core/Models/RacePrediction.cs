using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juoksulaskuri.Core.Models
{
    public class RacePrediction
    {
        public double Distance { get; set; }

        public TimeSpan Duration { get; set; }

        public double SpeedMS { get { return Distance / Duration.TotalSeconds; } } // [m/s]
        public double SpeedKMH { get { return (Distance / Duration.TotalSeconds) * 3.6; } } // [km/h]

        public string Pace { get; set; }

        public double PercentMax { get; set;}
        public double VO2 { get; set; }
        public double Vdot { get; set; }

        public double Energy {  get; set; }
    }
}
