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
            Database.SetInitializer(new FzrainDropCreateDatabaseIfModelChanges ());
            var builder = new ContainerBuilder();
            //DbContext db = new FzrainContext("fzrain");
          DependencyRegistrar.Register(builder);
          var container = builder.Build();
            var db = (FzrainContext)container.Resolve<IDbContext>();
            db.Database.Initialize(true);
           
       
        }
       

      

       
    }
}
