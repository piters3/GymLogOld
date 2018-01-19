using System.Web.Mvc;

namespace GymLog.MVC.Helpers
{
    public static class HtmlHelpers {
        public static string IsActive(this HtmlHelper htmlHelper, string action, string controller) {
            var routeData = htmlHelper.ViewContext.RouteData;

            var routeAction = routeData.Values["action"].ToString();
            var routeController = routeData.Values["controller"].ToString();

            var returnActive = (controller == routeController && action == routeAction);

            return returnActive ? "active" : "";
        }
    }
}