using GymLog.MVC.Helpers;
using GymLog.MVC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GymLog.MVC.Controllers
{
    [Authorize]
    public class MusclesController : Controller
    {
        public async Task<ActionResult> Index()
        {

            var muscles = await ClientHelper.Instance.GetAsync<IEnumerable<MuscleModel>>("/api/Muscles", User.Identity.Name);

            return View(muscles);
        }
        //public async Task<ActionResult> Index()
        //{
        //    var token = Request.Cookies["token"];
        //    if (token != null)
        //    {
        //        var value = token.Value;
        //        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", value);
        //    }

        //    var userID = Request.Cookies["userID"].Value;


        //    HttpResponseMessage response = await client.GetAsync(url);
        //    if (response.IsSuccessStatusCode)
        //    {
        //        var muscles = await response.Content.ReadAsAsync<List<MuscleModel>>();
        //        return View(muscles);
        //    }
        //    return Content("An error occurred.");
        //}
    }
}