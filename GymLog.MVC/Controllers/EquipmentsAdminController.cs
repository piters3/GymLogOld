using GymLog.MVC.Exceptions;
using GymLog.MVC.Helpers;
using GymLog.MVC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GymLog.MVC.Controllers
{
    public class EquipmentsAdminController : BaseApiController
    {
        public async Task<ActionResult> Index()
        {
            try
            {
                var muscles = await ClientHelper.Instance.GetAsync<IEnumerable<EquipmentViewModel>>("/api/Equipments", User.Identity.Name);
                return View(muscles);
            }
            catch (ApiException ex)
            {
                HandleBadRequest(ex);
                return View();
            }

        }
    }
}