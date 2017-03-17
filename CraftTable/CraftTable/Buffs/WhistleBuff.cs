using System;
using CraftTable.Abilities.Specialist;
using CraftTable.Contracts;

namespace CraftTable.Buffs
{
    public class WhistleBuff : IBuff
    {
        private Action afterSatisfaction;

        private int _stacks = 11;

        public bool IsActive => _stacks > 0;

        public void Step(IBuffActions buffActions)
        {
            if (afterSatisfaction != null)
            {
                afterSatisfaction();
                afterSatisfaction = null;
            }
            
            if (_stacks == 0)
            {
                var craftActions = buffActions as ICraftActions;
                //todo implement   Finishing touch
            }
        }

        public int Stacks => _stacks;

        public void Kill()
        {
            _stacks = 0;
        }

        public void OnCalculate(ActionInfo info, ICalculatorBuilder calculatorBuilder)
        {
            int stacks = _stacks;
            calculatorBuilder.ForProgress((efficincy, craftmanship) =>
            {
                efficincy.AddPercent(stacks % 3 == 0 ? 50 : 0);
            });

            if (info.AbilityType == typeof(Satisfaction))
            {
                afterSatisfaction = () => _stacks--;
            }

            if (info.Condition.IsGoodOrExtreme())
            {
                _stacks--;
            }
        }
    }


}