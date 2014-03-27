using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fzrain.Core.Domain;

namespace Fzrain.Data.Initializers
{
    public class CreateDatabase
    {
        public static void Initialize()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<FzrainContext>());
            DbContext db = new FzrainContext("fzrain");
            //DependencyRegistrar.Register();
            //var scope = DependencyRegistrar.Container.BeginLifetimeScope();
            
            db.Set<User>().Add(new User {UserName = "admin", Password = "123"});
            db.SaveChanges();

        }
    }
}
