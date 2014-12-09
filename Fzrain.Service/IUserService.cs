using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fzrain.Core;
using Fzrain.Core.Domain;

namespace Fzrain.Service
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
