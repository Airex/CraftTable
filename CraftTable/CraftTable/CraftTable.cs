using System;
using CraftTable.Contracts;
using CraftTable.Exceptions;

namespace CraftTable
{
    public class CraftTable : ICraftActions
    {
        public delegate CraftTable Factory(Recipe recipe, CraftMan craftMan);

        private int _step = 1;
        private int _craftPointsLeft;
        private int _durability;
        private int _progress;
        private int _quality;
        private readonly Recipe _recipe;
        private readonly IConditionService _conditionService;
        private readonly IRandomService _randomService;
        private readonly CraftMan _craftMan;
        private readonly IBuffCollector _buffCollector;
        private readonly ICalculator _calculator;
        Condition _preCondition = Condition.Normal;

        public CraftTable(Recipe recipe, CraftMan craftMan, IBuffCollector buffCollector, IConditionService conditionService, IRandomService randomService, ICalculator calculator)
        {
            _buffCollector = buffCollector;
            _conditionService = conditionService;
            _craftMan = craftMan;
            _randomService = randomService;
            _calculator = calculator;
            _recipe = recipe;
            _craftPointsLeft = craftMan.MaxCraftPoints;
            _durability = recipe.Durability;

        }

        public int Step => _step;
        public int Durability => _durability;
        public int Progress => _progress;
        public int Quality => _quality;
        public int CraftPoints => _craftPointsLeft;

        public void Act(Ability ability)
        {
            _step++;
            _buffCollector.Step(this);
            _calculator.Reset();
            _buffCollector.BuildCalculator(new ActionInfo(ability.GetType(), _preCondition), _calculator.GetBuilder());
            var condition = _conditionService.GetCondition(_calculator);
            _calculator.UseConditionMultiplier(GetMultiplier(condition));
            var craftServiceState = new CraftServiceState(condition, _craftPointsLeft, _step, _buffCollector.GetBuffAccessor());
            if (!ability.CanAct(craftServiceState)) throw new AbilityNotAvailableException();
            
            var isSuccess = _randomService.Select(new[] { _calculator.CalculateChance(ability.Chance), double.PositiveInfinity }) == 0;
            if (!isSuccess)
            {
                _calculator.Fail();
            }
            Console.WriteLine($"You use {ability.GetType().Name} : {(isSuccess ? "Success" : "Failed")}");
            ability.Execute(this);
            _preCondition = condition;
            Validate();
        }

        private double GetMultiplier(Condition condition)
        {
            switch (condition)
            {
                case Condition.Normal:
                    return 1.0;
                case Condition.Good:
                    return 1.5;
                case Condition.Extreme:
                    return 4.0;
                case Condition.Poor:
                    return 0.5;
                default:
                    throw new ArgumentOutOfRangeException(nameof(condition), condition, null);
            }
        }

        private void Validate()
        {
            if (_durability == 0 && _progress < _recipe.Difficulty)
                throw new CraftFailedException();
        }

        void ICraftActions.ApplyBuff(IBuff buff)
        {
            _buffCollector.Add(buff);
        }

        void ICraftActions.Synth(SynthDelegate synthDelegate)
        {
            var calculateProgress = synthDelegate(_recipe, _craftMan, _progress, _calculator);
            Console.WriteLine($" -> Progress increased by {calculateProgress}");
            _progress += calculateProgress;
        }

        void ICraftActions.Touch(int efficiency)
        {
            var calculateQuality = _calculator.CalculateQuality(efficiency, _craftMan.Control, _recipe.Level - _craftMan.Level);
            Console.WriteLine($" -> Quality increased by {calculateQuality}");
            _quality += calculateQuality;
        }

        void ICraftActions.UseCraftPoints(int craftPoints)
        {
            var calculateCraftPoints = _calculator.CalculateCraftPoints(craftPoints);
            Console.WriteLine($" -> Used {calculateCraftPoints} CP");
            _craftPointsLeft -= calculateCraftPoints;
        }

        void ICraftActions.UseDurability(int durability)
        {
            var calulated = Math.Min(_durability, _calculator.CalculateDurability(durability));
            Console.WriteLine($" -> Used {calulated} durability");
            _durability -= calulated;
        }

        void IBuffActions.RestoreCraftPoints(int craftPoints)
        {
            var craftPointsLeft = Math.Min(_craftMan.MaxCraftPoints - _craftPointsLeft, craftPoints);
            Console.WriteLine($" -> Restored {craftPointsLeft} CP");
            _craftPointsLeft += craftPointsLeft;
        }

        void IBuffActions.RestoreDurability(int durability)
        {
            var calculated = Math.Min(_recipe.Durability - durability, _durability + durability);
            Console.WriteLine($" -> Restored {calculated} durability");
            _durability += calculated;
        }
    }
}