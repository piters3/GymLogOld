using GymLog.MVC.Exceptions;
using GymLog.MVC.Helpers;
using GymLog.MVC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GymLog.MVC.Controllers
{
    [Authorize]
    public class WorkoutsAdminController : BaseApiController
    {
        public async Task<ActionResult> Index()
        {
            try
            {
                var workouts = await ClientHelper.Instance.GetAsync<IEnumerable<WorkoutViewModel>>("/api/workouts", User.Identity.Name);
                return View(workouts);
            }
            catch (ApiException ex)
            {
                HandleBadRequest(ex);
                return View();
            }
        }
    }
}