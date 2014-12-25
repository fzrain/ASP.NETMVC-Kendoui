using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fzrain.Web.Models.Lol
{
    public class ChampionGrowupViewModel
    {
        public int ChampionId { get; set; }
        public int GameId { get; set; }
        public string StartTime { get; set; }
        public double Proficiency { get; set; }
    }
}