using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fzrain.Core.Domain;
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
      

        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(userService.GetAllUsers(10,0).ToDataSourceResult(request),JsonRequestBehavior.AllowGet);
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
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, User  user)
        {
            if (user != null && ModelState.IsValid)
            {
                var newUser = userService.GetAllUsers(10, 0).Where(u => u.Id == user.Id).SingleOrDefault();
                newUser.UserName  = user.UserName ;
                newUser.Password = user.Password;
                userService.UpdateUser(newUser);
            }

            return Json(new[] { user }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Destroy([DataSourceRequest] DataSourceRequest request, User  user)
        {
            if (user != null)
            {

                userService.DeleteUser(userService.GetAllUsers(10,0).Where(u=>u.Id==user.Id).SingleOrDefault());
            }

            return Json(new[] { user }.ToDataSourceResult(request, ModelState));
        }
    }
}