using System.Data.Entity.ModelConfiguration;
using Fzrain.Core.Domain.Permission;

namespace Fzrain.Data.Mapping.Permission
{
    class DepartmentMap : EntityTypeConfiguration<Department>
    {
        public DepartmentMap()
        {
            HasKey(d => d.Id);
            HasOptional(d => d.ParentDpt).WithMany (d=>d.ChildrenDpt);        
            Property(d => d.DepartmentName).IsRequired();
            
        }
    }
}
