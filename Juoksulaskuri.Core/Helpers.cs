using Juoksulaskuri.Core.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Transactions;

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

        public static string MinPerKmToPace(double min)
        {
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
            // Riegel formula 1.06 exponent
            double s = Math.Pow(((predictDistance / result.Distance)), 1.06) * result.Duration.TotalSeconds;

            RacePrediction p = new RacePrediction();
            p.Distance = predictDistance;
            p.Duration = new TimeSpan(0, 0, Convert.ToInt32(s));
            p.Pace = SpeedToPace(p.SpeedMS);
            // Calculate Daniels % of VO2max for the distance
            p.PercentMax = (0.8 + 0.1894393 * Math.Exp(-0.012778 * p.Duration.TotalSeconds / 60) + 0.2989558 * Math.Exp(-0.1932605 * p.Duration.TotalSeconds / 60)) * 100;

            // Calorie expendature
            if (user.Weight > 0)
                p.Energy = user.Weight * predictDistance / 1000.0;

            // Oxygen consumption is 1.31ml/kg/nm
            // Add 5kcal for every liter of oxygen          

            // Calculate HR for the distance with Jack Daniels formula from max HR
            p.HR = Math.Min(user.MaxHeartrate, (0.5 + 0.5 * (p.PercentMax / 100.0 - 0.28) / 0.72) * user.MaxHeartrate);

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
