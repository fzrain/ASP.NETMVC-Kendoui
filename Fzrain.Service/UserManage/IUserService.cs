using System.Collections.Generic;
using System.Linq;
using Fzrain.Core.Domain.Permission;

namespace Fzrain.Service.UserManage
{
   public  interface  IUserService
   {
       List<Role> GetRoles();
   }
}
