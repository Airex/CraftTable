using System;
using System.Collections.Generic;
using CraftTable.Contracts;

namespace CraftTable
{
    internal class LookupService : ILookupService
    {
        private readonly IDictionary<int, double> _ing1RecipeLevelTable = new Dictionary<int, double>
        {
            {40, 36},
            {41, 36},
            {42, 37},
            {43, 38},
            {44, 39},
            {45, 40},
            {46, 41},
            {47, 42},
            {48, 43},
            {49, 44},
            {50, 45},
            {55, 50}, // 50_1star     *** unverified
            {70, 50}, // 50_2star     *** unverified
            {90, 58}, // 50_3star     *** unverified
            {110, 58}, // 50_4star     *** unverified
            {115, 100}, // 51 @ 169/339 difficulty
            {120, 100}, // 51 @ 210/410 difficulty
            {125, 100}, // 52
            {130, 110}, // 53
            {133, 110}, // 54
            {136, 110}, // 55
            {139, 124}, // 56
            {142, 129.5}, // 57
            {145, 134.5}, // 58
            {148, 139}, // 59
            {150, 140}, // 60
            {160, 151}, // 60_1star
            {170, 152.085}, // 60_2star
            {180, 153.185}, // 60_3star
            {190, 154.275} // 60_4star
        };

        private readonly IDictionary<int, double> _ing2RecipeLevelTable = new Dictionary<int, double>
        {
            {40, 33},
            {41, 34},
            {42, 35},
            {43, 36},
            {44, 37},
            {45, 38},
            {46, 39},
            {47, 40},
            {48, 40},
            {49, 41},
            {50, 42},
            {55, 47}, // 50_1star     *** unverified
            {70, 47}, // 50_2star     *** unverified
            {90, 56}, // 50_3star     *** unverified
            {110, 56}, // 50_4star     *** unverified
            {115, 100}, // 51 @ 169/339 difficulty
            {120, 100}, // 51 @ 210/410 difficulty
            {125, 100}, // 52
            {130, 110}, // 53
            {133, 110}, // 54
            {136, 110}, // 55
            {139, 124}, // 56
            {142, 129.5}, // 57
            {145, 133}, // 58
            {148, 136}, // 59
            {150, 139}, // 60
            {160, 150}, // 60_1star
            {170, 151.115}, // 60_2star
            {180, 152.215}, // 60_3star
            {190, 153.305} // 60_4star
        };

        private readonly IDictionary<int, int> _levelTable = new Dictionary<int, int>
        {
            {51, 120}, // 120
            {52, 125}, // 125
            {53, 130}, // 130
            {54, 133}, // 133
            {55, 136}, // 136
            {56, 139}, // 139
            {57, 142}, // 142
            {58, 145}, // 145
            {59, 148}, // 148
            {60, 150} // 150
        };

        private readonly IDictionary<int, int> _nymeaisWheelTable = new Dictionary<int, int>()
        {
            {1, 30},
            {2, 30},
            {3, 30},
            {4, 20},
            {5, 20},
            {6, 20},
            {7, 10},
            {8, 10},
            {9, 10},
            {10, 10},
            {11, 10}
        };

        public double? MapLevel(int level)
        {
            if (!_levelTable.ContainsKey(level)) return null;
            return _levelTable[level];
        }

        public double? MapInguenity1Level(int level)
        {
            if (!_ing1RecipeLevelTable.ContainsKey(level)) return null;
            return _ing1RecipeLevelTable[level];
        }

        public double? MapInguenity2Level(int level)
        {
            if (!_ing2RecipeLevelTable.ContainsKey(level)) return null;
            return _ing2RecipeLevelTable[level];
        }

        public double MapNymeriasWheelStacks(int stacks)
        {
            return _nymeaisWheelTable[stacks];
        }

        public double GetConditionMultiplier(Condition condition)
        {
            switch (condition)
            {
                case Condition.Normal:
                    return 1.0;
                case Condition.Good:
                    return 1.5;
                case Condition.Extreme:
                    return 4.0;
                case Condition.Poor:
                    return 0.5;
                default:
                    throw new ArgumentOutOfRangeException(nameof(condition), condition, null);
            }
        }
    }
}