using System;
using System.Collections.Generic;
using System.Linq;
using Fzrain.Core.Data;
using Fzrain.Core.Domain;
using Fzrain.Core.Domain.Lol;
using Fzrain.Core.Infrastructure;
using Fzrain.Service.Lol;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fzrain.Test.Service
{
    [TestClass]
    public class LolTest
    {
        [TestMethod]
        public void AddData()
        {
            EngineContext.Initialize(false);
            var lolService= EngineContext.Current.Resolve<ILolService>();
           // lolService.InitRecord("../../../Fzrain.Service/Lol/id.txt");
         var query =   lolService.GetAllBattles();
          //  List<Record> query = lolService.GetRecordsByName("网络中断突然").ToList ();
         Assert.AreNotEqual(query.Count (), 100);
        }
    }
}
