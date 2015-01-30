using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fzrain.Core.Domain.Permission;

namespace Fzrain.Data.Mapping.Permission
{
    class RoleMap:EntityTypeConfiguration<Role>
    {
        public RoleMap()
        {
            HasKey(r => r.Id);
            Property(r => r.RoleName).HasMaxLength(50);
            HasMany(r => r.Users).WithMany(u => u.Roles).Map(c => c.ToTable("User_R_Role"));
        }
    }
}
