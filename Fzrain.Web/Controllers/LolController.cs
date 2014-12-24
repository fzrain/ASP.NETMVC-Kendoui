using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fzrain.Core.Domain;
using Fzrain.Core.Domain.Lol;
using Fzrain.Service;
using Fzrain.Service.Lol;
using Fzrain.Web.Models.Lol;
using Kendo.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace Fzrain.Web.Controllers
{
    public class LolController : Controller
    {
        private readonly ILolService lolService;
        public List<string> MyHeroList = new List<string> { "笨笨秒杀上帝", "网络中断突然" };

        public LolController(ILolService lolService, IUserService userService)
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

            return Json(lolService.GetAllBattles().Select(b => new { b.BattleType, b.Duration, b.GameId, b.Id, b.StartTime, b.ChampionId, b.IsWin }).ToDataSourceResult(request));
        }

        public ActionResult ShowChampionInfo()
        {
            var recodes = lolService.GetAllRecords().Where(l => l.Battle.BattleType == 6).Select(l => new { l.ChampionId, l.IsWin, l.Name }).ToList();
            var champions = from r in recodes
                            group r by r.ChampionId
                            into g
                            select g.Key;
            List<LolChampionInfoViewMode> model = new List<LolChampionInfoViewMode>();
            foreach (var c in champions)
            {
                var heroRecords = recodes.Where(a => a.ChampionId == c).ToList();
                model.Add(new LolChampionInfoViewMode
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
        public ActionResult WaitUpdate()
        {
            var Ids = lolService.GetUpdateIds();
            return Json(Ids);
        }
        public ActionResult UpdateBattle(string ids)
        {
            var Ids = ids.Split(',');
            List<int> gameIds = new List<int>();
            foreach (var id in Ids)
            {
                gameIds.Add(Convert.ToInt32(id));
            }
            lolService.UpdateBattle(gameIds, 11);
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
            var records = lolService.GetAllRecords().Where(r => r.ChampionId == championId);
           double avg= records.Average(r => r.Kill);
            ViewBag.Avg = new List<double> { avg , avg , avg , avg , avg , avg , avg , avg , avg , avg };
            ViewBag.AvgRecord = "平均击杀：" +
                                records.Where(r => MyHeroList.Contains(r.Name)).Average(r => r.Kill).ToString("0.00") +
                                " 平均死亡：" +
                                records.Where(r => MyHeroList.Contains(r.Name)).Average(r => r.Death).ToString("0.00") +
                                " 平均助攻：" +
                                records.Where(r => MyHeroList.Contains(r.Name)).Average(r => r.Assist).ToString("0.00");

            return View(records.Where(r => MyHeroList.Contains(r.Name)).OrderByDescending(r => r.Battle.StartTime).Take(10).ToList());
        }

        public ActionResult FilterMenuChampion()
        {

            return Json(lolService.GetAllRecords().GroupBy(r => r.ChampionId).Select(c => c.Key), JsonRequestBehavior.AllowGet);
        }
    }
}