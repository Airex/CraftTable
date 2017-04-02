using System;
using System.Collections.Generic;
using System.Linq;

using CraftTable.Buffs;
using CraftTable.Contracts;
using CraftTable.Exceptions;

namespace CraftTable
{
    public class CraftTable : ICraftActions
    {
        public delegate CraftTable Factory(Recipe recipe, CraftMan craftMan, IProgressWatcher progressWatcher = null);

        private int _step = 1;
        private int _craftPointsLeft;
        private int _durability;
        private int _progress;
        private int _quality;
        private readonly Recipe _recipe;
        private readonly IConditionService _conditionService;
        private readonly IRandomService _randomService;
        private readonly CraftMan _craftMan;
        private readonly IProgressWatcher _progressWatcher;
        private readonly IBuffCollector _buffCollector;
        private readonly ICalculator _calculator;
        private readonly ILookupService _lookupService;
        private readonly ICraftQualityCalculator _craftQualityCalculator;
        private readonly int _reclaimChance = 0;
        private Condition _condition;
        private readonly List<Ability> _abilityQueue = new List<Ability>();

        public CraftTable(IBuffCollector buffCollector, IConditionService conditionService, IRandomService randomService, ICalculator calculator, ILookupService lookupService, ICraftQualityCalculator craftQualityCalculator,
            Recipe recipe, CraftMan craftMan, IProgressWatcher progressWatcher = null)
        {
            if (recipe == null) throw new ArgumentNullException(nameof(recipe));
            if (craftMan == null) throw new ArgumentNullException(nameof(craftMan));
            if (buffCollector == null) throw new ArgumentNullException(nameof(buffCollector));
            if (conditionService == null) throw new ArgumentNullException(nameof(conditionService));
            if (randomService == null) throw new ArgumentNullException(nameof(randomService));
            if (calculator == null) throw new ArgumentNullException(nameof(calculator));
            if (lookupService == null) throw new ArgumentNullException(nameof(lookupService));
            if (craftQualityCalculator == null) throw new ArgumentNullException(nameof(craftQualityCalculator));

            _progressWatcher = progressWatcher ?? new DefaultProgressWatcher();

            _buffCollector = buffCollector;
            _conditionService = conditionService;
            _craftMan = craftMan;
            _progressWatcher = progressWatcher ?? new DefaultProgressWatcher();
            _randomService = randomService;
            _calculator = calculator;
            _lookupService = lookupService;
            _craftQualityCalculator = craftQualityCalculator;
            _recipe = recipe;
            _craftPointsLeft = craftMan.MaxCraftPoints;
            _durability = recipe.Durability;
            _condition = _conditionService.GetCondition(_calculator);
            _quality = recipe.StartQuality;
        }

        public CraftTableInfo GetStatus()
        {
            return new CraftTableInfo()
            {
                Condition = _condition,
                CraftPoints = _craftPointsLeft,
                Durability = _durability,
                HighQualityChance = _craftQualityCalculator.CalculateHighQualityChance(_quality, _recipe.MaxQuality),
                Progress = _progress,
                Quality = _quality,
                Step = _step,
                Buffs = _buffCollector.GetBuffs().Select(buff => new BuffInfo()
                {
                    Type = buff.GetType(),
                    Stacks = (buff as IStacks)?.Stacks??0,
                    Steps = (buff as ISteps)?.Steps??0,
                    XivDb = buff.Id()
                }).ToArray()
            };
        }

        public void Act(Ability ability)
        {
            if (_durability <= 0 || _progress >= _recipe.Difficulty)
            {
                _progressWatcher.Log("Craft finished. No Actions Allowed.");
                throw new CraftAlreadyFinishedException();
            }

            bool abilityfailed = false;
            var craftServiceState = new CraftServiceState(_condition, _craftPointsLeft, _step, _buffCollector.GetBuffAccessor());
            if (!ability.CanAct(craftServiceState))
            {
                _progressWatcher.Log($"Use of {ability} is not allowed");
                throw new AbilityNotAvailableException();
            }
            _step++;
            _buffCollector.Step(this);
            _calculator.Reset(_condition);
            _buffCollector.BuildCalculator(new ActionInfo(ability.GetType(), _condition), _calculator.GetBuilder());
            var chance = _calculator.CalculateChance(ability.Chance);
            var isSuccess = _randomService.Select(new[] { chance, 1000.0 }) == 0;
            if (!isSuccess)
            {
                abilityfailed = true;
                _calculator.Fail();
            }
            _progressWatcher.Log($"You use {ability} : {(isSuccess ? "Success" : "Failed")} with chance {chance}%");
            _progressWatcher.Log($" -> Condition is {_condition.ToString()}");
            ability.Execute(this, !abilityfailed);
            _buffCollector.PostAction(this);
            _condition = _conditionService.GetCondition(_calculator);
            _buffCollector.KillNotActive();

            Validate(abilityfailed, chance);

            var copyOfAbilities = _abilityQueue.ToArray();
            _abilityQueue.Clear();

            foreach (var a in copyOfAbilities)
            {
                Act(a);
            }
        }

