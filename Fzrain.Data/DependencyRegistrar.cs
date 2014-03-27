using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core;

namespace Fzrain.Data
{
    public  class DependencyRegistrar
    {
        public  static IContainer Container { get; set; }
        public static void Register()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<FzrainContext>().As<IDbContext>();          
            Container = builder.Build();
        }
    }
}
