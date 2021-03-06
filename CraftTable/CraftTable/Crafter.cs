using System;

namespace CraftTable
{
    [Flags]
    public enum Crafter
    {
        Culinarian = 1,
        Alchemist = 2,
        GoldSmith =4,
        Weaver =8,
        Leatherworker =16,
        Armorer = 32,
        BlackSmith = 64,
        Carpenter = 128,
        All = Culinarian | Alchemist | GoldSmith | Weaver | Leatherworker | Armorer | BlackSmith | Carpenter,
    }
}