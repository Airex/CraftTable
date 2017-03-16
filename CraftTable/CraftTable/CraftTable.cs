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
            var condition = _conditionService.GetCondition();
            var craftServiceState = new CraftServiceState(condition, _craftPointsLeft, _step, _buffCollector.GetBuffAccessor());
            if (!ability.CanAct(craftServiceState)) throw new AbilityNotAvailableException();
            _calculator.Reset();
            _calculator.UseConditionMultiplier(GetMultiplier(condition));
            _buffCollector.BuildCalculator(new ActionInfo() { AbilityType = ability.GetType() }, _calculator.GetBuilder());
            if (_randomService.SelectItem(new[] { _calculator.CalculateChance(ability.Chance), -1 }) > 0) return;
            ability.Execute(this);
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

        void ICraftActions.Synth(int efficiency)
        {
            _progress += _calculator.CalculateProgress(efficiency, _craftMan.Craftmanship);
        }

        void ICraftActions.SynthPercent(int percent)
        {
            _progress += (int)((_recipe.Difficulty - _progress) * (double) percent / 100);
        }

        void ICraftActions.Touch(int efficiency)
        {
            var calculateQuality = _calculator.CalculateQuality(efficiency, _craftMan.Control);
            Console.WriteLine($"Quality increased by {calculateQuality}");
            _quality += calculateQuality;
        }

        void ICraftActions.UseCraftPoints(int craftPoints)
        {
            _craftPointsLeft -= _calculator.CalculateCraftPoints(craftPoints);
        }

        void ICraftActions.UseDurability(int durability)
        {
            _durability = Math.Max(0, _durability - _calculator.CalculateDurability(durability));
        }

        void IBuffActions.RestoreCraftPoints(int craftPoints)
        {
            _craftPointsLeft = Math.Min(_craftMan.MaxCraftPoints, craftPoints + _craftPointsLeft);
        }

        void IBuffActions.RestoreDurability(int durability)
        {
            _durability = Math.Min(_recipe.Durability, _durability + durability);
        }
    }
}