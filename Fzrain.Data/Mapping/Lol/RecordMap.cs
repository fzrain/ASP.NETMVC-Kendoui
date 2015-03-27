using System.Data.Entity.ModelConfiguration;
using Fzrain.Core.Domain.Lol;

namespace Fzrain.Data.Mapping.Lol
{
   public  class RecordMap : EntityTypeConfiguration<Record>
    {
       public RecordMap()
       {
           ToTable("Lol_Record");
           HasKey(r => r.Id);
           HasRequired(r => r.Battle).WithMany(b => b.Records).WillCascadeOnDelete (true);
       }
    }
}
