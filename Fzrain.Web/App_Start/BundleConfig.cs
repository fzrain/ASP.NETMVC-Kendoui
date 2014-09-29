using System.Web.Optimization;

namespace Fzrain.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            "~/Scripts/jquery-2.*"));
            bundles.Add(new StyleBundle("~/bundles/homeStyle").Include(
                "~/Content/Home/css/bootstrap.css",
               "~/Content/Home/css/style.css"));
        }

    }
}
