using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fzrain.Core.Domain;
using Fzrain.Service;
using Fzrain.Service.Lol;
using Kendo.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace Fzrain.Web.Controllers
{
    public class LolController : Controller
    {
        private readonly ILolService lolService;
        private readonly IUserService userService;
        public LolController(ILolService lolService, IUserService userService)
        {
            this.lolService = lolService;
            this.userService = userService;
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

            return Json(lolService.GetAllBattles().Select(b=>new{b.BattleType ,b.Duration,b.GameId ,b.Id ,b.StartTime  }).ToDataSourceResult(request));
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, User user)
        {
            if (user != null && ModelState.IsValid)
            {
                userService.InsertUser(user);
            }

            return Json(new[] { user }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, User user)
        {

            if (user != null && ModelState.IsValid)
            {
                var u = userService.GetById(user.Id);
                u.UserName = user.UserName;
                u.Password = user.Password;
                u.Birthday = user.Birthday;
                u.Gender = user.Gender;
                userService.UpdateUser(u);
            }

            return Json(new[] { user }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Destroy([DataSourceRequest] DataSourceRequest request, int id)
        {
            var user = userService.GetById(id);
            if (user != null)
            {
                userService.DeleteUser(user);
            }

            return Json(new[] { user }.ToDataSourceResult(request, ModelState));
        }
    }
}