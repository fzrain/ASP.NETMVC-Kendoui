using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using Fzrain.Core.Domain.Lol;
using Fzrain.Service.Lol;
using Fzrain.Web.Models.Lol;
using Kendo.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Fzrain.Data;

namespace Fzrain.Web.Controllers
{
    public class LolController : Controller
    {
        private readonly ILolService lolService;
        public List<string> MyHeroList = new List<string> { "笨笨秒杀上帝", "网络中断突然" };
        
        public LolController(ILolService lolService)
        {
            this.lolService = lolService;
        }

        // GET: Lol
        public ActionResult Index()
        {
          
            return View();
        }
        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {

            if (request.Sorts.Count == 0)//默认按Id倒序排列
                request.Sorts.Add(new SortDescriptor { Member = "GameId", SortDirection = ListSortDirection.Descending });

            return Json(lolService.GetAllBattles().Select(b => new { b.BattleType, b.Duration, b.GameId, b.Id, b.StartTime, b.ChampionId, b.IsWin,b.ContributeOrder  }).ToDataSourceResult(request));
        }

        public ActionResult ShowChampionInfo(string filter, DateTime? beginDate)
        {
            var allRecodes = lolService.GetAllRecords().IncludeProperties(r=>r.Battle);
            if (!string.IsNullOrWhiteSpace(filter))
            {
                int num = Convert.ToInt32(filter);
                if (num >= 100)
                    allRecodes = allRecodes.Take(10*num);
                else
                {
                    DateTime startTime = DateTime.Now.AddMonths(-num);
                    allRecodes = allRecodes.Where(r => r.Battle.StartTime > startTime);
                }
                
                
            }
            if (beginDate.HasValue)
            {
                allRecodes = allRecodes.Where(r => r.Battle.StartTime > beginDate);
            }
            var recodes = allRecodes.Where(l => l.Battle.BattleType == 6).Select(l => new { l.ChampionId, l.IsWin, l.Name }).ToList();
            var champions = from r in recodes
                            group r by r.ChampionId
                            into g
                            select g.Key;
            List<LolChampionInfoViewModel> model = new List<LolChampionInfoViewModel>();
            foreach (var c in champions)
            {
                var heroRecords = recodes.Where(a => a.ChampionId == c).ToList();
                model.Add(new LolChampionInfoViewModel
                {
                    ChampionId = c,
                    TotalApprance = heroRecords.Count,
                    TotalWinCount = heroRecords.Where(a => a.IsWin == 1).Count(),
                    MyApprance = heroRecords.Where(a => MyHeroList.Contains(a.Name)).Count(),
                    MyWinCount = heroRecords.Where(a => MyHeroList.Contains(a.Name)).Count(a => a.IsWin == 1)
                });
            }
            return View(model.OrderByDescending(l => l.MyApprance).ToList());
        }
        public ActionResult WaitUpdate(string qq, int areaId)
        {
            var ids = lolService.GetUpdateIds(qq, areaId);
            return Json(ids);
        }
        public ActionResult UpdateBattle(string ids, int areaId, string myRoleName)
        {
            List<int> gameIds = ids.Split(',').Select(id => Convert.ToInt32(id)).ToList();
            lolService.UpdateBattle(gameIds, areaId, myRoleName);
            return Content("ok");
        }
        public ActionResult GetBattleDetail(int gameId)
        {
            var records = lolService.GetAllRecords().Where(r => r.Battle.GameId == gameId).ToList();
            return PartialView("BattleDetail", records);
        }

        public ActionResult MyPerformance(int? championId)
        {
            championId = championId ?? 1;
            var records = lolService.GetAllRecords().Where(r => r.ChampionId == championId).ToList();
            int avg = records.Sum(r => GetProficiency(r));
            
            avg = avg / records.Count();
            ViewBag.Avg = new List<double> { avg, avg, avg, avg, avg, avg, avg, avg, avg, avg };
            ViewBag.AvgInfo = new List<string>
            {
                  records.Where(r => MyHeroList.Contains(r.Name)).Average(r => r.Kill).ToString("0.00") ,    
                  records.Where(r => MyHeroList.Contains(r.Name)).Average(r => r.Death).ToString("0.00"),
                  records.Where(r => MyHeroList.Contains(r.Name)).Average(r => r.Assist).ToString("0.00"),
                  records.Average(r => r.Kill).ToString("0.00"),
                  records.Average(r => r.Death).ToString("0.00"),
                  records.Average(r => r.Assist).ToString("0.00")
            };
            var myRecords = records
                    .Where(r => MyHeroList.Contains(r.Name))
                    .OrderByDescending(r => r.Battle.StartTime)
                    .Take(10)
                    .ToList();
            List<ChampionGrowupViewModel> model = new List<ChampionGrowupViewModel>();
            foreach (var r in myRecords)
            {
                model.Add(new ChampionGrowupViewModel
                {
                    ChampionId = r.ChampionId,
                    GameId = r.Battle.GameId,
                    StartTime = r.Battle.StartTime,
                    Proficiency = GetProficiency(r)
                });
            }
            return View(model.OrderBy(m => m.StartTime).ToList());
        }
        [NonAction]
        public int GetProficiency(Record r)
        {
            int duration = r.Battle.Duration;
            int killIndex = r.Battle.Records.Where(record => record.Kill >= r.Kill).Count();
            int goldIndex = r.Battle.Records.Where(record => record.GoldEarned >= r.GoldEarned).Count();
            int damageIndex = r.Battle.Records.Where(record => record.TotalDamage >= r.TotalDamage).Count();
            double kda = ((double)(r.Kill * 150 - r.Death * 80 + r.Assist * 30) / duration) * 1300;
            int contribute = 100 * (33 - killIndex - goldIndex - damageIndex);
            int specialTag = (r.BattleTagList.Contains("5") ? 300 : 0) + (r.BattleTagList.Contains("6") ? 500 : 0) +
                             (r.BattleTagList.Contains("7") ? 1000 : 0) + (r.BattleTagList.Contains("8") ? 800 : 0);
            return Convert.ToInt32(kda) + contribute + specialTag;

        }

        public ActionResult FilterMenuChampion()
        {
            return Json(lolService.GetAllRecords().GroupBy(r => r.ChampionId).Select(c => c.Key), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetContribute()
        {
            return Json(lolService.GetContributes(), JsonRequestBehavior.AllowGet);
        }
    }
}