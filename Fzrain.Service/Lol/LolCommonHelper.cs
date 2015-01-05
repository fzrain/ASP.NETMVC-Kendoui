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
     
        public static List<int> GetGameIds(string qq, int areaId)
        {

            string p = "[[3,{\"qquin\":\"" + qq + "\",\"area_id\":\"" + areaId + "\",\"champion_id\":0,\"offset\":0,\"limit \":0}]]";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://api.pallas.tgp.qq.com/core/tcall?callback=getGameDetailCallback&dtag=profile&p=" + p + "&t=1417937593108");
            request.Method = "Get";
            request.Headers.Add(HttpRequestHeader.Cookie, "pgv_si=s5815257088; pgv_info=ssid=s9130164624; pgv_pvid=219706695; pt2gguin=o0250970574; RK=re3LT0wkGY; ptcz=2f0439abda0979a7efd95ca2ea2e376a6ff344ced275740021b85762766c75b1; pgv_pvi=2879912960; ac=1,006,003; ptui_loginuin=250970574; puin=250970574; pkey=000154A93EF90070F8EF39FCB54F32C9AC2BF20748E742C1323E97899AA2259EA4115BC2E7B95145E5AD0214D90890C3CCA67AA5BEC03F9BB2546EA794FE1CB447DA1BB2A190DB7A010624F7674631C8C3346F85177BA5E5ECB38A0720A459F5CB97E77280D9F4981B081EBC9F044AC369A4437F864C9469; aid=11; uname=; vlist=11%3AU5641261114915464100; refurl=http%3A%2F%2Fapi.tgp.qq.com%2Fprofile%2Fv1405%2Fhistory.shtml%3Fvuin%3D250970574%26vaid%3D11%23hid%3D0%26gid%3D654953872%26pnum%3D1; refnum=1");

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
            request.Headers.Add(HttpRequestHeader.Cookie, "pgv_si=s5815257088; pgv_info=ssid=s9130164624; pgv_pvid=219706695; pt2gguin=o0250970574; RK=re3LT0wkGY; ptcz=2f0439abda0979a7efd95ca2ea2e376a6ff344ced275740021b85762766c75b1; pgv_pvi=2879912960; ac=1,006,003; ptui_loginuin=250970574; puin=250970574; pkey=000154A93EF90070F8EF39FCB54F32C9AC2BF20748E742C1323E97899AA2259EA4115BC2E7B95145E5AD0214D90890C3CCA67AA5BEC03F9BB2546EA794FE1CB447DA1BB2A190DB7A010624F7674631C8C3346F85177BA5E5ECB38A0720A459F5CB97E77280D9F4981B081EBC9F044AC369A4437F864C9469; aid=11; uname=; vlist=11%3AU5641261114915464100; refurl=http%3A%2F%2Fapi.tgp.qq.com%2Fprofile%2Fv1405%2Fhistory.shtml%3Fvuin%3D250970574%26vaid%3D11%23hid%3D0%26gid%3D654953872%26pnum%3D1; refnum=1");

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
