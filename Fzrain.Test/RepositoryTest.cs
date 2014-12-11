using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using Autofac.Integration.Mvc;
using Fzrain.Core.Configuration;
using Fzrain.Core.Data;
using Fzrain.Core.Domain;
using Fzrain.Core.Infrastructure;
using Fzrain.Core.Infrastructure.DependencyManagement;
using Fzrain.Data.Initializers;
using Fzrain.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Autofac;

namespace Fzrain.Test
{
    [TestClass]
    public class RepositoryTest
    {
        [TestMethod]
        public void TestRepository()
        {       
            EngineContext.Initialize(false);
            var userRepository = EngineContext.Current.Resolve<IRepository<User>>();
            var query = from u in userRepository.Table
                where u.UserName == "admin"
                select u;
            Assert.AreEqual(query.SingleOrDefault().Password ,"11111");

        }
        [TestMethod]
        public void TestEngineInit()
        {
            //FzrainEngine fzrainEngine = new FzrainEngine();
            //Type t = typeof (FzrainEngine);
            //t.GetMethod("RegisterDependencies",System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).Invoke(fzrainEngine,new object[]{null });
            var builder = new ContainerBuilder();
            var container = builder.Build();

            //we create new instance of ContainerBuilder
            //because Build() or Update() method can only be called once on a ContainerBuilder.


            //dependencies
            var config = ConfigurationManager.GetSection("FzrainConfig") as FzrainConfig;
            FzrainEngine engine = new FzrainEngine();
            var typeFinder = new WebAppTypeFinder(config);
            builder = new ContainerBuilder();
            builder.RegisterInstance(config).As<FzrainConfig>().SingleInstance();
            builder.RegisterInstance(engine).As<IEngine>().SingleInstance();
            builder.RegisterInstance(typeFinder).As<ITypeFinder>().SingleInstance();
            builder.Update(container);

            //register dependencies provided by other assemblies
            builder = new ContainerBuilder();
            var drTypes = typeFinder.FindClassesOfType<IDependencyRegistrar>();
            var drInstances = new List<IDependencyRegistrar>();
            foreach (var drType in drTypes)
                drInstances.Add((IDependencyRegistrar)Activator.CreateInstance(drType));
            //sort
            drInstances = drInstances.AsQueryable().OrderBy(t => t.Order).ToList();
            foreach (var dependencyRegistrar in drInstances)
                dependencyRegistrar.Register(builder, typeFinder);
            builder.Update(container);


         //   this._containerManager = new ContainerManager(container);

            //set dependency resolver
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

        }

        [TestMethod]
        public void TestService()
        {

            //var builder = new ContainerBuilder();
            //DependencyRegistrar.Register(builder);
            //var container = builder.Build();
            EngineContext.Initialize(false);
            var userService = EngineContext.Current.Resolve<IUserService>();
           var user= userService.GetAllUsers().FirstOrDefault ();
            Assert.AreEqual(user.UserName, "admin");

        }
    }
}
