using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fzrain.Core.Domain;
using Fzrain.Core.Infrastructure;
using Fzrain.Service;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace Fzrain.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EditingPopup_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(userService.GetAllUsers().ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingPopup_Create([DataSourceRequest] DataSourceRequest request, User user)
        {
            if (user != null && ModelState.IsValid)
            {
                userService.InsertUser(user);
            }

            return Json(new[] { user }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingPopup_Update([DataSourceRequest] DataSourceRequest request, User user)
        {
            if (user != null && ModelState.IsValid)
            {
                userService.UpdateUser(user);
            }

            return Json(new[] { user }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingPopup_Destroy([DataSourceRequest] DataSourceRequest request, User user)
        {
            if (user != null)
            {
                userService.DeleteUser(user);
            }

            return Json(new[] { user }.ToDataSourceResult(request, ModelState));
        }

    }
}