using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fzrain.Web.Models.Lol
{
    public class LolChampionInfoViewModel
    {
        public int ChampionId { get; set; }
        public int MyApprance { get; set; }
        public int MyWinCount { get; set; }
        public int TotalApprance { get; set; }
        public int TotalWinCount { get; set; }

    }
}