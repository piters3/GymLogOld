using GymLog.MVC.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GymLog.MVC.Controllers
{
    public class EquipmentsController : Controller
    {
        string url = "http://localhost:53737/api/equipments";
        private HttpClient client;


        public EquipmentsController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<ActionResult> Index()
        {
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var equipments = await response.Content.ReadAsAsync<List<EquipmentModel>>();
                return View(equipments);
            }
            return Content("An error occurred.");
        }
    }
}