using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Fzrain.Core.Infrastructure;

namespace Fzrain.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
       //     StackExchange.Profiling.EntityFramework6.MiniProfilerEF6.Initialize();//监控sql性能
            EngineContext.Initialize(false);
         
        }

        //protected void Application_BeginRequest()
        //{
        //    if (Request.IsLocal)
        //    {
        //        MiniProfiler.Start();
        //    }
            
        //}

        //protected void Application_EndRequest()
        //{
        //    MiniProfiler.Stop();
        //}
    }
}
