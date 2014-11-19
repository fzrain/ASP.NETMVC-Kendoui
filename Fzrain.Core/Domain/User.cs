using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fzrain.Core.Domain
{
   public   class User:BaseEntity 
    {
       public string  UserName { get; set; }
       public string Password { get; set; }
       public DateTime  Birthday { get; set; }
       public Gender Gender { get; set; }
       public string  Email { get; set; }
       public string  MobilePhone { get; set; }
       public bool IsPublicMobile { get; set; }
    }
  public  enum Gender
   {
       Male,
       Female
   }
}
