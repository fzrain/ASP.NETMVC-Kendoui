using System.Linq;
using Autofac;
using Autofac.Core;
using Fzrain.Core.Data;
using Fzrain.Core.Domain;
using Fzrain.Data.Initializers;
using Fzrain.Web.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fzrain.Data.Test
{
    [TestClass]
    public class AutofacTest
    {
     
        // [TestMethod]
        //public void TestResolve()
        //{
        ////  DependencyRegistrar.Register();
        //     var builder = new ContainerBuilder();
        //    DependencyRegistrar.Register(builder);
        //     var container = builder.Build();
        //   var db = container.Resolve<IDbContext>();
        //    Assert.IsNotNull(db);
        //}
         [TestMethod]
         public void SimpleReg()
         {
             var cb = new ContainerBuilder();
             cb.RegisterType<FzrainContext>().As<IDbContext>();
             var c = cb.Build();
             //带参数的ctor
             var a = c.Resolve<IDbContext>(new NamedParameter("nameOrConnectionString", "fzrain"));
             Assert.IsNotNull(a);
             Assert.IsInstanceOfType(a, typeof(FzrainContext));
         }

        [TestMethod]
        public void IocDi()
        {
            var cb = new ContainerBuilder();
            cb.RegisterType<B>();
            cb.RegisterType<A>();
            var c = cb.Build();
            //带参数的ctor
            var a = c.Resolve<B>();
            Assert.IsNotNull(a.A);
        }
        //[TestMethod]
        //public void RepositoryDi()
        //{
        //    var builder = new ContainerBuilder();
        //    DependencyRegistrar.Register(builder);
        //    var container = builder.Build();
        //    var userRepository= container.Resolve<IRepository<User>>();
        //    Assert.IsNotNull(userRepository .Table);
        //}
    }
    class A   { }
    class B
    {
        public A A { get; set; }
        public B(A a)
        {
            this.A = a;
        }
    }
}
