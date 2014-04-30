using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Autofac;

namespace Fzrain.Web.Framework.Mvc
{
   public  class FzrainDependencyResolver : IDependencyResolver
    {
      private   ContainerBuilder builder;
       private IContainer container;
       public FzrainDependencyResolver(ContainerBuilder builder,IContainer container)
       {
           this.builder = builder;
           this.container = container;
          
       }
        public object GetService(Type serviceType)
        {
          return   container.ResolveOptional(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            var type = typeof(IEnumerable<>).MakeGenericType(serviceType);
            return (IEnumerable<object>)container.Resolve(type);
        }
    }
}
