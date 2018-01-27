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


        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                var workout = await ClientHelper.Instance.GetAsync<WorkoutViewModel>($"/api/workouts/{id}", User.Identity.Name);
                if (workout == null)
                {
                    return HttpNotFound();
                }
                return View(workout);
            }
            catch (ApiException ex)
            {
                HandleBadRequest(ex);
                return View();
            }
        }


        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                var workout = await ClientHelper.Instance.GetAsync<WorkoutViewModel>($"/api/workouts/{id}", User.Identity.Name);
                if (workout == null)
                {
                    return HttpNotFound();
                }
                var exercises = await ClientHelper.Instance.GetAsync<IEnumerable<ExerciseViewModel>>("/api/exercises", User.Identity.Name);
                var users = await ClientHelper.Instance.GetAsync<IEnumerable<UserShortViewModel>>("/api/users", User.Identity.Name);
                ViewBag.UsersList = new SelectList(users, "Id", "UserName", workout.UserId);     //nie ustawia selected
                ViewBag.ExercisesList = new SelectList(exercises, "Id", "Name", workout.Exercise.Id);     //nie ustawia selected
                return View(workout);
            }
            catch (ApiException ex)
            {
                HandleBadRequest(ex);
                return View();
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(WorkoutViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await ClientHelper.Instance.PutAsync($"/api/workouts/{model.Id}", model, User.Identity.Name);
                    TempData["message"] = string.Format("Trening został zedytowany!");
                    return RedirectToAction("Index");
                }
                catch (ApiException ex)
                {
                    HandleBadRequest(ex);
                    return View();
                }
            }
            ModelState.AddModelError("", "Popraw błędy formularza");
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await ClientHelper.Instance.DeleteAsync<WorkoutViewModel>($"/api/workouts/{id}", User.Identity.Name);
            }
            catch (ApiException ex)
            {
                HandleBadRequest(ex);
                return View();
            }
            TempData["message"] = string.Format("Trening został usunięty!");
            return RedirectToAction("Index");
        }
    }
}