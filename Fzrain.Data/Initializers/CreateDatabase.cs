using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Fzrain.Core.Domain;

namespace Fzrain.Data.Initializers
{
    public class CreateDatabase 
    {
        public static void Initialize()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<FzrainContext>());
          
            //DbContext db = new FzrainContext("fzrain");
            var container = DependencyRegistrar.Register();
            var db = container.Resolve<IDbContext>(new NamedParameter("nameOrConnectionString", "fzrain"));
            if (!db.Set<User>().Any())
            {
                new FrameDataSeeder(db);
            }
            //new CreateDatabase().Seed((FzrainContext)db);
       
        }

       
    }
}
