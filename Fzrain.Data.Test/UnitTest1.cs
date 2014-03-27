using System;
using Fzrain.Data.Initializers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fzrain.Data.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            CreateDatabase.Initialize();
        }
    }
}
