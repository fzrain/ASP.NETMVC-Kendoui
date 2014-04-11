using System;
using System.Linq;
using Fzrain.Core.Data;
using Fzrain.Core.Domain;
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
            var container = DependencyRegistrar.Register();
           var userRepository= container.Resolve<IRepository<User>>();
            var query = from u in userRepository.Table
                where u.UserName == "admin"
                select u;
            Assert.AreEqual(query.SingleOrDefault().Password ,"123");

        }
    }
}
