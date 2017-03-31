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
}