using System;
using System.Collections.Generic;

namespace CraftTable
{
    public class CraftTableInfo
    {
        public int Step { get; set; }
        public int Durability { get; set; }
        public int Progress { get; set; }
        public int Quality { get; set; }
        public int CraftPoints { get; set; }
        public Condition Condition { get; set; }
        public IList<BuffInfo> Buffs { get; set; }
        public int HighQualityChance { get; set; }
    }

    public class BuffInfo
    {
        public Type Type { get; set; }
        public string XivDb { get; set; }
        public int Steps { get; set; }
        public int Stacks { get; set; }
    }
}