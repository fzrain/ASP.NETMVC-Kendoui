using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fzrain.Core.Domain.Lol
{
  public   class Battle:BaseEntity 
    {
        public int BattleType { get; set; }
        public int GameId { get; set; }
        public DateTime StartTime { get; set; }
        public int Duration { get; set; }
        public int IsWin { get; set; }
        public int ChampionId { get; set; }
        public int ContributeOrder { get; set; }

       public virtual  ICollection<Record> Records { get; set; }
       public virtual ICollection<BrilliantTime> BrilliantTimes { get; set; }
    }
}
