using System;

namespace CraftTable
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AbilityDescriptorAttribute : Attribute
    {
        public AbilityDescriptorAttribute(string name, Crafter crafterAfinity, int cpCost, bool isCrossClass)
        {
            CpCost = cpCost;
            CrafterAfinity = crafterAfinity;
            IsCrossClass = isCrossClass;
            Name = name;
        }

        public string Name { get; set; }
        public Crafter CrafterAfinity { get; set; }
        public bool IsCrossClass { get; set; }
        public int CpCost { get; set; }
    }
}