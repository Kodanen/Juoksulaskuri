using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace Juoksulaskuri.Core.Models
{
    public class RaceResult
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Matka on pakollinen")]
        public double Distance { get; set; }

        [Required(ErrorMessage = "Aika on pakollinen")]
        public TimeSpan Duration { get; set; }

        public DateTime Modified { get; set; }

        public double SpeedMS { get { return Distance / Duration.TotalSeconds; } } // [m/s]

        public double SpeedKMH { get { return (Distance / Duration.TotalSeconds) * 3.6; } } // [km/h]

        public override string ToString()
        {
            
            return Math.Round(Distance, 0) + "m " + Duration.ToString(@"hh\:mm\:ss");
        }

        public bool IsValid()
        {
            // ToDo Add Speed
            return Distance > 100 && Distance < 200000 && Duration > TimeSpan.FromSeconds(10) && Duration < TimeSpan.FromHours(48);
        }
    }
}
