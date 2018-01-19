using System.Web.Mvc;

namespace GymLog.MVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            var qwe = User.Identity.Name;
            var zxc = User.Identity.AuthenticationType;

            return View();
        }
    }
}