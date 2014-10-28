using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fzrain.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
            
        }

        public ActionResult Update()
        {
            return View();
        }

        public ActionResult AboutMe()
        {
            throw new NotImplementedException();
        }

        public ActionResult Blog()
        {
            return View();
        }

        public ActionResult Forum()
        {
            throw new NotImplementedException();
        }
        
        public ActionResult FrameAll()
        {
            return View();
        }
    }
}