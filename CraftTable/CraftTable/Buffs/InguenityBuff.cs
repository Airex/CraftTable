using System;
using CraftTable.Contracts;

namespace CraftTable.Buffs
{
    public class InguenityBuff : StepsBasedBuff
    {
        private readonly int _power;
        public InguenityBuff(int power) : base(5)
        {
            _power = power;
        }

        public override void OnCalculate(ActionInfo info, ICalculatorBuilder calculatorBuilder)
        {
            calculatorBuilder.ForRecipeLevel((level, manLevel, action, service) =>
            {
                var effCrafterLevel = service.MapLevel(manLevel) ?? manLevel;
                double effRecipeLevel = level;

                double levelDifference;

                var lvl = _power == 1 ? service.MapInguenity1Level(level) : service.MapInguenity2Level(level);
                if (lvl.HasValue)
                {
                    effRecipeLevel = lvl.Value;
                    levelDifference = effCrafterLevel - effRecipeLevel;
                }
                else
                {
                    levelDifference = effCrafterLevel - (effRecipeLevel - (_power == 1 ? 5 : 7)); // fall back on 2.2 estimate
                }

                if (levelDifference > 0)
                {
                    levelDifference = Math.Min(levelDifference, 20);
                }

                if (levelDifference < 0)
                {
                    levelDifference = Math.Max(levelDifference, -5);
                }
                action.Add(levelDifference);

            });
        }

        protected override void OnStep(IBuffActions buffActions)
        {

        }
    }
}