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
    public class DaylogsAdminController : BaseApiController
    {
        public async Task<ActionResult> Index()
        {
            try
            {
                var workouts = await ClientHelper.Instance.GetAsync<IEnumerable<DaylogViewModel>>("/api/daylogs", User.Identity.Name);
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
                var workouts = await ClientHelper.Instance.GetAsync<IEnumerable<WorkoutViewModel>>("/api/workouts", User.Identity.Name);
                var exercises = await ClientHelper.Instance.GetAsync<IEnumerable<ExerciseViewModel>>("/api/exercises", User.Identity.Name);
                ViewBag.UsersList = new SelectList(users, "Id", "UserName");

                //var workoutsList = new MultiSelectList(exercises);
                //workoutsList.DataValueField = 

                ViewBag.ExercisesList = new MultiSelectList(exercises, "Id", "Name");
                ViewBag.WorkoutsList = new MultiSelectList(workouts, "Id", "DisplayValue");
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
        public async Task<ActionResult> Create(DaylogViewModel model, FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int i = 0;
                    List<WorkoutViewModel> list = new List<WorkoutViewModel>();
                    foreach (var id in model.WorkoutsIds)
                    {
                        var workout = await ClientHelper.Instance.GetAsync<WorkoutViewModel>($"/api/workouts/{id}", User.Identity.Name);
                        WorkoutViewModel wk = new WorkoutViewModel()
                        {
                            //Id = workout.Id,
                            //ExerciseId = workout.ExerciseId,
                            Exercise = workout.Exercise,
                            Sets = model.ExerciseDetails[i],
                            Reps = model.ExerciseDetails[i+1],
                            Weight = model.ExerciseDetails[i+2],
                            UserId = model.UserId
                        };
                        list.Add(wk);
                        i += 3;
                    }

                    model.Workouts = list;
                    await ClientHelper.Instance.PostAsync("/api/daylogs", model, User.Identity.Name);
                    TempData["message"] = string.Format("Plan treningowy został dodany!");
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
                var daylog = await ClientHelper.Instance.GetAsync<DaylogViewModel>($"/api/daylogs/{id}", User.Identity.Name);
                if (daylog == null)
                {
                    return HttpNotFound();
                }
                return View(daylog);
            }
            catch (ApiException ex)
            {
                HandleBadRequest(ex);
                return View();
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await ClientHelper.Instance.DeleteAsync<DaylogViewModel>($"/api/daylogs/{id}", User.Identity.Name);
            }
            catch (ApiException ex)
            {
                HandleBadRequest(ex);
                return View();
            }
            TempData["message"] = string.Format("Plan treningowy został usunięty!");
            return RedirectToAction("Index");
        }
    }
}