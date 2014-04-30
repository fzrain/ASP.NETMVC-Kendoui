using System;
using System.Linq;
using Fzrain.Core.Data;
using Fzrain.Core.Domain;
using Fzrain.Data.Initializers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Autofac;

namespace Fzrain.Data.Test
{
    [TestClass]
    public class RepositoryTest
    {
        [TestMethod]
        public void TestRepository()
        {

            var builder = new ContainerBuilder();
            DependencyRegistrar.Register(builder);
            var container = builder.Build();
           var userRepository= container.Resolve<IRepository<User>>();
            var query = from u in userRepository.Table
                where u.UserName == "admin"
                select u;
            Assert.AreEqual(query.SingleOrDefault().Password ,"123");

        }
   
    }
}
