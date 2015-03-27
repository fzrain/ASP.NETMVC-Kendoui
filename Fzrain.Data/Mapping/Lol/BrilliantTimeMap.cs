using System.Data.Entity.ModelConfiguration;
using Fzrain.Core.Domain.Lol;

namespace Fzrain.Data.Mapping.Lol
{
    class BrilliantTimeMap : EntityTypeConfiguration<BrilliantTime>
    {
        public BrilliantTimeMap()
        {
            ToTable("Lol_BrilliantTime");
            HasKey(b => b.Id);
            HasOptional(b => b.Battle).WithMany(b => b.BrilliantTimes);
        }
    }
}
