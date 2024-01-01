using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juoksulaskuri.Core.Models
{
    public static class EnergyExpenditure
    {
        /// <summary>
        /// Calculate factor of energy expenditure when compared to flat running
        /// 0.0% = 1.000
        /// </summary>
        /// <param name="grade">-30% ... +30%</param>
        /// <returns>1.000 ... 5.000</returns>
        public static double GetEnergyExpenditureFactor(double grade, bool percentageAsWholeNumber = true)
        {
            double p = grade;
            if(percentageAsWholeNumber)
            {   
                if(grade > 30 || grade < -30)
                    throw new ArgumentException("Incorrect incline value");

                p = grade / 100.0;
            }
            else
            {
                if (grade > 0.30 || grade < -0.30)
                    throw new ArgumentException("Incorrect incline value");

                p = grade;
            }

            return 15.649 * Math.Pow(p, 2) + 2.99953 * p + 1.0; //1.0209
        }

        /// <summary>
        /// Get factor for Grade Adjusted Pace calculation
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="percentageAsWholeNumber"></param>
        /// <returns></returns>
        public static double GetGAPFactor(double grade, bool percentageAsWholeNumber = true)
        {
            double p = grade;
            if (percentageAsWholeNumber)
            {
                if (grade > 30 || grade < -30)
                    throw new ArgumentException("Incorrect incline value");

                p = grade / 100.0;
            }
            else
            {
                if (grade > 0.30 || grade < -0.30)
                    throw new ArgumentException("Incorrect incline value");

                p = grade;
            }

            return 3968 * Math.Pow(p, 8) + 
                    1264 * Math.Pow(p, 7) + 
                    -935.3 * Math.Pow(p, 6) + 
                    -290.8 * Math.Pow(p, 5) + 
                    21.71 * Math.Pow(p, 4) + 
                    15.62 * Math.Pow(p, 3) + 
                    17.5 * Math.Pow(p, 2) + 
                    2.811 * p + 
                    1.0;
        }
    }
}
