using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fzrain.Core.Domain;
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

            return Json(lolService.GetAllBattles().Select(b=>new{b.BattleType ,b.Duration,b.GameId ,b.Id ,b.StartTime,b.ChampionId,b.IsWin  }).ToDataSourceResult(request));
        }

        public ActionResult ShowChampionInfo()
        {
           var recodes= lolService.GetAllRecords().Where(l=>l.Battle.BattleType ==6).Select(l=>new{l.ChampionId,l.IsWin,l.Name}).ToList();
            var champions = from r in recodes
                group r by r.ChampionId
                into g
                select g.Key;
            List<LolChampionInfoViewMode> model = new List<LolChampionInfoViewMode>();
            foreach (var c in champions)
            {
              var heroRecords=  recodes.Where(a => a.ChampionId == c).ToList();
                model.Add(new LolChampionInfoViewMode
                {
                    ChampionId =c,
                    TotalApprance =heroRecords.Count ,
                    TotalWinCount =heroRecords.Where(a=>a.IsWin ==1).Count(),
                    MyApprance =heroRecords.Where (a=>a.Name =="网络中断突然"||a.Name =="笨笨秒杀上帝").Count(),
                    MyWinCount = heroRecords.Where(a =>a.Name == "网络中断突然" || a.Name == "笨笨秒杀上帝").Count(a=>a.IsWin ==1)
                });
            }
            return View(model.OrderByDescending(l=>l.MyApprance).ToList());
        }
        public ActionResult WaitUpdate()
        {
           var Ids= lolService.GetUpdateIds();
            return Json(Ids);
        }
        public ActionResult UpdateBattle(string ids)
        {
           var Ids= ids.Split(',');
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
           var records= lolService.GetAllRecords().Where(r => r.Battle.GameId == gameId).ToList();
            return PartialView("BattleDetail",records);
        }
    }
}