using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using Fzrain.Data.Initializers;
using Fzrain.Web.Framework.Mvc;

namespace Fzrain.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            CreateDatabase.Initialize();
            var build = new ContainerBuilder();
            Fzrain.Data.DependencyRegistrar.Register(build);
            build.RegisterControllers(Assembly.GetExecutingAssembly());
         IContainer container=   Fzrain.Web.Framework.DependencyRegistrar.Register(build);
       //  DependencyResolver.SetResolver(new FzrainDependencyResolver(build, container));
         DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
           
        }
    }
}
