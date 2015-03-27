using System.Data.Entity.Migrations;
using Fzrain.Core.Domain.Permission;

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
