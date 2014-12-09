using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fzrain.Core.Domain;

namespace Fzrain.Data.Initializers
{
   internal sealed class Configuration : DbMigrationsConfiguration<FzrainContext>
   {
       public Configuration()
       {
           AutomaticMigrationsEnabled = true;
           AutomaticMigrationDataLossAllowed = true;
       }
       protected override void Seed(FzrainContext context)
       {
           context.Set<User>().AddOrUpdate(u => u.UserName, new User { UserName = "admin", Password = "11111" });

           context.SaveChanges();
       }

   }
}
