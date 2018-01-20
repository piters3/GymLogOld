using GymLog.MVC.Exceptions;
using GymLog.MVC.Helpers;
using GymLog.MVC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GymLog.MVC.Controllers
{
    [Authorize]
    public class UsersAdminController : BaseApiController
    {
        public async Task<ActionResult> Index()
        {
            try
            {
                var users = await ClientHelper.Instance.GetAsync<IEnumerable<UserViewModel>>("/api/Users", User.Identity.Name);
                return View(users);
            }
            catch (ApiException ex)
            {
                HandleBadRequest(ex);
                return View();
            }

        }
    }
}