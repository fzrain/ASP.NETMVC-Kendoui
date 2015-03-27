using System.Data.Entity.ModelConfiguration;
using Fzrain.Core.Domain.Permission;

namespace Fzrain.Data.Mapping.Permission
{
    class ModuleMap : EntityTypeConfiguration<Module>
    {
        public ModuleMap()
        {
            HasKey(m => m.Id);
            Property(m => m.ModuleName).HasMaxLength(50);
            HasOptional(m => m.ParentModule).WithMany(d => d.ChildrenModules);   
            HasMany(m => m.Users).WithMany(u => u.Modules).Map(c => c.ToTable("User_R_Module"));
            HasMany(m => m.Roles).WithMany(r => r.Modules).Map(c => c.ToTable("Role_R_Module"));
            HasMany(m => m.Departments).WithMany(d => d.Modules).Map(c => c.ToTable("Department_R_Module"));
        }
    }
}
