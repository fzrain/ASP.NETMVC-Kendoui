using System.Data.Entity.ModelConfiguration;
using Fzrain.Core.Domain.Configuration;

namespace Fzrain.Data.Mapping.Configuration
{
    public partial class SettingMap : EntityTypeConfiguration<Setting>
    {
        public SettingMap()
        {          
            this.HasKey(s => s.Id);
            this.Property(s => s.Name).IsRequired().HasMaxLength(200);
            this.Property(s => s.Value).IsRequired().HasMaxLength(2000);
        }
    }
}
