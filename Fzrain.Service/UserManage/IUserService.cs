using System.Linq;
using Fzrain.Core.Domain.Permission;

namespace Fzrain.Service.UserManage
{
   public  interface  IUserService
   {
       User GetById(int id);
        void InsertUser(User user);
        void UpdateUser(User user);
        void DeleteUser(User user);
        IQueryable<User> GetAllUsers();
   }
}
