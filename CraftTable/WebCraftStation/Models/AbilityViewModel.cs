namespace WebCraftStation.Models
{
    public class AbilityViewModel
    {
        public string Name { get; set; }
        public string XivDbId { get; set; }
        public bool IsEnabled { get; set; }
        public int CraftPointsCost { get; set; }
        public bool IsHighLigthed { get; set; }
        public string Category { get; set; }
        public int Order { get; set; }
    }

    public class BuffViewModel
    {
        public string Name { get; set; }
        public string XivDbId { get; set; }
        public int Stacks { get; set; }
        public int Steps { get; set; }
    }
}