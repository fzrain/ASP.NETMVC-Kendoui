using Autofac;
using Fzrain.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fzrain.Web.Framework
{
    public class DependencyRegistrar
    {
        public static IContainer Register(ContainerBuilder builder)
        {
          //  var builder = new ContainerBuilder();
            builder.RegisterType<UserService>().As<IUserService>();
            return builder.Build();
        }
    }
}
