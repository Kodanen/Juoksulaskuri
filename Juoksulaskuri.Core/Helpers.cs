using Juoksulaskuri.Core.Models;
using System.Collections.Generic;

namespace Juoksulaskuri.Core
{
    public static class Helpers
    {
        /// <summary>
        /// Speed [m/s] to pace
        /// </summary>
        /// <param name="speedMs"></param>
        /// <returns>[mm]:[ss]</returns>
        public static string SpeedToPace(double speedMs)
        {
            // Minutes per kilometer
            var min = 1000.0 / speedMs / 60.0;
            var fullmin = Math.Truncate(min);

            return fullmin.ToString("#0") + ":" + Math.Truncate((min - fullmin) * 60).ToString("00");
        }


        /// <summary>
        /// Calculate race prediction based on DISTANCE user data and another race
        /// </summary>
        /// <param name="user"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static RacePrediction CalculateRacePrediction(UserInfo user, RaceResult result, double predictDistance)
        {
            double s = Math.Pow(((predictDistance / result.Distance)), 1.1) * result.Duration.TotalSeconds;

            RacePrediction p = new RacePrediction();
            p.Distance = predictDistance;
            p.Duration = new TimeSpan(0, 0, Convert.ToInt32(s));
            p.Pace = SpeedToPace(p.SpeedMS);

            // Calorie expendature
            if (user.Weight > 0)
                p.Energy = user.Weight * predictDistance / 1000.0;

            // Oxygen consumption is 1.31ml/kg/nm
            // Add 5kcal for every liter of oxygen

            // Jack Daniels
            p.PercentMax = 0.8 + 0.1894393 * Math.Exp(-0.012778 * result.Duration.TotalSeconds) + 0.2989558 * Math.Exp(-0.1932605 * result.Duration.TotalSeconds) * 100;
            p.VO2 = -4.6 + 0.182258 * (p.Distance / p.Duration.TotalSeconds) + Math.Pow(0.000104 * (p.Distance / p.Duration.TotalSeconds), 2);
            p.Vdot = p.VO2 / p.PercentMax;            

            return p;
        }

        /// <summary>
        /// Calculate predictions over these distances
        /// </summary>
        /// <returns></returns>
        public static List<double> PredictionDistances()
        {
            return new List<double>() { 1000, 3000, 5000, 10000, 21092, 42195, 50000, 100000 };
        }
    }
}
