using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using Fzrain.Core.Domain.Permission;

namespace Fzrain.Web.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnAuthentication(AuthenticationContext filterContext)
        {           
            base.OnAuthentication(filterContext);
            var user= Session["user"] as User;
            if (user==null )
            {
                Response.Redirect("/Login");
            }
        }
    }
}