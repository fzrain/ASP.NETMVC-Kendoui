using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fzrain.Core.Domain.Lol;


namespace Fzrain.Data.Mapping.Lol
{
    class BattleMapping : EntityTypeConfiguration<Battle>
    {
        public BattleMapping()
        {
            ToTable("Lol_Battle");
            HasKey(b => b.Id);
            Property(b => b.StartTime).HasColumnType("datetime2");
        }
    }
}
