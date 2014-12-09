using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fzrain.Core;
using Fzrain.Core.Domain;
using Fzrain.Core.Infrastructure;
using Fzrain.Service;
using Kendo.Mvc;
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

        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {

            if (request.Sorts .Count ==0)//默认按Id倒序排列
                request.Sorts.Add(new SortDescriptor { Member = "Id", SortDirection = ListSortDirection.Descending });
                   
            return Json(userService.GetAllUsers().ToDataSourceResult (request));
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
            var user= userService.GetById(id);
            if (user != null)
            {
                userService.DeleteUser(user);
            }

            return Json(new[] { user }.ToDataSourceResult(request, ModelState));
        }

    }
}