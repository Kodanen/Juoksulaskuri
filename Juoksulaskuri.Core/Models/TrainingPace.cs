using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juoksulaskuri.Core.Models
{
    // Jack Daniels training paces
    public class TrainingPaces
    {
        public string RecoveryPace { get; set; }
        public string EasyPace { get; set; }
        public string MaratonPace { get; set; }
        public string TempoPace { get; set; }
        public string ThresholdPace { get; set; }
        public string IntervalPace { get; set; }
        public string RepetitionPace { get; set; }


        private double vdot;
        public double Vdot 
        {
            get
            {
                return vdot;
            }
            set
            {
                vdot = value;
                UpdatePaces(); 
            }
        }

        private void UpdatePaces()
        {
            // [Min / km]
            double easy = 119.43 * Math.Pow(vdot, -0.797);
            EasyPace = Helpers.MinPerKmToPace(easy);

            double maraton = 121.76 * Math.Pow(vdot, -0.841);
            MaratonPace = Helpers.MinPerKmToPace(maraton);

            // ToDo Korjaa kaava
            double tempo = 121.76 * Math.Pow(vdot, -0.841);
            TempoPace = Helpers.MinPerKmToPace(tempo);

            double threshold = 100.28 * Math.Pow(vdot, -0.808);
            ThresholdPace = Helpers.MinPerKmToPace(threshold);

            double interval = 93.122 * Math.Pow(vdot, -0.81);
            IntervalPace = Helpers.MinPerKmToPace(interval);

            double repetition = 117.86 * Math.Pow(vdot, -0.888);
            RepetitionPace = Helpers.MinPerKmToPace(repetition);
        }
    }


}
