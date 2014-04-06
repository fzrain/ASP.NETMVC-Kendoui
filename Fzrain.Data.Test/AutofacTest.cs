using System;
using Autofac;
using Autofac.Core;
using Fzrain.Data.Initializers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fzrain.Data.Test
{
    [TestClass]
    public class AutofacTest
    {
        [TestMethod]
        public void InitDataBase()
        {
            CreateDatabase.Initialize();
        }
         [TestMethod]
        public void TestResolve()
        {
          DependencyRegistrar.Register();
           var container=  DependencyRegistrar.Register();
           var db = container.Resolve<IDbContext>(new NamedParameter("nameOrConnectionString", "fzrain"));
            Assert.IsNotNull(db);
        }
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
