using CraftTable.Contracts;

namespace CraftTable.CraftActors
{
    public delegate void RecipeLevelActor(int recipeLevel, int craftManLevel, ICalculatorActor action, ILookupService lookupService);
}