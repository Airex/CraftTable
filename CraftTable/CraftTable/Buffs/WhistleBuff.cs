using System;
using CraftTable.Contracts;

namespace CraftTable.Buffs
{
    public class WhistleBuff:IBuff
    {
        private int _stacks = 11;

        public bool IsActive => _stacks < 0;

        public void Step(IBuffActions buffActions)
        {
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
            if (_stacks % 3 == 0 && ApplicableToAbility(info.AbilityType))
            {
                //todo: increate effectivnes
            }

            if (info.Condition.IsGoodOrExtreme())
            {
                _stacks--;
            }
        }

        private bool ApplicableToAbility(Type infoAbilityType)
        {
            return false;
        }
    }

   
}