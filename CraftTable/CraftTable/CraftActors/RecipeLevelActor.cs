using CraftTable.Contracts;

namespace CraftTable.CraftActors
{
    public delegate void RecipeLevelActor(int recipeLevel, ICalculatorActor action, ILookupService lookupService);
}