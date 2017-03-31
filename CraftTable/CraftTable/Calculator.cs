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
        private ProgressActor _progress = (a, b,c) => { };
        private QualityActor _quality = (a, b,c) => { };
        private CraftPointsActor _craftPoints = a => { };
        private ChanceActor _chance = a => { };
        private ConditionChanceActor _conditionChance = (a, b) => { };
        private RecipeLevelActor _levelActor = (a, c, d) => { };
        private ReclaimChanceActor _reclaimChanceActor = (a) => { };

        private bool _failed;

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
            _progress(efficiencyActor, craftmanshipActor,!_failed);
            var level = _lookupService.MapLevel(craftmanLevel) ?? craftmanLevel;
            return (int)_efficiencyCalculator.CraftmanshipToProgress(craftmanshipActor.Value, efficiencyActor.Value, (int)level, CalculateLevelDifference(recipeLevel, craftmanLevel));
        }

        public int CalculateQuality(int efficiency, int value, int recipeLevel, int craftmanLevel)
        {
            var efficiencyActor = new CalculatorActor(efficiency);
            var controlActor = new CalculatorActor(value);
            _quality(efficiencyActor, controlActor,!_failed);
            return (int)_efficiencyCalculator.ControlToQuality(controlActor.Value, efficiencyActor.Value, recipeLevel, CalculateLevelDifference(recipeLevel, craftmanLevel));
        }

        public int CalculateCraftPoints(int value)
        {
            var actor = new CalculatorActor(value);
            _craftPoints(actor);
            return (int)Math.Round(actor.Value, 0);
        }

        public void Reset(Condition condition)
        {
            _failed = false;
            _efficiencyCalculator.UseConditionMultylier(_lookupService.GetConditionMultiplier(condition));
            _durability = a => { };
            _progress = (a, b,c) => { };
            _quality = (a, b,c) => { };
            _craftPoints = a => { };
            _chance = a => { };
            _conditionChance = (a, b) => { };
            _levelActor = (a, c, d) => { };
            _reclaimChanceActor = (a) => { };
        }

        public void Fail()
        {
            _failed = true;
            GetBuilder().ForQuality((efficincy, control, c) => { efficincy.Multiply(0); });
            GetBuilder().ForProgress((efficincy, crafmanship, c) => { efficincy.Multiply(0); });
        }

        public double CalculateReclaimChance(int reclaimChance)
        {
            CalculatorActor actor = new CalculatorActor(reclaimChance);
            _reclaimChanceActor(actor);
            return actor.Value;
        }

        public int CalculateChance(int abilityChance)
        {
            var chanceActor = new CalculatorActor(abilityChance);
            _chance(chanceActor);
            return Math.Min((int)chanceActor.Value, 100);
        }

        public double CalculateConditionChance(Condition condition, int value)
        {
            CalculatorActor chanceActor = new CalculatorActor(value);
            _conditionChance(condition, chanceActor);
            return chanceActor.Value;
        }

        

        private double CalculateLevelDifference(int recipeLevel, int craftmaneLevel)
        {
            var effCrafterLevel = _lookupService.MapLevel(craftmaneLevel) ?? craftmaneLevel;

            var actor = new CalculatorActor(0);
            _levelActor(recipeLevel, actor, _lookupService);
            double lvl = recipeLevel;
            if (actor.Value>0)
                lvl = actor.Value;

            double levelDifference = effCrafterLevel - lvl;
            

            if (levelDifference > 0)
            {
                levelDifference = Math.Min(levelDifference, 20);
            }

            if (levelDifference < 0)
            {
                levelDifference = Math.Max(levelDifference, -5);
            }
            return (int)levelDifference;
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

        public void ForReclaimChance(ReclaimChanceActor action)
        {
            _reclaimChanceActor += action;
        }
    }
}