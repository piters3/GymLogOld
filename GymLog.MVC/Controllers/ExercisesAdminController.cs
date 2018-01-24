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
    public class ExercisesAdminController : BaseApiController
    {
        public async Task<ActionResult> Index()
        {
            try
            {
                var ex = await ClientHelper.Instance.GetAsync<IEnumerable<ExerciseViewModel>>("/api/exercises", User.Identity.Name);
                return View(ex);
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
                var equipments = await ClientHelper.Instance.GetAsync<IEnumerable<EquipmentViewModel>>("/api/equipments", User.Identity.Name);
                var muscles = await ClientHelper.Instance.GetAsync<IEnumerable<MuscleViewModel>>("/api/muscles", User.Identity.Name);
                ViewBag.EquipmentsList = new SelectList(equipments, "Id", "Name");
                ViewBag.MusclesList = new SelectList(muscles, "Id", "Name");
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
        public async Task<ActionResult> Create(ExerciseViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await ClientHelper.Instance.PostAsync("/api/exercises", model, User.Identity.Name);
                    TempData["message"] = string.Format("Ćwiczenie zostało dodane!");
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
                var ex = await ClientHelper.Instance.GetAsync<ExerciseViewModel>($"/api/exercises/{id}", User.Identity.Name);
                if (ex == null)
                {
                    return HttpNotFound();
                }
                return View(ex);
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
                var ex = await ClientHelper.Instance.GetAsync<ExerciseViewModel>($"/api/exercises/{id}", User.Identity.Name);
                if (ex == null)
                {
                    return HttpNotFound();
                }
                var equipments = await ClientHelper.Instance.GetAsync<IEnumerable<EquipmentViewModel>>("/api/equipments", User.Identity.Name);
                var muscles = await ClientHelper.Instance.GetAsync<IEnumerable<MuscleViewModel>>("/api/muscles", User.Identity.Name);
                ViewBag.EquipmentsList = new SelectList(equipments, "Id", "Name", ex.Equipment.Id);     //nie ustawia selected
                ViewBag.MusclesList = new SelectList(muscles, "Id", "Name", ex.Muscle.Id);              //nie ustawia selected

                return View(ex);
            }
            catch (ApiException ex)
            {
                HandleBadRequest(ex);
                return View();
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ExerciseViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await ClientHelper.Instance.PutAsync($"/api/exercises/{model.Id}", model, User.Identity.Name);
                    TempData["message"] = string.Format("Ćwiczenie zostało zedytowane!");
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
                await ClientHelper.Instance.DeleteAsync<ExerciseViewModel>($"/api/exercises/{id}", User.Identity.Name);
            }
            catch (ApiException ex)
            {
                HandleBadRequest(ex);
                return View();
            }
            TempData["message"] = string.Format("Ćwiczenie zostało usunięte!");
            return RedirectToAction("Index");
        }
    }
}