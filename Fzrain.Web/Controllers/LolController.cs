using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Fzrain.Core.Domain.Lol;
using Fzrain.Service.Lol;
using Fzrain.Web.Framework.Mvc;
using Fzrain.Web.Models.Common;
using Fzrain.Web.Models.Lol;
using Kendo.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Fzrain.Data;
using System.Linq.Expressions;
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
            ViewBag.AppearRate = lolService.GetAppearRate();
            ViewBag.CarryAbility = lolService.GetCarryAbility();
            return View();
        }

        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {

            if (request.Sorts.Count == 0)//默认按Id倒序排列
                request.Sorts.Add(new SortDescriptor { Member = "GameId", SortDirection = ListSortDirection.Descending });

            return Json(lolService.GetAllBattles().Select(b => new { b.BattleType, b.Duration, b.GameId, b.Id, b.StartTime, b.ChampionId, b.IsWin, b.ContributeOrder }).ToDataSourceResult(request));
        }

        public ActionResult ShowChampionInfo(string filter, DateTime? beginDate)
        {
            var allRecodes = lolService.GetAllRecords().IncludeProperties(r => r.Battle).OrderByDescending(r => r.Battle.StartTime).AsQueryable();
            if (!string.IsNullOrWhiteSpace(filter))
            {
                int num = Convert.ToInt32(filter);
                if (num >= 100)
                    allRecodes = allRecodes.Take(10 * num);
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
            var recodes = allRecodes.Select(l => new { l.ChampionId, l.IsWin, l.Name, l.Contribute, l.ContributeOrder }).ToList();
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
                    MyContribute = heroRecords.Any(a => MyHeroList.Contains(a.Name)) ? heroRecords.Where(a => MyHeroList.Contains(a.Name)).Average(a => a.ContributeOrder) : 0,
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

            var ids = lolService.GetAllBattles().GroupBy(r => r.ChampionId).Select(c => new { c.Key, num = c.Count() }).OrderByDescending(k => k.num).ToList();
            List<ChampionGrowupViewModel> model = new List<ChampionGrowupViewModel>();
            if (!heroId.HasValue)
            {
                heroId = ids.Select(id => id.Key).First();
            }
            ViewBag.CurrentId = heroId;
            var recordLists = lolService.GetAllRecords().IncludeProperties(r => r.Battle).ToList();
            foreach (var championId in ids.Select(id => id.Key))
            {
                var records = recordLists.Where(r => r.ChampionId == championId).ToList();
                double avg = 0;
                foreach (var r in records)
                    avg += r.Contribute;
                avg = Math.Round(avg / records.Count(), 2);
                ViewData[championId.ToString()] = new List<double> { avg, avg, avg, avg, avg, avg, avg, avg, avg, avg, avg, avg, avg, avg, avg, avg, avg, avg, avg, avg };

                var myRecords = records
                    .Where(r => MyHeroList.Contains(r.Name))
                    .OrderByDescending(r => r.Battle.StartTime)
                    .Take(20)
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


        public ActionResult FullDataAnalysis()
        {

            return View();
        }

        public ActionResult DataInfo(int dataInfo, DateTime? startTime, DateTime? endTime, bool ismine = false, bool isAsc = true)
        {
            var models = lolService.GetAllRecords().IncludeProperties(r => r.Battle);
            if (ismine)
                models = models.Where(r => MyHeroList.Contains(r.Name));

            if (startTime.HasValue)
                models = models.Where(r => r.Battle.StartTime > startTime);

            if (endTime.HasValue)
                models = models.Where(r => r.Battle.StartTime < endTime);

            List<KeyValuePairViewModel> list = new List<KeyValuePairViewModel>();

            switch (dataInfo)
            {
                case 1: PrepareAnalyseModel(list, models, s => new { s.Key, Value = s.Average(r => r.TotalDamage) }); break;
                case 2: PrepareAnalyseModel(list, models, s => new { s.Key, Value = s.Average(r => r.Contribute)}); break;
                case 3: PrepareAnalyseModel(list, models, s => new { s.Key, Value = s.Average(r => r.ContributeOrder)}); break;
                case 4: PrepareAnalyseModel(list, models, s => new { s.Key, Value = s.Average(r => r.Kill) }); break;
                case 5: PrepareAnalyseModel(list, models, s => new { s.Key, Value = s.Average(r => r.Death) }); break;
                case 6: PrepareAnalyseModel(list, models, s => new { s.Key, Value = s.Average(r => r.Assist) }); break;
                case 7: PrepareAnalyseModel(list, models, s => new { s.Key, Value = s.Average(r => r.DamageTaken) }); break;
            }
            list = isAsc ? list.OrderBy(l => l.Value).ToList() : list.OrderByDescending(l => l.Value).ToList();
            return Json(list);
        }

	    public ActionResult TagInfo(string  tagInfo, DateTime? startTime, DateTime? endTime, bool ismine = false,
		    bool isAsc = true)
	    {
		    //lolService.GetAllRecords().Where(r => r.BattleTagList.Contains(tagInfo.ToString())).ToList();
			var models = lolService.GetAllRecords().IncludeProperties(r => r.Battle);
			if (ismine)
				models = models.Where(r => MyHeroList.Contains(r.Name));

			if (startTime.HasValue)
				models = models.Where(r => r.Battle.StartTime > startTime);

			if (endTime.HasValue)
				models = models.Where(r => r.Battle.StartTime < endTime);

		    models = models.Where(r => r.BattleTagList.Contains(tagInfo));
            List<KeyValuePairViewModel> list = new List<KeyValuePairViewModel>();
		    PrepareAnalyseModel(list, models, s => new {s.Key, Value = s.Count()});		
			list = isAsc ? list.OrderBy(l => l.Value).ToList() : list.OrderByDescending(l => l.Value).ToList();
			return Json(list);
		}

	    public void PrepareAnalyseModel(List<KeyValuePairViewModel> list, IQueryable<Record> models,
            Expression<Func<IGrouping<int, Record>, dynamic>> seletor)
        {            
            var list4 = models.GroupBy(r => r.ChampionId)
                   .OrderBy(s => s.Key)
                   .Select(seletor);
            foreach (var m in list4)
                list.Add(new KeyValuePairViewModel { Key = m.Key, Value = m.Value });

        }

        public ActionResult BrightInfo(int championId, bool isMine)
        {
            var records = lolService.GetAllRecords().IncludeProperties(r => r.Battle).Where(r => r.ChampionId == championId && (!isMine || MyHeroList.Contains(r.Name)));
            return Json(records.OrderByDescending(r => r.Contribute).ToList().Select(r => new { r.Name, r.IsWin, r.Contribute, Battle = new { r.Battle.StartTime, r.Battle.GameId } }));


        }

        public ActionResult Records([DataSourceRequest] DataSourceRequest request)
        {
            return View("Records");
        }
        public ActionResult RecordsRead([DataSourceRequest] DataSourceRequest request)
        {
            if (request.Sorts.Count == 0)//默认按Id倒序排列
                request.Sorts.Add(new SortDescriptor { Member = "Id", SortDirection = ListSortDirection.Descending });

            return Json(lolService.GetAllRecords().ToDataSourceResult(request));
        }
        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonNetResult
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior
            };
        }
    }
}