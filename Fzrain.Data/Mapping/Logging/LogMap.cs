using System.Data.Entity.ModelConfiguration;
using Fzrain.Core.Domain.Logging;

namespace Fzrain.Data.Mapping.Logging
{
    public partial class LogMap : EntityTypeConfiguration<Log>
    {
        public LogMap()
        {
          
            this.HasKey(l => l.Id);
            this.Property(l => l.Message).IsRequired();
            this.HasOptional(l=>l.User)
                .WithMany();

        }
    }
}
