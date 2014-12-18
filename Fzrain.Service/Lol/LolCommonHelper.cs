using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Chsword;
using Fzrain.Core.Domain.Lol;

namespace Fzrain.Service.Lol
{
   internal  class LolCommonHelper
    {
       public  static List<int> GetGameIdsV2(string qq,int areaId)
       {

           string p = "[[3,{\"qquin\":\"" + qq + "\",\"area_id\":\"" + areaId + "\",\"champion_id\":0,\"offset\":0,\"limit \":0}]]";
           HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://api.pallas.tgp.qq.com/core/tcall?callback=getGameDetailCallback&dtag=profile&p=" + p + "&t=1417937593108");
           request.Method = "Get";
           request.Headers.Add(HttpRequestHeader.Cookie, "ac=1,006,008; pt2gguin=o0250970574; RK=3f2DD0wkEY; ptcz=b1b444e3f1095f3d0213fd2cc508f8e83ca86c7365fcad2b914b2284525bb7c4; pgv_pvid=7088641281; pgv_pvi=4190417920; o_cookie=250970574; pgv_si=s9706957824; pgv_info=ssid=s9182984347; puin=250970574; pkey=00015483FEF80070668E7AF7DED00D9099625C5D8BD808E6A2BAE6B70EA989C9BDD7662ACDE2DF8AFFD1261DFD2BBAE9A6CA3B6260447B52B2D10EB5D1C9FFB86026FD46EA911919C6A26D9F0A2891C1CE30337C67BD3301CB3DB711FC0AABC87331EDF28DE79865B9C4B3DD9EF8B1E4BCE1AE1AE6376C6E; aid=11; uname=");

           request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.2; WOW64; Trident/7.0; .NET4.0E; .NET4.0C; InfoPath.3; .NET CLR 3.5.30729; .NET CLR 2.0.50727; .NET CLR 3.0.30729)";
           //   request.Headers.Add();
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

       public  static Battle GetDataByIdV2(int id,int areaId)
       {
           //  string id = "633344492";
           HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://api.pallas.tgp.qq.com/core/tcall?callback=getGameDetailCallback&dtag=profile&p=[[4,{\"area_id\":\"" + areaId + "\",\"game_id\":\"" + id + "\"}]]&t=1417937593108");

           request.Method = "Get";
           request.Headers.Add(HttpRequestHeader.Cookie, "ac=1,006,008; pt2gguin=o0250970574; RK=3f2DD0wkEY; ptcz=b1b444e3f1095f3d0213fd2cc508f8e83ca86c7365fcad2b914b2284525bb7c4; pgv_pvid=7088641281; pgv_pvi=4190417920; o_cookie=250970574; pgv_si=s9706957824; pgv_info=ssid=s9182984347; puin=250970574; pkey=00015483FEF80070668E7AF7DED00D9099625C5D8BD808E6A2BAE6B70EA989C9BDD7662ACDE2DF8AFFD1261DFD2BBAE9A6CA3B6260447B52B2D10EB5D1C9FFB86026FD46EA911919C6A26D9F0A2891C1CE30337C67BD3301CB3DB711FC0AABC87331EDF28DE79865B9C4B3DD9EF8B1E4BCE1AE1AE6376C6E; aid=11; uname=");

           request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.2; WOW64; Trident/7.0; .NET4.0E; .NET4.0C; InfoPath.3; .NET CLR 3.5.30729; .NET CLR 2.0.50727; .NET CLR 3.0.30729)";

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

        public static List<int> GetGameIds(string qq, int areaId)
        {

            string p = "[[3,{\"qquin\":\"" + qq + "\",\"area_id\":\"" + areaId + "\",\"champion_id\":0,\"offset\":0,\"limit \":0}]]";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://api.pallas.tgp.qq.com/core/tcall?callback=getGameDetailCallback&dtag=profile&p=" + p + "&t=1417937593108");
            request.Method = "Get";
            request.Headers.Add(HttpRequestHeader.Cookie, "ied_rf=--; puin=250970574; pkey=000154901B570070A68449E4256A0DD78A26CEDDB02CE35990ED862A05FE323B7C13C27F4CAFBCABFC4A273D1D088DDB6D2701C5B41662369EC77EED6C6D488C4081FA0AC746E707B7A684B3FE3B4F20DC9061341DC19001AE9914B7E078B072F0D2381E0BB3D83CA70E446C540C246B4DD81781FAB1C56E; aid=11; uname=; pgv_si=s2308837376; pgv_info=ssid=s4890273172; pgv_pvid=219706695; pt2gguin=o0857142253; RK=re3LT0wkGY; ptcz=2d314c7926a2f9f482a43ff1a15cf7587491c5976ee01fb56e232043d23702ce; pgv_pvi=2879912960; ac=1,006,008; uin_cookie=857142253; euin_cookie=963438AA3F508A3E5F7A03576E1B905A265FB4B48132C86E");

            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.2; WOW64; Trident/7.0; .NET4.0E; .NET4.0C; InfoPath.3; .NET CLR 3.5.30729; .NET CLR 2.0.50727; .NET CLR 3.0.30729)";
            //   request.Headers.Add();
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

        public static Battle GetDataById(int id, int areaId)
        {
            //  string id = "633344492";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://api.pallas.tgp.qq.com/core/tcall?callback=getGameDetailCallback&dtag=profile&p=[[4,{\"area_id\":\"" + areaId + "\",\"game_id\":\"" + id + "\"}]]&t=1417937593108");

            request.Method = "Get";
            request.Headers.Add(HttpRequestHeader.Cookie, "ied_rf=--; puin=250970574; pkey=000154901B570070A68449E4256A0DD78A26CEDDB02CE35990ED862A05FE323B7C13C27F4CAFBCABFC4A273D1D088DDB6D2701C5B41662369EC77EED6C6D488C4081FA0AC746E707B7A684B3FE3B4F20DC9061341DC19001AE9914B7E078B072F0D2381E0BB3D83CA70E446C540C246B4DD81781FAB1C56E; aid=11; uname=; pgv_si=s2308837376; pgv_info=ssid=s4890273172; pgv_pvid=219706695; pt2gguin=o0857142253; RK=re3LT0wkGY; ptcz=2d314c7926a2f9f482a43ff1a15cf7587491c5976ee01fb56e232043d23702ce; pgv_pvi=2879912960; ac=1,006,008; uin_cookie=857142253; euin_cookie=963438AA3F508A3E5F7A03576E1B905A265FB4B48132C86E");

            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.2; WOW64; Trident/7.0; .NET4.0E; .NET4.0C; InfoPath.3; .NET CLR 3.5.30729; .NET CLR 2.0.50727; .NET CLR 3.0.30729)";

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
