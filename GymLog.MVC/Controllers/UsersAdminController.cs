using GymLog.MVC.Exceptions;
using GymLog.MVC.Helpers;
using GymLog.MVC.Models;
using System.Collections.Generic;
using System.Net;
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
                var users = await ClientHelper.Instance.GetAsync<IEnumerable<UserViewModel>>("/api/users", User.Identity.Name);
                return View(users);
            }
            catch (ApiException ex)
            {
                HandleBadRequest(ex);
                return View();
            }
        }


        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                var user = await ClientHelper.Instance.GetAsync<UserViewModel>($"/api/users/{id}", User.Identity.Name);
                if (user == null)
                {
                    return HttpNotFound();
                }
                return View(user);
            }
            catch (ApiException ex)
            {
                HandleBadRequest(ex);
                return View();
            }
        }
    }
}