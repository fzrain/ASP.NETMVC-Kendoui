using System;
using System.Linq;
using System.Reflection;
using Fzrain.Core;
using Fzrain.Core.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fzrain.Test.Core
{
    [TestClass]
    public class TypeFinder
    {
        [TestMethod]
        public void TestMethod1()
        {
            //EngineContext.Initialize(false);
            //var typeFinder = EngineContext.Current.Resolve<ITypeFinder>();
            //var entities = typeFinder.FindClassesOfType<BaseEntity>();
           var assembly= Assembly.LoadFile("E:/我的框架/FzrainFrameWork/lib/Fzrain.Core.dll");
          var t=  assembly.GetTypes().Where(type => type.BaseType != null && type.BaseType.Name == "BaseEntity");
         
        }
    }
}
