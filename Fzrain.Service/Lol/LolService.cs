using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using Fzrain.Core.Data;
using Fzrain.Core.Domain.Lol;
using Fzrain.Service.Configuration;
using Newtonsoft.Json;

namespace Fzrain.Service.Lol
{
    public class LolService : ILolService
    {
        #region 私有字段
        private readonly IRepository<Battle> battleRepository;
        private readonly IRepository<Record> recordRepository;
        private readonly IRepository<ChampionInfo> championInfoRepository;
        private readonly IRepository<SnapShot> snapShotRepository;
        private readonly ISettingService settingService;
        #endregion

        #region 构造函数
        public LolService(IRepository<Battle> battleRepository, IRepository<Record> recordRepository, ISettingService settingService, IRepository<ChampionInfo> championInfoRepository, IRepository<SnapShot> snapShotRepository)
        {
            this.battleRepository = battleRepository;
            this.recordRepository = recordRepository;
            this.settingService = settingService;
            this.championInfoRepository = championInfoRepository;
            this.snapShotRepository = snapShotRepository;
        }
        #endregion

        #region 方法
        public void UpdateBattle(List<int> ids, int areaId, string myRoleName)
        {

            foreach (var id in ids)
            {
                Thread.Sleep(5000);
                var battle = GetDataById(Convert.ToInt32(id), areaId);
                battle.ChampionId = battle.Records.FirstOrDefault(r => r.Name == myRoleName).ChampionId;
                battle.IsWin = battle.Records.FirstOrDefault(r => r.Name == myRoleName).IsWin;

                List<double> list = new List<double>();

                foreach (Record record in battle.Records)
                {
                    double damageRatio = Math.Round((double)record.TotalDamage / battle.Records.Sum(r => r.TotalDamage), 2);
                    double killRatio = Math.Round((double)record.Kill / battle.Records.Sum(r => r.Kill), 2);
                    double deathRatio = Math.Round((double)record.Death / battle.Records.Sum(r => r.Death), 2);
                    double assistRatio = Math.Round((double)(record.Assist + record.Kill) / battle.Records.Where(r => r.IsWin == record.IsWin).Sum(r => r.Kill), 2);
                    double contribute = Math.Round(damageRatio * 50 + killRatio * 25 - deathRatio * 15 + assistRatio * 5, 2);
                    list.Add(contribute);
                    record.Contribute = contribute;

                }
                foreach (double d in list)
                {
                    int index = battle.Records.Count(r => r.Contribute > d);
                    // ReSharper disable once CompareOfFloatsByEqualityOperator
                    var records = battle.Records.Where(r => r.Contribute == d).ToList();
                    foreach (Record record in records)
                    {
                        record.ContributeOrder = index + 1;
                    }

                }
                battle.ContributeOrder = battle.Records.First(r => r.Name == myRoleName).ContributeOrder;
                //   battleRepository.Update(battle);
                battleRepository.Insert(battle);
            }
        }

        public IQueryable<Battle> GetAllBattles()
        {
            return battleRepository.Table;
        }


        public IQueryable<Record> GetRecordsByName(string name)
        {
            return recordRepository.Table.Where(r => r.Name == name);
        }

        public IQueryable<Record> GetAllRecords()
        {
            return recordRepository.Table;
        }

        public List<int> GetUpdateIds(string qq, int areaId)
        {
            var allIds = GetGameIds(qq, areaId);
            var ids = battleRepository.Table.Select(b => b.GameId);
            return allIds.Where(id => !ids.Contains(id)).ToList();
        }

