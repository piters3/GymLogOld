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
    public class EquipmentsAdminController : BaseApiController
    {
        public async Task<ActionResult> Index()
        {
            try
            {
                var muscles = await ClientHelper.Instance.GetAsync<IEnumerable<EquipmentViewModel>>("/api/equipments", User.Identity.Name);
                return View(muscles);
            }
            catch (ApiException ex)
            {
                HandleBadRequest(ex);
                return View();
            }

        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(EquipmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await ClientHelper.Instance.PostAsync("/api/equipments", model, User.Identity.Name);
                    TempData["message"] = string.Format("Sprzęt został dodany!");
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
                var eq = await ClientHelper.Instance.GetAsync<EquipmentViewModel>($"/api/equipments/{id}", User.Identity.Name);
                if (eq == null)
                {
                    return HttpNotFound();
                }
                return View(eq);
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
                var eq = await ClientHelper.Instance.GetAsync<EquipmentViewModel>($"/api/equipments/{id}", User.Identity.Name);
                if (eq == null)
                {
                    return HttpNotFound();
                }
                return View(eq);
            }
            catch (ApiException ex)
            {
                HandleBadRequest(ex);
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EquipmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await ClientHelper.Instance.PutAsync($"/api/equipments/{model.Id}", model, User.Identity.Name);
                    TempData["message"] = string.Format("Sprzęt został zedytowany!");
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


        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                var eq = await ClientHelper.Instance.GetAsync<EquipmentViewModel>($"/api/equipments/{id}", User.Identity.Name);
                if (eq == null)
                {
                    return HttpNotFound();
                }
                return View(eq);
            }
            catch (ApiException ex)
            {
                HandleBadRequest(ex);
                return View();
            }
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await ClientHelper.Instance.DeleteAsync<EquipmentViewModel>($"/api/equipments/{id}", User.Identity.Name);
            }
            catch (ApiException ex)
            {
                HandleBadRequest(ex);
                return View();
            }
            TempData["message"] = string.Format("Sprzęt został usunięty!");
            return RedirectToAction("Index");
        }
    }
}