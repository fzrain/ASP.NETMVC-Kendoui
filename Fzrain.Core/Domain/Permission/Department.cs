using System.Collections.Generic;

namespace Fzrain.Core.Domain.Permission
{
   public  class Department:BaseEntity
    {
       public string  DepartmentName { get; set; }

       public virtual  Department  ParentDpt { get; set; }

       public virtual ICollection<Department> ChildrenDpt { get; set; }
       public virtual ICollection<User> Users { get; set; }
       public virtual ICollection<Module> Modules { get; set; }
    }
}
