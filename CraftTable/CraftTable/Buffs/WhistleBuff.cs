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

        private int _stacks = 11;

        public bool IsActive => _stacks > 0;

        public void Step(IBuffActions buffActions)
        {
           
        }

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

                if (_stacks == 0)
                {
                    //todo implement   Finishing touch
                }
            });
        }

        public void Kill()
        {
            _stacks = 0;
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

        
    }


}