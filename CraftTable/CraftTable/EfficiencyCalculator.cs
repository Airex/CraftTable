using System;
using CraftTable.Contracts;

namespace CraftTable
{
    public class EfficiencyCalculator : IEfficiencyCalculator
    {
        private double _conditionMultiplier = 1.0;
        public double CraftmanshipToProgress(double craftsmanship, double efficiency)
        {
            double baseProgress = 0;
            double levelCorrectionFactor = 0;
            double levelCorrectedProgress = 0;
            var crafterLevel = 150;
            var levelDifference = 0;

            if (crafterLevel >= 120)
            {
                baseProgress = 2.09860e-5 * craftsmanship * craftsmanship + 0.196184 * craftsmanship + 2.68452;

                // Level boost for recipes below crafter level
                // Level boost arbitrarily capped at 100 levels for now because of limited data
                if (levelDifference > 0)
                {
                    levelCorrectionFactor += 0.0504824 * Math.Min(levelDifference, 5);
                }
                if (levelDifference > 5)
                {
                    levelCorrectionFactor += 0.0205906 * Math.Min(levelDifference - 5, 10);
                }
                if (levelDifference > 15)
                {
                    levelCorrectionFactor += 0.0106398 * Math.Min(levelDifference - 15, 5);
                }
                if (levelDifference > 20)
                {
                    levelCorrectionFactor += 6.69723e-4 * Math.Min(levelDifference - 20, 100);
                }

                // Level penalty for recipes above crafter level
                // Level difference penalty appears to be capped at -6
                levelDifference = Math.Max(levelDifference, -6);
                if (levelDifference < 0)
                {
                    levelCorrectionFactor += 0.0807176 * Math.Max(levelDifference, -5);
                }
                if (levelDifference < -5)
                {
                    levelCorrectionFactor += 0.0525673 * Math.Max(levelDifference - (-5), -1);
                }

                levelCorrectedProgress = (1 + levelCorrectionFactor) * baseProgress;
            }
            else if (crafterLevel < 120)
            {
                baseProgress = 0.214959 * craftsmanship + 1.6;

                // Level boost for recipes below crafter level
                // Level boost arbitrarily capped at 100 levels for now because of limited data
                if (levelDifference > 0)
                {
                    levelCorrectionFactor += 0.0495218 * Math.Min(levelDifference, 5);
                }
                if (levelDifference > 5)
                {
                    levelCorrectionFactor += 0.0221127 * Math.Min(levelDifference - 5, 10);
                }
                if (levelDifference > 15)
                {
                    levelCorrectionFactor += 0.0103120 * Math.Min(levelDifference - 15, 5);
                }
                if (levelDifference > 20)
                {
                    levelCorrectionFactor += 6.68438e-4 * Math.Min(levelDifference - 20, 100);
                }

                // Level penalty for recipes above crafter level
                // Level difference penalty was capped at -9 in 2.2
                levelDifference = Math.Max(levelDifference, -9);
                /*
                if (levelDifference < 0){
                    levelCorrectionFactor += 0.080554 * Math.max(levelDifference, -5);
                }
                if (levelDifference < -5){
                    levelCorrectionFactor += 0.0487896 * Math.max(levelDifference - (-5), -1);
                }
                */

                if ((levelDifference < -5))
                {
                    levelCorrectionFactor = 0.0501 * levelDifference;
                }
                else if ((-5 <= levelDifference) && (levelDifference < 0))
                {
                    levelCorrectionFactor = 0.10 * levelDifference;
                }

                levelCorrectedProgress = (1 + levelCorrectionFactor) * baseProgress;
            }

            return Math.Round(levelCorrectedProgress  * efficiency / 100);
        }

        public double ControlToProgress(double control, double efficiency)
        {
            Console.WriteLine("Calculated control: "+control);
            int recipeLevel = 150;
            double baseQuality = 0;
            double recipeLevelFactor = 0;
            double levelCorrectionFactor = 0;
            double levelCorrectedQuality = 0;
            int levelDifference = 0;

            if (recipeLevel >= 115)
            {
                baseQuality = 3.37576e-5 * control * control + 0.338835 * control + 33.1305;

                recipeLevelFactor = 3.37610e-4 * (115 - recipeLevel);

                // Level penalty for recipes above crafter level
                // Level difference penalty appears to be capped at -6
                levelDifference = Math.Max(levelDifference, -6);
                if (levelDifference < 0)
                {
                    levelCorrectionFactor = 0.0400267 * Math.Max(levelDifference, -3);
                }
                if (levelDifference < -3)
                {
                    levelCorrectionFactor += 0.0451309 * Math.Max(levelDifference - (-3), -3);
                }

                levelCorrectedQuality = baseQuality * (1 + levelCorrectionFactor) * (1 + recipeLevelFactor);
            }
            else if (recipeLevel > 50)
            {
                baseQuality = 3.46e-5 * control * control + 0.3514 * control + 34.66;

                levelDifference = Math.Max(levelDifference, -5);
                if (levelDifference <= -5)
                {
                    levelCorrectionFactor = 0.05374 * levelDifference;
                }
                else
                {
                    //if levelDifference > -5
                    // Ingenuity does not quite reduce LDiff to 0
                    levelCorrectionFactor = 0.05 * -0.5;
                }

                levelCorrectedQuality = baseQuality * (1 + levelCorrectionFactor);
            }
            else
            {
                baseQuality = 3.46e-5 * control * control + 0.3514 * control + 34.66;

                levelDifference = Math.Max(levelDifference, -5);
                if (levelDifference < 0)
                {
                    levelCorrectionFactor = 0.05 * levelDifference;
                }

                levelCorrectedQuality = baseQuality * (1 + levelCorrectionFactor);
            }

            Console.WriteLine("Calculated Quality " + levelCorrectedQuality);

            var controlToProgress = Math.Round(levelCorrectedQuality * efficiency/100 * _conditionMultiplier);

            Console.WriteLine("Calculated Quality "+controlToProgress);
            return controlToProgress;
        }

        public void UseConditionMultylier(double m)
        {
            _conditionMultiplier = m;
        }
    }
}