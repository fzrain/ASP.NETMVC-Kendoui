using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fzrain.Core.Data;
using Fzrain.Core.Domain.Permission;
using Fzrain.Service.UserManage;

namespace Fzrain.Web.Controllers
{
    public class LoginController : Controller
    {
	    private IRepository<User> userService;
	    public LoginController(IRepository<User> userService)
	    {
		    this.userService = userService;
	    }

	    // GET: Login
        public ActionResult Index()
        {
            return View();
        }

	    public ActionResult In(string email, string password)
	    {
		  var user=  userService.Table.FirstOrDefault(u => u.UserName == email && u.Password == password);
		    return Content(user!=null ? "true" : "false");
	    }
    }
}