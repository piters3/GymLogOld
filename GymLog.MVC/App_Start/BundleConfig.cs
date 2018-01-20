using System.Web.Optimization;

namespace GymLog.MVC
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            //USER CSS
            bundles.Add(new StyleBundle("~/UserCSS").Include(
                         "~/Content/User/bootstrap.min.css",
                         "~/Content/User/sticky-footer-navbar.css"));


            //USER JS
            bundles.Add(new ScriptBundle("~/UserJS").Include(
                         "~/Scripts/User/jquery-3.1.1.slim.min.js",
                         "~/Scripts/User/tether.min.js",
                         "~/Scripts/User/bootstrap.min.js"));
        }
    }
}
