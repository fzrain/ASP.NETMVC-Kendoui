using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Fzrain.Core.Data;
using Fzrain.Core.Domain.Lol;

namespace Fzrain.Service.Lol
{
    public class LolService : ILolService
    {
        private readonly IRepository<Battle> battleRepository;
        private readonly IRepository<Record > recordRepository;

        public LolService(IRepository<Battle> battleRepository, IRepository<Record> recordRepository)
        {
            this.battleRepository = battleRepository;
            this.recordRepository = recordRepository;
        }
        public void UpdateBattle(List<int> ids,int areaId)
        {       
          
            foreach (var id in ids)
            {
                Thread.Sleep(5000);
                var b = LolCommonHelper.GetDataById(Convert.ToInt32(id), areaId);
                b.ChampionId = b.Records.Where(r => r.Name == "网络中断突然").FirstOrDefault().ChampionId;
                b.IsWin = b.Records.Where(r => r.Name == "网络中断突然").FirstOrDefault().IsWin;
                battleRepository.Insert(b);


            }
        }

        public IQueryable<Battle> GetAllBattles()
        {
          return  battleRepository.Table;
        }

        public void InitRecord(string filePath)
        {
            var ids = File.ReadAllLines(filePath, Encoding.UTF8).ToList().Distinct().ToList();
            int area = 27;
            foreach (var id in ids)
            {
                Thread.Sleep(5000);
                var b = LolCommonHelper.GetDataById(Convert.ToInt32(id), area);
               
                    battleRepository.Insert(b);
                
               
            }
           // 

           // battleRepository.Delete(battleRepository.Table.Where(b => b.GameId == 633344492).FirstOrDefault());
        }

        public IQueryable<Record> GetRecordsByName(string name)
        {
         return    recordRepository.Table.Where(r => r.Name == name);
        }

        public IQueryable<Record> GetAllRecords()
        {
            return recordRepository.Table;
        }

        public List<int> GetUpdateIds()
        {
           var allIds=  LolCommonHelper.GetGameIds("250970574",11);
           int maxId=   battleRepository.Table.Select(b => b.GameId).Max();
          return   allIds.FindAll(id => id > maxId);
        }
    }
}
