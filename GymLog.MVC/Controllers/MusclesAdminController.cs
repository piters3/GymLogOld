using GymLog.MVC.Exceptions;
using GymLog.MVC.Helpers;
using GymLog.MVC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GymLog.MVC.Controllers
{
    [Authorize]
    public class MusclesAdminController : BaseApiController
    {
        public async Task<ActionResult> Index()
        {
            try
            {
                var muscles = await ClientHelper.Instance.GetAsync<IEnumerable<MuscleViewModel>>("/api/Muscles", User.Identity.Name);
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
        public async Task<ActionResult> Create(MuscleViewModel model)
        {
            if (ModelState.IsValid)
            {
                await ClientHelper.Instance.PostAsync("/api/Muscles", model, User.Identity.Name);
                TempData["message"] = string.Format("Mięsień został dodany!");
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Popraw błędy formularza");
            return View(model);
        }
    }
}