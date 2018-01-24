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


        public async Task<ActionResult> Create()
        {
            try
            {
                var users = await ClientHelper.Instance.GetAsync<IEnumerable<UserViewModel>>("/api/users", User.Identity.Name);
                var exercises = await ClientHelper.Instance.GetAsync<IEnumerable<ExerciseViewModel>>("/api/exercises", User.Identity.Name);
                ViewBag.UsersList = new SelectList(users, "Id", "UserName");
                ViewBag.ExercisesList = new SelectList(exercises, "Id", "Name");
            }
            catch (ApiException ex)
            {
                HandleBadRequest(ex);
                return View();
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(WorkoutViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await ClientHelper.Instance.PostAsync("/api/workouts", model, User.Identity.Name);
                    TempData["message"] = string.Format("Trening został dodany!");
                    return RedirectToAction("Index");
                }
                catch (ApiException ex)
                {
                    HandleBadRequest(ex);
                }

            }
            ModelState.AddModelError("", "Popraw błędy formularza");
            return View(model);
        }
    }
}