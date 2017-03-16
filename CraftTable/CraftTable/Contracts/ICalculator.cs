﻿namespace CraftTable.Contracts
{
    public interface ICalculator
    {
        int CalculateDurability(int value);
        int CalculateProgress(int efficiency, int value);
        int CalculateQuality(int efficiency, int value);
        int CalculateCraftPoints(int value);

        ICalculatorBuilder GetBuilder();

        void Reset();

        int CalculateChance(int abilityChance);
        void UseConditionMultiplier(double getMultiplier);
    }
}