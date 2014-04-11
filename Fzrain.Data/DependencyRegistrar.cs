using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core;
using Fzrain.Core.Data;
using Fzrain.Core.Domain;

namespace Fzrain.Data
{
    public  class DependencyRegistrar
    {
     //   public  static IContainer Container { get; set; }
        public static IContainer  Register()
        {
            var builder = new ContainerBuilder();           
            builder.RegisterType<FzrainContext>().As<IDbContext>().WithParameter(new NamedParameter("nameOrConnectionString", "fzrain")) ;
            builder.RegisterType<EfRepository<User>>().As<IRepository<User>>();
         return  builder.Build();
        }
    }
}
