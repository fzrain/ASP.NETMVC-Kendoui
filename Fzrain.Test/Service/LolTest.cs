using System;
using System.Collections.Generic;
using System.Linq;
using Fzrain.Core.Data;
using Fzrain.Core.Domain;
using Fzrain.Core.Domain.Lol;
using Fzrain.Core.Infrastructure;
using Fzrain.Service.Lol;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fzrain.Data;

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

        [TestMethod]
        public void UpdateBattle()
        {
            EngineContext.Initialize(false);
            var battleRepositpry = EngineContext.Current.Resolve<IRepository<Battle>>();
            var recordRepositpry = EngineContext.Current.Resolve<IRepository<Record>>();
          var myRecords=  recordRepositpry.Table.Where(r => r.Name == "笨笨秒杀上帝").IncludeProperties(r=>r.Battle).ToList();
            foreach (var record in myRecords)
            {
             var battle=   battleRepositpry.GetById(record.Battle.Id);
                battle.ChampionId = record.ChampionId;
                battle.IsWin = record.IsWin;
                battleRepositpry.Update(battle);
            }
            Assert.AreNotEqual(1, 100);
        }
    }
}
