using System;
using CraftTable.Contracts;
using CraftTable.CraftActors;

namespace CraftTable
{
    public class Calculator : ICalculatorBuilder, ICalculator
    {
        private readonly IEfficiencyCalculator _efficiencyCalculator;
        private DurabilityActor _durability = a => { };
        private ProgressActor _progress = (a, b) => { };
        private QualityActor _quality = (a, b) => { };
        private CraftPointsActor _craftPoints = a => { };
        private ChanceActor _chance = a => { };
        private ConditionChanceActor _conditionChance = (a, b) => { };
        private RecipeLevelActor _levelActor = a => { };

        public Calculator(IEfficiencyCalculator efficiencyCalculator)
        {
            _efficiencyCalculator = efficiencyCalculator;
        }

        public void ForDurability(DurabilityActor action)
        {
            _durability += action;
        }

        public void ForProgress(ProgressActor action)
        {
            _progress += action;
        }

        public void ForQuality(QualityActor action)
        {
            _quality += action;
        }

        public void ForCraftPoints(CraftPointsActor action)
        {
            _craftPoints += action;
        }

        public int CalculateDurability(int value)
        {
            var actor = new CalculatorActor(value);
            _durability(actor);
            return (int) Math.Round(actor.Value,0);
        }

        public int CalculateProgress(int efficiency, int value, int levelDifference)
        {
            var efficiencyActor = new CalculatorActor(efficiency);
            var craftmanshipActor = new CalculatorActor(value);
            _progress(efficiencyActor, craftmanshipActor);
            return (int)_efficiencyCalculator.CraftmanshipToProgress(craftmanshipActor.Value, efficiencyActor.Value, CalculateLevelDifference(levelDifference));
        }

        public int CalculateQuality(int efficiency, int value, int levelDifference)
        {
            var efficiencyActor = new CalculatorActor(efficiency);
            var controlActor = new CalculatorActor(value);
            _quality(efficiencyActor, controlActor);
            return (int) _efficiencyCalculator.ControlToProgress(controlActor.Value, efficiencyActor.Value, CalculateLevelDifference(levelDifference));
        }

        public int CalculateCraftPoints(int value)
        {
            var actor = new CalculatorActor(value);
            _craftPoints(actor);
            return (int) Math.Round(actor.Value, 0);
        }

        public void Reset()
        {
            _durability = a => { };
            _progress = (a, b) => { };
            _quality = (a, b) => { };
            _craftPoints = a => { };
        }

        public int CalculateChance(int abilityChance)
        {
            var chanceActor = new CalculatorActor(abilityChance);
            _chance(chanceActor);
            return (int) chanceActor.Value;
        }
        public void UseConditionMultiplier(double getMultiplier)
        {
            _efficiencyCalculator.UseConditionMultylier(getMultiplier);
        }

        public double CalculateConditionChance(Condition condition, int value)
        {
            CalculatorActor chanceActor = new CalculatorActor(value);
            _conditionChance(condition, chanceActor);
            return chanceActor.Value;
        }

        private int CalculateLevelDifference(int difference)
        {
            CalculatorActor actor = new CalculatorActor(difference);
            _levelActor(actor);
            return (int) actor.Value;
        }

        public ICalculatorBuilder GetBuilder()
        {
            return this;
        }

        public void ForChance(ChanceActor action)
        {
            _chance += action;
        }

        public void ForConditionChance(ConditionChanceActor action)
        {
            _conditionChance += action;
        }

        public void ForRecipeLevel(RecipeLevelActor action)
        {
            _levelActor += action;
        }
    }
}