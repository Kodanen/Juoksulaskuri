using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace Juoksulaskuri.Core.Models
{
    public class UserInfo
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nimi tarvitaan")]
        public string Name { get; set; }

        public double Age { get; set; }

        public double Weight { get; set; }

        public double Sex {  get; set; }    

        public int RestingHeartrate { get; set; }
        public int AertHeartrate { get; set; }
        public int LTHeartrate { get; set; }

        public int MaxHeartrate { get; set; }
        
        /// <summary>
        /// Aerobic Threshold speed [m/s]
        /// </summary>
        public double AertSpeed {  get; set; }
        /// <summary>
        /// Lactate Threshold pace [#m:ss]
        /// </summary>
        public string AertPace { get { return Helpers.SpeedToPace(AertSpeed); } }

        /// <summary>
        /// Lactate Threshold speed [m/s]
        /// </summary>
        public double LTSpeed { get; set; }
        /// <summary>
        /// Lactate Threshold pace [#m:ss]
        /// </summary>
        public string LTPace { get { return Helpers.SpeedToPace(LTSpeed); } }

        public double Vdot { get; set; }
        public double VO2max { get; set; }

        public DateTime Modified { get; set; }

        public UserInfo()
        {
            Name = string.Empty;
            Weight = 0;
        }

        public bool IsValid()
        {
            if (Name == null || Name.Length == 0) return false;

            return true;
        }
    }
}
