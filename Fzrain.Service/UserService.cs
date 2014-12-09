using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fzrain.Core;
using Fzrain.Core.Data;
using Fzrain.Core.Domain;

namespace Fzrain.Service
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> userRepository;

        public UserService(IRepository<User> userRepository)
        {
            this.userRepository = userRepository;
        }

        public User GetById(int id)
        {
            return userRepository.GetById(id);
        }

        public void InsertUser(User user)
        {
            userRepository.Insert(user);
        }

        public void UpdateUser(User user)
        {
            userRepository.Update(user);
        }

        public void DeleteUser(User user)
        {
            userRepository.Delete(user);
        }

        public IQueryable<User> GetAllUsers()
        {
         
            return userRepository.Table;
        }
    }
}
