using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fzrain.Core.Domain.Lol;

namespace Fzrain.Service.Lol
{
   public  interface ILolService
   {
       
       void UpdateBattle(List<int> ids,int areaId,string myRoleName);
       IQueryable<Battle> GetAllBattles();
       void InitRecord(string filePath);
       IQueryable<Record> GetRecordsByName(string name);
       IQueryable<Record> GetAllRecords();
        List<int> GetUpdateIds(string qq,int areaId);
   }
}
