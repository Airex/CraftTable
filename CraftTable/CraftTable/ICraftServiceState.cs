﻿namespace CrafterExperiment
{
    public interface ICraftServiceState
    {
        int StepNumber { get; }
        Condition Condition { get; }
        int CraftPointsLeft { get; }
        IBuffAccessor BuffAccessor { get; }
    }
}