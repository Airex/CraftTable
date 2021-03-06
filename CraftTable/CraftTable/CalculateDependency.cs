using CraftTable.Contracts;

namespace CraftTable
{
    public delegate T CalculateDependency<out T>(IBuffAccessor buffAccessor, ILookupService lookupService, Recipe recipe)
        where T : struct;
}