using System.Data.Entity.ModelConfiguration;
using Fzrain.Core.Domain.Logging;

namespace Fzrain.Data.Mapping.Logging
{
    public partial class LogMap : EntityTypeConfiguration<Log>
    {
        public LogMap()
        {
          
            this.HasKey(l => l.Id);
            this.Property(l => l.ShortMessage).IsRequired();
            this.Property(l => l.IpAddress).HasMaxLength(200);

            this.Ignore(l => l.LogLevel);

            this.HasOptional(l=>l.User)
                .WithMany()
                .HasForeignKey(l => l.UserId)
            .WillCascadeOnDelete(true);

        }
    }
}
