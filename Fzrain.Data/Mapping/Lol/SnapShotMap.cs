using System.Data.Entity.ModelConfiguration;
using Fzrain.Core.Domain.Lol;

namespace Fzrain.Data.Mapping.Lol
{
    class SnapShotMap: EntityTypeConfiguration<SnapShot>
    {
        public SnapShotMap()
        {
            ToTable("Lol_SnapShot");
            HasKey(b => b.Id);
        }
    }
}
