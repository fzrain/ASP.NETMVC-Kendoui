using System.Web.Mvc;

namespace Fzrain.Web.Areas.Demo
{
    public class DemoAreaRegistration : AreaRegistration
    {
        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
         "DemoRoute",
         "Demo1/{controller}/{action}/{id}",
         new { action = "Index", id = UrlParameter.Optional }
           );
        }

        public override string AreaName
        {
            get { return "Demo1"; }
        }
    }
}