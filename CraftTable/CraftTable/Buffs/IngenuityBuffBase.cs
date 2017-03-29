using System;
using CraftTable.Contracts;

namespace CraftTable.Buffs
{
    public abstract class IngenuityBuffBase : StepsBasedBuff
    {
        private readonly int _power;

        protected IngenuityBuffBase(int power) : base(5)
        {
            _power = power;
        }

        public override void OnCalculate(ActionInfo info, ICalculatorBuilder calculatorBuilder)
        {
            calculatorBuilder.ForRecipeLevel((level, action, service) =>
            {
                var lvl = _power == 1 ? service.MapInguenity1Level(level) : service.MapInguenity2Level(level);
                if (lvl.HasValue)
                {
                    action.Add(lvl.Value);
                }
                else
                {
                    action.Add((double) level - (_power == 1 ? 5 : 7));
                }
            });
        }
    }
}