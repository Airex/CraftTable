using CraftTable.Contracts;

namespace CraftTable
{
    public delegate T CalculateDependency<out T>(IBuffAccessor buffAccessor, ILookupService lookupService)
        where T : struct;
}