using System.Linq;
using System.Web.Mvc;
using Fzrain.Core.Data;
using Fzrain.Core.Domain.Permission;

namespace Fzrain.Web.Controllers
{
    public class LoginController : Controller
    {
	    private readonly IRepository<User> userService;
	    public LoginController(IRepository<User> userService)
	    {
		    this.userService = userService;
	    }

	    // GET: Login
        public ActionResult Index()
        {
            var user = Session["user"] as User;
            if (user!=null )
            {
                Response.Redirect("/User");
            }
            return View();
        }

	    public ActionResult In(string email, string password)
	    {
		  var user=  userService.Table.FirstOrDefault(u => u.UserName == email && u.Password == password);
	        if (user!=null)
	        {
	            Session["user"] = user;
	        }
		    return Content(user!=null ? "true" : "false");
	    }

        public ActionResult Out()
        {
            Session.Abandon();
            return RedirectToAction("Index");
        }
    }
}