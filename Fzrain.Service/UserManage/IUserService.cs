using System.Collections.Generic;
using Fzrain.Core.Domain.Permission;

namespace Fzrain.Service.UserManage
{
   public  interface  IUserService
   {
       List<Role> GetAllRoles();
       List<Role> GetRoles(int userId);
       void SetRoles(long userId,List<long > roleIds);

       List<Module> GetAllModules();
       List<Module> GeModules(int userId);
       void SetModules(long userId, List<long> moduleIds);
   }
}
