using System.Collections.Generic;
using System.Linq;
using Fzrain.Core.Domain.Permission;

namespace Fzrain.Service.UserManage
{
   public  interface  IUserService
   {
       List<Role> GetAllRoles();
       List<Role> GetRoles(int userId);
       void SetRoles(int userId,List<int> roleIds);
   }
}
