using System;

namespace Fzrain.Core.Domain.Permission
{
   public  class User:BaseEntity 
    {
   
       public string UserName { get; set; }
       public string Password { get; set; }
       public string  DisplayName { get; set; }
       public DateTime Birthday { get; set; }
       public Gender Gender { get; set; }

       public virtual  Department  Department { get; set; }

    }
  public  enum Gender
   {
       Male,
       Female
   }
}
