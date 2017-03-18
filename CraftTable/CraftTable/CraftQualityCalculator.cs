using System;
using CraftTable.Contracts;

namespace CraftTable
{
    public class CraftQualityCalculator : ICraftQualityCalculator
    {
        public int CalculateHighQualityChance(int quality, int maxQuality)
        {
            double qualityPercent = (double)quality / maxQuality*100;
            var hqPercent = 1;

            if (Math.Abs(qualityPercent) < double.Epsilon)
            {
                hqPercent = 1;
            }
            else if (qualityPercent >= 100)
            {
                hqPercent = 100;
            }
            else
            {
                while (A(hqPercent) < qualityPercent && hqPercent < 100)
                {
                    hqPercent += 1;
                }
            }
            return hqPercent;
        }

        private double A(double x)
        {
            return (-5.6604E-6 * Math.Pow(x, 4) + 0.0015369705 * Math.Pow(x, 3) - 0.1426469573 * Math.Pow(x, 2) + 5.6122722959 * x - 5.5950384565);
        }
    }
}