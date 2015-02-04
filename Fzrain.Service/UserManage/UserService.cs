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
    }
}
