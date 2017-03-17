using System;

namespace CraftTable.Contracts
{
    public interface ILookupService
    {
        double? MapLevel(int level);
        double? MapInguenity1Level(int level);
        double? MapInguenity2Level(int level);
        double MapNymeriasWheelStacks(int stacks);
    }
}