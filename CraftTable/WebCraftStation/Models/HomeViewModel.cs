using System.Collections.Generic;
using System.Reflection.Emit;
using System.Web.WebPages;

namespace WebCraftStation.Models
{
    public class HomeViewModel
    {
        public IList<AbilityViewModel> Abilities { get; set; }
        public StatsViewModel Stats { get; set; }
        public IList<BuffViewModel> Buffs { get; set; }
        public string Message { get; set; }
        public IList<LogViewModel> Logs { get; set; }
    }

    public class LogViewModel
    {
        public string Text { get; set; }
    }

    public class StatsViewModel
    {
        public int Step { get; set; }
        public int Durability { get; set; }
        public int Progress { get; set; }
        public int Quality { get; set; }
        public string Condition { get; set; }
        public int MaxDurability { get; set; }
        public int MaxQuality { get; set; }
        public int MaxProgress { get; set; }
        public int CraftPoints { get; set; }
        public int MaxCraftPoints { get; set; }
        public int HighQualityChance { get; set; }
    }
}