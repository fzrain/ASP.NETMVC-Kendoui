using System.Collections.Generic;
using System.Linq;
using Fzrain.Core.Data;
using Fzrain.Core.Domain.Permission;

namespace Fzrain.Service.UserManage
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> userRepository;
        private readonly IRepository<Role> roleRepository;

        public UserService(IRepository<User> userRepository, IRepository<Role> roleRepository)
        {
            this.userRepository = userRepository;
            this.roleRepository = roleRepository;
        }

        public List<Role> GetRoles()
        {
          return   roleRepository.Table.ToList();
        }

        public void SetRoles(int userId, List<int> roleIds)
        {
            User user=  userRepository.GetById(userId);
            user.Roles = roleRepository.Table.Where(r => roleIds.Contains(r.Id)).ToList();
            userRepository.Update(user);
        }
    }
}
