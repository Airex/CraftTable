using System;

namespace CrafterExperiment
{
    public interface ICalculatorBuilder
    {
        void ForDurability(DurabilityActor action);
        void ForProgress(ProgressActor action);
        void ForQuality(QualityActor action);
        void ForCraftPoints(CraftPointsActor action);
        void ForChance(ChanceActor action);
    }
}