        public bool CanAct(Ability ability)
        {
            if (_durability <= 0 || _progress >= _recipe.Difficulty) return false;
            var craftServiceState = new CraftServiceState(_condition, _craftPointsLeft, _step, _buffCollector.GetBuffAccessor());
            return ability.CanAct(craftServiceState);
        }

        private void Validate(bool abilityfailed, int chance)
        {

            if (_durability == 0 && _progress < _recipe.Difficulty)
            {
                _progressWatcher.Log("Craft failed!");
                var reclaimChance = _calculator.CalculateReclaimChance(_reclaimChance);
                var wasReclaimed = _randomService.Select(new[] { reclaimChance, 1000.0 }) == 0;
                if (wasReclaimed)
                    _progressWatcher.Log($"Resources were reclaimed with chance {reclaimChance}%");
                throw new CraftFailedException(wasReclaimed);
            }
            if (_progress >= _recipe.Difficulty)
            {
                var hqChance = _craftQualityCalculator.CalculateHighQualityChance(_quality, _recipe.MaxQuality);
                var isHighQuality = _randomService.Select(new[] { hqChance, 1000.0 }) == 0;
                _progressWatcher.Log($"Craft successful. {(isHighQuality ? "HQ" : "NQ")} with chance {hqChance}%");
                throw new CraftSuccessException(isHighQuality, hqChance);
            }

            if (abilityfailed)
            {
                throw new AbilityFailedException(chance);
            }
        }

        void ICraftActions.ApplyBuff(IBuff buff)
        {
            _buffCollector.Add(buff);
        }

        void ICraftActions.Synth(SynthDelegate synthDelegate)
        {
            var calculateProgress = synthDelegate(_recipe, _craftMan, _progress, _calculator);
            _progressWatcher.Log($" -> Progress increased by {calculateProgress}");
            _progress += Math.Min(calculateProgress, _recipe.Difficulty - _progress);
        }

        void ICraftActions.Touch(int efficiency)
        {
            var calculateQuality = _calculator.CalculateQuality(efficiency, _craftMan.Control, _recipe.Level, _craftMan.Level);
            _progressWatcher.Log($" -> Quality increased by {calculateQuality}");
            _quality += Math.Min(_recipe.MaxQuality - _quality, calculateQuality);
        }

        void ICraftActions.UseCraftPoints(int craftPoints)
        {
            var calculateCraftPoints = _calculator.CalculateCraftPoints(craftPoints);
            _progressWatcher.Log($" -> Used {calculateCraftPoints} CP");
            _craftPointsLeft -= calculateCraftPoints;
        }

        void ICraftActions.UseDurability(int durability)
        {
            var calulated = Math.Min(_durability, _calculator.CalculateDurability(durability));
            _progressWatcher.Log($" -> Used {calulated} durability");
            _durability -= calulated;
        }

        public T CalculateDependency<T>(CalculateDependency<T> input) where T : struct
        {
            return input(_buffCollector.GetBuffAccessor(), _lookupService, _recipe);
        }

        void IBuffActions.RestoreCraftPoints(int craftPoints)
        {
            var craftPointsLeft = Math.Min(_craftMan.MaxCraftPoints - _craftPointsLeft, craftPoints);
            _progressWatcher.Log($" -> Restored {craftPointsLeft} CP");
            _craftPointsLeft += craftPointsLeft;
        }

        void IBuffActions.RestoreDurability(int durability)
        {
            if (_durability <= 0) return;

            var calculated = Math.Min(_recipe.Durability - _durability, durability);
            _progressWatcher.Log($" -> Restored {calculated} durability");
            _durability += calculated;
        }

        
        public void QueueAbility(Ability a)
        {
            _abilityQueue.Add(a);
        }
    }
}