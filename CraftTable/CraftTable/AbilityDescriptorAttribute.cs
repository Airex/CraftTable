using System;

namespace CraftTable
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AbilityDescriptorAttribute : Attribute
    {
        public string Name { get; set; }
        public Crafter CrafterAfinity { get; set; }
        public bool IsCrossClass { get; set; }
    }
}