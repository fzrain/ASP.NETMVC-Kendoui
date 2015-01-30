using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fzrain.Core.Domain.Lol;

namespace Fzrain.Data.Mapping.Lol
{
    class ChampionInfoMap : EntityTypeConfiguration<ChampionInfo>
    {
        public ChampionInfoMap()
        {
            ToTable("Lol_ChampionInfo");
            HasKey(b => b.Id);
           
        }
    }
}
