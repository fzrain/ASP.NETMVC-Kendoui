using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fzrain.Core.Domain;

namespace Fzrain.Data.Mapping
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
