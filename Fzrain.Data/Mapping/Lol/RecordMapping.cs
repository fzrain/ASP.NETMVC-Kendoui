using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fzrain.Core.Domain.Lol;

namespace Fzrain.Data.Mapping.Lol
{
   public  class RecordMapping : EntityTypeConfiguration<Record>
    {
       public RecordMapping()
       {
           ToTable("Lol_Record");
           HasKey(r => r.Id);
           HasRequired(r => r.Battle).WithMany(b => b.Records).WillCascadeOnDelete (true);
       }
    }
}
