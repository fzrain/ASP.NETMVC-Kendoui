using System.Data.Entity.ModelConfiguration;
using Fzrain.Core.Domain.Permission;

namespace Fzrain.Data.Mapping.Permission
{
    public  class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            HasKey(u => u.Id);
            Property(u => u.UserName).IsRequired().HasMaxLength(50);
            Property(u => u.Password).IsRequired().HasMaxLength(50);
            Property(u => u.Birthday).HasColumnType("datetime2");
            HasOptional(u => u.Department).WithMany(d=>d.Users);
         
        }
    }
}
