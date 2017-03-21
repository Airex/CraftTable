using System;

namespace CraftTable.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AbilityDescriptorAttribute : Attribute
    {
        public AbilityDescriptorAttribute(string name, Crafter crafterAfinity, int cpCost, bool isCrossClass, Category category, int order)
        {
            CpCost = cpCost;
            CrafterAfinity = crafterAfinity;
            IsCrossClass = isCrossClass;
            Category = category;
            Order = order;
            Name = name;
        }

        public string Name { get; set; }
        public Crafter CrafterAfinity { get; set; }
        public bool IsCrossClass { get; set; }
        public int CpCost { get; set; }
        public Category Category { get; set; }
        public int Order { get; set; }

    }

    public enum Category
    {
        Synhtesis,
        Quality,
        CP,
        Durability,
        Buffs,
        Specialist,
        Other
    }
}