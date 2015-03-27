using System;
using System.Collections.Generic;

namespace Fzrain.Core.Domain.Permission
{
   public  class User:BaseEntity 
    {
   
       public string UserName { get; set; }
       public string Password { get; set; }
       public string  DisplayName { get; set; }
       public DateTime Birthday { get; set; }
       public Gender Gender { get; set; }
       public string MobilePhone { get; set; }
       public bool IsPublic { get; set; }
       public byte[] Photo { get; set; }

       public virtual  Department  Department { get; set; }
       public virtual ICollection<Role > Roles { get; set; }
       public virtual ICollection<Module> Modules { get; set; }
    }
  public  enum Gender
   {
       Male,
       Female
   }
}
