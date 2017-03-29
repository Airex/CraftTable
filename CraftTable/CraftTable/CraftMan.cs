namespace CraftTable
{
    public class CraftMan
    {
        public CraftMan(Crafter crafter, int control, int craftmanship, int maxCraftPoints, int level = 0)
        {
            Crafter = crafter;
            Control = control;
            Craftmanship = craftmanship;
            MaxCraftPoints = maxCraftPoints;
            Level = level;
        }

        public Crafter Crafter { get; set; }
        public int Craftmanship { get; set; }
        public int Control { get; set; }
        public int MaxCraftPoints { get; set; }
        public int Level { get; }
    }
}