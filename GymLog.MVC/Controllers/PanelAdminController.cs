using System.Web.Mvc;

namespace GymLog.MVC.Controllers
{
    [Authorize]
    public class PanelAdminController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}