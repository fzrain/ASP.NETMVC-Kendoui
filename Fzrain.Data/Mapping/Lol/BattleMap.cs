using System.Data.Entity.ModelConfiguration;
using Fzrain.Core.Domain.Lol;


namespace Fzrain.Data.Mapping.Lol
{
    class BattleMap : EntityTypeConfiguration<Battle>
    {
        public BattleMap()
        {
            ToTable("Lol_Battle");
            HasKey(b => b.Id);
            Property(b => b.StartTime).HasColumnType("datetime2");
        }
    }
}
