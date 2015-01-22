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
           [TestMethod]
        public void GetChampionContribute()
        {
            EngineContext.Initialize(false);
         
            var battleRepositpry = EngineContext.Current.Resolve<IRepository<Battle>>();
            var recordRepositpry = EngineContext.Current.Resolve<IRepository<Record>>();
            //var champions = from r in recordRepositpry.Table
            //    group r by r.ChampionId
            //    into g
            //    select g.Key;
            //foreach ( int champion in champions)
            //{
              
            //}
            int championId = 1;
            var records = recordRepositpry.Table.Where(r => r.ChampionId == championId).IncludeProperties(r=>r.Battle);
            List<double> ratioList = new List<double>();
            foreach (Record record in records)
            {
                 List<Record> list= battleRepositpry.Table.Where(b => b.Id == record.Battle.Id).First().Records.Where (r=>r.IsWin ==record.IsWin).ToList ();

              double damageRatio = Math.Round((double)record.TotalDamage / list.Sum(r => r.TotalDamage),2);
              double killRatio =  Math.Round((double)record.Kill  / list.Sum(r => r.Kill),2);
              double deathRatio =  Math.Round((double)record.Death  / list.Sum(r => r.Death),2);
              double assistRatio = Math.Round((double)record.Assist / list.Sum(r => r.Kill), 2);
            
            }
        }
    }
}
