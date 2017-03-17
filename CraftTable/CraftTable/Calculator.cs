using System;
using CraftTable.Contracts;
using CraftTable.CraftActors;

namespace CraftTable
{
    public class Calculator : ICalculatorBuilder, ICalculator
    {
        private readonly IEfficiencyCalculator _efficiencyCalculator;
        private readonly ILookupService _lookupService;
        private DurabilityActor _durability = a => { };
        private ProgressActor _progress = (a, b) => { };
        private QualityActor _quality = (a, b) => { };
        private CraftPointsActor _craftPoints = a => { };
        private ChanceActor _chance = a => { };
        private ConditionChanceActor _conditionChance = (a, b) => { };
        private RecipeLevelActor _levelActor = (a, b, c, d) => { };

        public Calculator(IEfficiencyCalculator efficiencyCalculator, ILookupService lookupService)
        {
            _efficiencyCalculator = efficiencyCalculator;
            _lookupService = lookupService;
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
            return (int)Math.Round(actor.Value, 0);
        }

        public int CalculateProgress(int efficiency, int value, int recipeLevel, int craftmanLevel)
        {
            var efficiencyActor = new CalculatorActor(efficiency);
            var craftmanshipActor = new CalculatorActor(value);
            _progress(efficiencyActor, craftmanshipActor);
            return (int)_efficiencyCalculator.CraftmanshipToProgress(craftmanshipActor.Value, efficiencyActor.Value, CalculateLevelDifference(recipeLevel, craftmanLevel));
        }

        public int CalculateQuality(int efficiency, int value, int recipeLevel, int craftmanLevel)
        {
            var efficiencyActor = new CalculatorActor(efficiency);
            var controlActor = new CalculatorActor(value);
            _quality(efficiencyActor, controlActor);
            return (int)_efficiencyCalculator.ControlToProgress(controlActor.Value, efficiencyActor.Value, CalculateLevelDifference(recipeLevel, craftmanLevel));
        }

        public int CalculateCraftPoints(int value)
        {
            var actor = new CalculatorActor(value);
            _craftPoints(actor);
            return (int)Math.Round(actor.Value, 0);
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
            return (int)chanceActor.Value;
        }
        public void UseCondition(Condition condition)
        {
            _efficiencyCalculator.UseConditionMultylier(_lookupService.GetConditionMultiplier(condition));
        }

        public double CalculateConditionChance(Condition condition, int value)
        {
            CalculatorActor chanceActor = new CalculatorActor(value);
            _conditionChance(condition, chanceActor);
            return chanceActor.Value;
        }

        private int CalculateLevelDifference(int recipeLevel, int craftmaneLevel)
        {
            var actor = new CalculatorActor(0);
            _levelActor(recipeLevel, craftmaneLevel, actor, _lookupService);
            return (int)actor.Value;
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