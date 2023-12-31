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
        public int AnAertHeartrate { get; set; }

        public int MaxHeartrate { get; set; }
        

        public double AertSpeed {  get; set; }  
        public double AnAertSpeed { get; set; } 


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
