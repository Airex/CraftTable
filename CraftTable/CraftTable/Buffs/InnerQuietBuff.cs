using System;
using CraftTable.Abilities;
using CraftTable.Abilities.Specialist;
using CraftTable.Contracts;

namespace CraftTable.Buffs
{
    [BuffXivDb(251)]
    public class InnerQuietBuff : IBuff,IStacks
    {
        private int _stacks = 1;
        public int Stacks
        {
            get { return _stacks; }
            private set
            {
                _stacks = Math.Min(11, value);
            }
        }

        public bool IsActive { get; private set; } = true;


        public void Step(IBuffActions buffActions)
        {

        }

        public void Kill()
        {
            IsActive = false;
        }

        public void OnCalculate(ActionInfo info, ICalculatorBuilder calculatorBuilder)
        {
            int stacks = Stacks -1;
            calculatorBuilder.ForQuality((efficiency, control,s) =>
            {
                
                if (info.AbilityType == typeof(ByregotsBlessing))
                {
                    Kill();
                    efficiency.Add(20 * stacks);
                }

                if (info.AbilityType == typeof(ByregotsMiracle))
                {
                    _stacks = (int)Math.Round((double)_stacks / 2);
                    efficiency.Add(10 * stacks);
                }

                if (info.AbilityType == typeof(ByregotsBrow))
                {
                    Kill();
                    efficiency.Add(10 * stacks);
                }

                control.AddPercent(20 * stacks);

                if (s)
                {
                    if (info.AbilityType == typeof(PreciseTouch)) Stacks++;
                    Stacks++;
                }

            });
        }
    }
}