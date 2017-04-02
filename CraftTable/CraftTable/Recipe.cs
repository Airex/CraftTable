namespace CraftTable
{
    public sealed class Recipe
    {
        public Recipe(int difficulty, int durability, int maxQuality, int level = 0)
        {
            Difficulty = difficulty;
            Durability = durability;
            MaxQuality = maxQuality;
            Level = level;

        }

        public int Durability { get; }
        public int MaxQuality { get; }
        public int Difficulty { get; }
        public int Level { get; }
        public int StartQuality { get; set; }
    }
}