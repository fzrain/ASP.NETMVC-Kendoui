using System.Collections.Generic;
using System.Linq;
using Fzrain.Core.Data;
using Fzrain.Core.Domain.Permission;
using Fzrain.Data;

namespace Fzrain.Service.UserManage
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> userRepository;
        private readonly IRepository<Role> roleRepository;
        private readonly IRepository<Module> moduleRepository;

        public UserService(IRepository<User> userRepository, IRepository<Role> roleRepository, IRepository<Module> moduleRepository)
        {
            this.userRepository = userRepository;
            this.roleRepository = roleRepository;
            this.moduleRepository = moduleRepository;
        }
        /// <summary>
        /// 过滤权限
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<Module> GetUserAccessModules(int userId)
        {
            var user = userRepository.Table.Where(u => u.Id == userId).IncludeProperties(u => u.Roles).FirstOrDefault();
           return  user.Modules.ToList();
        }

        public List<Role> GetAllRoles()
        {
          return   roleRepository.Table.ToList();
        }

        public List<Role> GetRoles(int userId)
        {
           var user= userRepository.Table.Where(u => u.Id == userId).IncludeProperties(u => u.Roles).FirstOrDefault();
            return  user.Roles.ToList();
        }

        public void SetRoles(long userId, List<long> roleIds)
        {
            var user=  userRepository.Table.Where(u=>u.Id==userId).IncludeProperties(u=>u.Roles).FirstOrDefault();
            user.Roles = roleRepository.Table.Where(r => roleIds.Contains(r.Id)).ToList();
            userRepository.Update(user);
        }

        public List<Module> GetAllModules()
        {
            return moduleRepository.Table.ToList();
        }

        public List<Module> GeModules(int userId)
        {
            var user = userRepository.Table.Where(u => u.Id == userId).IncludeProperties(u => u.Modules).FirstOrDefault();
            return user.Modules.ToList();
        }

        public void SetModules(long userId, List<long> moduleIds)
        {
            var user = userRepository.Table.Where(u => u.Id == userId).IncludeProperties(u => u.Modules).FirstOrDefault();
            user.Modules = moduleRepository.Table.Where(m => moduleIds.Contains(m.Id)).ToList();
            userRepository.Update(user);
        }
    }
}
