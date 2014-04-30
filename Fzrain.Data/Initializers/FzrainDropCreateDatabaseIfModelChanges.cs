using Fzrain.Core.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fzrain.Data.Initializers
{
    public class FzrainDropCreateDatabaseIfModelChanges : DropCreateDatabaseIfModelChanges<FzrainContext >
    {
        protected override void Seed(FzrainContext context)
        {
            context.Set<User>().Add(new User { UserName = "admin", Password = "123" });
            context.Set<User>().Add(new User { UserName = "tom", Password = "123" });
            context.SaveChanges();
        }
    }
}
