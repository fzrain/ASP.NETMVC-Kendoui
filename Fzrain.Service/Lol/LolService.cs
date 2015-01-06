using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Chsword;
using Fzrain.Core.Data;
using Fzrain.Core.Domain.Lol;
using Fzrain.Service.Configuration;

namespace Fzrain.Service.Lol
{
    public class LolService : ILolService
    {
        private readonly IRepository<Battle> battleRepository;
        private readonly IRepository<Record > recordRepository;
        private readonly ISettingService settingService;
       
        public LolService(IRepository<Battle> battleRepository, IRepository<Record> recordRepository, ISettingService settingService)
        {
            this.battleRepository = battleRepository;
            this.recordRepository = recordRepository;
            this.settingService = settingService;
        }
        public void UpdateBattle(List<int> ids,int areaId,string myRoleName)
        {       
          
            foreach (var id in ids)
            {
                Thread.Sleep(5000);
                var b =GetDataById(Convert.ToInt32(id), areaId);
                b.ChampionId = b.Records.Where(r => r.Name == myRoleName).FirstOrDefault().ChampionId;
                b.IsWin = b.Records.Where(r => r.Name == myRoleName).FirstOrDefault().IsWin;
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
            const int area = 27;
            foreach (var id in ids)
            {
                Thread.Sleep(5000);
                var b = GetDataById(Convert.ToInt32(id), area);
               
                    battleRepository.Insert(b);
                
               
            }
         
        }

        public IQueryable<Record> GetRecordsByName(string name)
        {
         return    recordRepository.Table.Where(r => r.Name == name);
        }

        public IQueryable<Record> GetAllRecords()
        {
            return recordRepository.Table;
        }

     

        public List<int> GetUpdateIds(string qq, int areaId)
        {
           var allIds=  GetGameIds(qq, areaId);
           int maxId=   battleRepository.Table.Select(b => b.GameId).Max();
          return   allIds.FindAll(id => id > maxId);
        }

        public List<int> GetGameIds(string qq, int areaId)
        {

            string p = "[[3,{\"qquin\":\"" + qq + "\",\"area_id\":\"" + areaId + "\",\"champion_id\":0,\"offset\":0,\"limit \":0}]]";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://api.pallas.tgp.qq.com/core/tcall?callback=getGameDetailCallback&dtag=profile&p=" + p + "&t=1417937593108");
            request.Method = "Get";
            request.Headers.Add(HttpRequestHeader.Cookie, settingService.GetValueByName("lolCookie"));

            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.2; WOW64; Trident/7.0; .NET4.0E; .NET4.0C; InfoPath.3; .NET CLR 3.5.30729; .NET CLR 2.0.50727; .NET CLR 3.0.30729)";
         
            // ReSharper disable once AssignNullToNotNullAttribute
            var reader = new StreamReader(request.GetResponseAsync().Result.GetResponseStream());
            var r = reader.ReadToEnd();
            string json = r.Substring(26, r.Length - 38);
            dynamic record = new JDynamic(json);
            dynamic d = record.data[0].battle_list;
            List<int> list = new List<int>();
            for (int i = 0; i < d.Length; i++)
            {
                list.Add(d[i].game_id);
            }
            return list;
        }
        public Battle GetDataById(int id, int areaId)
        {
            //  string id = "633344492";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://api.pallas.tgp.qq.com/core/tcall?callback=getGameDetailCallback&dtag=profile&p=[[4,{\"area_id\":\"" + areaId + "\",\"game_id\":\"" + id + "\"}]]&t=1417937593108");

            request.Method = "Get";
            request.Headers.Add(HttpRequestHeader.Cookie, settingService.GetValueByName("lolCookie"));

            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.2; WOW64; Trident/7.0; .NET4.0E; .NET4.0C; InfoPath.3; .NET CLR 3.5.30729; .NET CLR 2.0.50727; .NET CLR 3.0.30729)";

            // ReSharper disable once AssignNullToNotNullAttribute
            var reader = new StreamReader(request.GetResponseAsync().Result.GetResponseStream());
            var r = reader.ReadToEnd();
            string json = r.Substring(26, r.Length - 38);
            dynamic record = new JDynamic(json);
            string time = record.data[0].battle.start_time.ToString();
            if (string.IsNullOrWhiteSpace(time))
            {
                return null;
            }
            dynamic rs = record.data[0].battle.gamer_records;
            List<Record> list = new List<Record>();
            for (int i = 0; i < rs.Length; i++)
            {
                string tagList = string.Empty;
                for (int j = 0; j < rs[i].battle_tag_list.Length; j++)
                {
                    tagList += rs[i].battle_tag_list[j].tag_id + ";";
                }
                list.Add(new Record
                {
                    QQ = rs[i].qquin,
                    ChampionId = rs[i].champion_id,
                    GoldEarned = rs[i].gold_earned,
                    DamageTaken = rs[i].total_damage_taken,
                    TotalDamage = rs[i].total_damage_dealt_to_champions,
                    Name = Regex.Unescape(rs[i].name),
                    IsWin = rs[i].win,
                    Kill = rs[i].champions_killed,
                    Death = rs[i].num_deaths,
                    Assist = rs[i].assists,
                    Item0 = rs[i].item0,
                    Item1 = rs[i].item1,
                    Item2 = rs[i].item2,
                    Item3 = rs[i].item3,
                    Item4 = rs[i].item4,
                    Item5 = rs[i].item5,
                    BattleTagList = tagList.TrimEnd(';'),
                    MinionsKilled = rs[i].minions_killed,
                    LargestKillingSpree = rs[i].largest_killing_spree,

                });


            }
            return new Battle
            {
                GameId = record.data[0].battle.game_id,
                StartTime = record.data[0].battle.start_time,
                BattleType = record.data[0].battle.game_type,
                // ChampionId = record.data[0].battle.champion_id,
                Duration = record.data[0].battle.duration,
                //   IsWin = record.data[0].battle.win,
                Records = list
            };





        }
    }
}
