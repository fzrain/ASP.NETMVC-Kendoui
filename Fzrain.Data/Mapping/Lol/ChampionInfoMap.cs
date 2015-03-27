using System.Data.Entity.ModelConfiguration;
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
