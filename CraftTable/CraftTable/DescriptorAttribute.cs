using System;
using System.Drawing;
using System.IO;

namespace CraftTable
{
    [Flags]
    public enum Crafter
    {
        Culinarian,
        Alchemist,
        All = Culinarian | Alchemist
    }

    public class DescriptorAttribute : Attribute
    {
        public  Crafter CrafterAfinity { get; set; }
    }
}