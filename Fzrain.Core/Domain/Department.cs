using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fzrain.Core.Domain
{
   public  class Department:BaseEntity
    {
       public string  DepartmentName { get; set; }

       public virtual  Department  ParentDpt { get; set; }

       public virtual ICollection<Department> ChildrenDpt { get; set; }
       public virtual ICollection<User> Users { get; set; }
    }
}
