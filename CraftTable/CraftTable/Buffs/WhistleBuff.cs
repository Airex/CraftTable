using System;
using CraftTable.Abilities.Specialist;
using CraftTable.Attributes;
using CraftTable.Contracts;

namespace CraftTable.Buffs
{
    [BuffXivDb(880)]
    public class WhistleBuff : IBuff,IStacks
    {
        private Action _afterSatisfaction;
        private Action _afterNymerianWheel;

        public WhistleBuff()
        {
            IsActive = true;
        }

        private int _stacks = 11;

        public bool IsActive { get; set; }
     
        public int Stacks => _stacks;

        public void Step(IBuffActionsRegistry buffActionsRegistry)
        {
            buffActionsRegistry.RegisterPostAbility(actions =>
            {
                if (_afterSatisfaction != null)
                {
                    _afterSatisfaction();
                    _afterSatisfaction = null;
                }

                if (_afterNymerianWheel != null)
                {
                    _afterNymerianWheel();
                    _afterNymerianWheel = null;
                }

                if (Stacks <= 0)
                {
                    Kill();
                    actions.QueueAbility(new FinishingTouches());
                }
               
            });
        }

        public void Kill()
        {
            IsActive = false;
        }

        public void OnCalculate(ActionInfo info, ICalculatorBuilder calculatorBuilder)
        {
            int stacks = _stacks;
            calculatorBuilder.ForProgress((efficincy, craftmanship, s) =>
            {
                efficincy.AddPercent(stacks % 3 == 0 ? 50 : 0);
            });

            if (info.AbilityType == typeof(NymeiasWheel))
            {
                _afterNymerianWheel = Kill;
            }

            if (info.AbilityType == typeof(Satisfaction))
            {
                _afterSatisfaction = () => _stacks--;
            }

            if (info.Condition.IsGoodOrExcellent())
            {
                _stacks--;
            }
        }

        class FinishingTouches : Ability
        {
            public override int Chance { get; } = 50;

            public override void Execute(ICraftActions craftActions, bool isSuccess)
            {
                craftActions.Synth(Synth.FromEfficiency(150));
                craftActions.Touch(150);
            }

            public override bool CanAct(ICraftServiceState serviceState)
            {
                return true;
            }
        }
    }

    
}