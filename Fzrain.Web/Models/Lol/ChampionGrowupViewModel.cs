using System;

namespace Fzrain.Web.Models.Lol
{
    public class ChampionGrowupViewModel
    {
        public int ChampionId { get; set; }
        public int GameId { get; set; }
        public DateTime StartTime { get; set; }
        public double? Proficiency { get; set; }
    }
}