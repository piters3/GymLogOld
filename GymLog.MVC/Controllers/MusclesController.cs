using GymLog.MVC.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Security.Claims;

namespace GymLog.MVC.Controllers
{
    public class MusclesController : Controller
    {
        string url = "http://localhost:53737/api/muscles";
        private HttpClient client;

        public MusclesController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        
        }

        public async Task<ActionResult> Index()
        {
            var token = Request.Cookies["token"];
            if (token != null)
            {
                var value = token.Value;
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", value);
            }

            var userID = Request.Cookies["userID"].Value;


            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var muscles = await response.Content.ReadAsAsync<List<MuscleModel>>();
                return View(muscles);
            }
            return Content("An error occurred.");
        }
    }
}