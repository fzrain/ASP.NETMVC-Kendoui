using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fzrain.Core.Domain;

namespace Fzrain.Data.Mapping
{
    class DepartmentMapping : EntityTypeConfiguration<Department>
    {
        public DepartmentMapping()
        {
            HasKey(d => d.Id);
            HasOptional(d => d.ParentDpt).WithMany (d=>d.ChildrenDpt);        
            Property(d => d.DepartmentName).IsRequired();
        }
    }
}