        public Dictionary<int, int> GetAppearRate(string starttime = "2014-10-19")
        {
            DateTime time = Convert.ToDateTime(starttime);
            var championInfo = championInfoRepository.Table.Select(c => new { c.ChampionId, c.Position }).ToList();
            var championIds = recordRepository.Table.Where(r => r.Name == "网络中断突然" && r.Battle.StartTime > time)
                 .Select(r => r.ChampionId).ToList();
            Dictionary<int, int> dir = new Dictionary<int, int> { { 1, 0 }, { 2, 0 }, { 3, 0 }, { 4, 0 }, { 5, 0 } };
            foreach (int championId in championIds)
            {
                var position = championInfo.Where(c => c.ChampionId == championId).Select(c => c.Position).FirstOrDefault();
                dir[Convert.ToInt32(position)]++;
            }
            return dir;
        }

        public Dictionary<int, int> GetCarryAbility(string starttime = "2014-10-19")
        {
            DateTime time = Convert.ToDateTime(starttime);
            //   var championInfo = championInfoRepository.Table.Select(c => new { c.ChampionId, c.Position }).ToList();
            var contributeOrders = recordRepository.Table.Where(r => r.Name == "网络中断突然" && r.Battle.StartTime > time).Select(r => r.ContributeOrder).ToList();
            Dictionary<int, int> dir = new Dictionary<int, int> { { 1, 0 }, { 2, 0 }, { 3, 0 }, { 4, 0 }, { 5, 0 } };
            foreach (int order in contributeOrders)
            {
                if (order <= 2)
                {
                    dir[1]++;
                }
                else if (order <= 4)
                {
                    dir[2]++;
                }
                else if (order <= 6)
                {
                    dir[3]++;
                }
                else if (order <= 8)
                {
                    dir[4]++;
                }
                else if (order <= 10)
                {
                    dir[5]++;
                }

            }
            return dir;
        }

        public IQueryable<SnapShot> GetAllSnapShots()
        {
           return  snapShotRepository.Table;
        }
        #endregion

        #region 工具方法
        private  dynamic GetJsonResponse(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "Get";
            request.Headers.Add(HttpRequestHeader.Cookie, settingService.GetValueByName("lolCookie"));

            request.UserAgent = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; Trident/6.0)";

            // ReSharper disable once AssignNullToNotNullAttribute
            var reader = new StreamReader(request.GetResponseAsync().Result.GetResponseStream());
            var r = reader.ReadToEnd();
            string json = r.Substring(26, r.Length - 38);
            return JsonConvert.DeserializeObject(json);
        }


        private List<int> GetGameIds(string qq, int areaId)
        {
            string p = "[[3,{\"qquin\":\""+qq+"\",\"area_id\":\""+areaId+"\",\"bt_num\":\"0\",\"bt_list\":[],\"champion_id\":0,\"offset\":0,\"limit\":0,\"mvp_flag\":-1}]]";
           // string p = "[[3,{\"qquin\":\"" + qq + "\",\"area_id\":\"" + areaId + "\",\"champion_id\":0,\"offset\":0,\"limit \":0}]]";
            string url = "http://api.pallas.tgp.qq.com/core/tcall?callback=getGameDetailCallback&dtag=profile&p=" + p +
                         "&t=1417937593108";
            dynamic record = GetJsonResponse(url);
            dynamic d = record.data[0].battle_list;
            List<int> list = new List<int>();
            for (int i = 0; i < d.Count; i++)
            {
                list.Add(Convert.ToInt32(d[i].game_id));
            }
            return list;
        }
        public Battle GetDataById(int id, int areaId)
        {

            dynamic record =
                GetJsonResponse(
                    "http://api.pallas.tgp.qq.com/core/tcall?callback=getGameDetailCallback&dtag=profile&p=[[4,{\"area_id\":\"" +
                    areaId + "\",\"game_id\":\"" + id + "\"}]]&t=1417937593108");

            dynamic rs = record.data[0].battle.gamer_records;
            List<Record> list = new List<Record>();
            for (int i = 0; i < rs.Count; i++)
            {
                string tagList = string.Empty;
                for (int j = 0; j < rs[i].battle_tag_list.Count; j++)
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
                    Level =rs[i].level,
                    Experience =rs[i].exp,
                    Name = rs[i].name,
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
                    GameScore =rs[i].game_score,
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

     
        #endregion

    }
}
