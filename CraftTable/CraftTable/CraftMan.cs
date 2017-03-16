namespace CrafterExperiment
{
    public class CraftMan
    {
        public CraftMan(int control, int craftmanship, int maxCraftPoints, int level = 0)
        {
            Control = control;
            Craftmanship = craftmanship;
            MaxCraftPoints = maxCraftPoints;
            Level = level;
        }

        public int Craftmanship { get; set; }
        public int Control { get; set; }
        public int MaxCraftPoints { get; set; }
        public int Level { get; }
    }
}