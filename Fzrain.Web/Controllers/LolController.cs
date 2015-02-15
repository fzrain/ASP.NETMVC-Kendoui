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
            List<string> model = new List<string> {};
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
            var allRecodes = lolService.GetAllRecords().IncludeProperties(r=>r.Battle).OrderByDescending(r=>r.Battle.StartTime).AsQueryable();
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
            var recodes = allRecodes.Where(l => l.Battle.BattleType == 6).Select(l => new { l.ChampionId, l.IsWin, l.Name,l.Contribute ,l.ContributeOrder }).ToList();
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
                    MyWinCount = heroRecords.Where(a => MyHeroList.Contains(a.Name)).Count(a => a.IsWin == 1),
                    MyContribute =heroRecords.Any(a => MyHeroList.Contains(a.Name)) ? heroRecords.Where(a => MyHeroList.Contains(a.Name)).Average(a=>a.ContributeOrder ) : 0,
                    TotalContribute = heroRecords.Average(a => a.ContributeOrder)

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
      
      

        public ActionResult FilterMenuChampion()
        {
            return Json(lolService.GetAllRecords().GroupBy(r => r.ChampionId).Select(c => c.Key), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ShowMyChampionInfos(int? heroId)
        {
           
            var ids = lolService.GetAllBattles().GroupBy(r => r.ChampionId).Select(c => new {c.Key,num= c.Count()}).OrderByDescending(k=>k.num).ToList();
            List<ChampionGrowupViewModel> model = new List<ChampionGrowupViewModel>();
            if (!heroId.HasValue)
            {
                heroId = ids.Select(id => id.Key).First();
            }
            ViewBag.CurrentId = heroId;
            var recordLists = lolService.GetAllRecords().IncludeProperties(r => r.Battle).ToList();
            foreach (var championId in ids.Select(id=>id.Key))
            {
                var records = recordLists.Where(r => r.ChampionId == championId).ToList();
                double avg = 0;
                foreach (var r in records)
                    avg += r.Contribute;
                avg = avg / records.Count();
                ViewData[championId.ToString()] = new List<double> { avg, avg, avg, avg, avg, avg, avg, avg, avg, avg };
               
                    var myRecords = records
                        .Where(r => MyHeroList.Contains(r.Name))
                        .OrderByDescending(r => r.Battle.StartTime)
                        .Take(10)
                        .ToList();
           
                foreach (var r in myRecords)
                {
                    model.Add(new ChampionGrowupViewModel
                    {
                        ChampionId = r.ChampionId,
                        GameId = r.Battle.GameId,
                        StartTime = r.Battle.StartTime,
                        Proficiency = r.Contribute
                    });
                }
            }

            ViewBag.ChampionInfos = model;
            return View(ids.Select(id => id.Key).ToList());
        }
    }
}