using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fzrain.Core.Domain.Permission
{
   public  class Module:BaseEntity
    {
       public string  ModuleName { get; set; }
       public string  Controller { get; set; }
       public string  Action { get; set; }
       public int Order { get; set; }
       public virtual Module  ParentModule { get; set; }
       public virtual ICollection<Module> ChildrenModules { get; set; }
       public virtual ICollection<User> Users { get; set; }
       public virtual ICollection<Department> Departments { get; set; }
       public virtual ICollection<Role> Roles { get; set; }
    }
}
