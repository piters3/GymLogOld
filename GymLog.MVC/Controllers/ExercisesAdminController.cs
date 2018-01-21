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


        //public async Task<ActionResult> Create()
        //{
        //    try
        //    {
        //        var equipments = await ClientHelper.Instance.GetAsync<IEnumerable<EquipmentViewModel>>("/api/equipments", User.Identity.Name);
        //        var muscles = await ClientHelper.Instance.GetAsync<IEnumerable<EquipmentViewModel>>("/api/muscles", User.Identity.Name);
        //        ViewBag.Equipments = new SelectList(equipments, "asd", "zxc");
        //        ViewBag.Muscles = new SelectList(muscles, "cvb", "dgf");
        //    }
        //    catch (ApiException ex)
        //    {
        //        HandleBadRequest(ex);
        //        return View();
        //    }
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Create(ExerciseViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        await ClientHelper.Instance.PostAsync("/api/exercises", model, User.Identity.Name);
        //        TempData["message"] = string.Format("Ćwiczenie zostało dodane!");
        //        return RedirectToAction("Index");
        //    }
        //    ModelState.AddModelError("", "Popraw błędy formularza");
        //    return View(model);
        //}


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
    }
}