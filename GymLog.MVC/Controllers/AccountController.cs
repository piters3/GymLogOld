using GymLog.MVC.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.DataHandler.Serializer;
using System.Security.Claims;
using System.Web;
using Newtonsoft.Json;

namespace GymLog.MVC.Controllers
{
    public class AccountController : Controller
    {
        string url = "http://localhost:53737/api/muscles";
        private HttpClient client;

        public AccountController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }




        //private async Task<string> GetToken(string userName, string password)
        //{
        //    var formContent = new FormUrlEncodedContent(new[]
        //    {
        //         new KeyValuePair<string, string>("grant_type", "password"),
        //         new KeyValuePair<string, string>("username", userName),
        //         new KeyValuePair<string, string>("password", password),
        //    });

        //    var result = await client.PostAsync("/Token", formContent);

        //    var responseJson = await result.Content.ReadAsStringAsync();
        //    var jObject = JObject.Parse(responseJson);
        //    return jObject.GetValue("access_token").ToString();
        //}

        public async Task<T> AuthenticateAsync<T>(string userName, string password)
        {
            var result = await client.PostAsync("/Token", new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("userName", userName),
                    new KeyValuePair<string, string>("password", password)
                }));

            string json = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(json);
        }

        private static async Task<string> GetRequest(string token, string apiBaseUri, string requestPath)
        {
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(apiBaseUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = await client.GetAsync(requestPath);
                var responseString = await response.Content.ReadAsStringAsync();
                return responseString;
            }
        }


        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            //var token = await GetToken(model.UserName, model.Password);

            var result = await AuthenticateAsync<SignInResult>(model.UserName, model.Password);

            if (!string.IsNullOrEmpty(result.AccessToken))
            {
                var token = new HttpCookie("token", result.AccessToken)
                {
                    HttpOnly = true
                };
                Response.Cookies.Add(token);

                var userID = new HttpCookie("userID", result.UserID)
                {
                    HttpOnly = true
                };
                Response.Cookies.Add(userID);

                return RedirectToAction("Index", "Equipments");
            }


            //var result = await AuthenticateAsync<SignInResult>(model.UserName, model.Password);

            //FormsAuthentication.SetAuthCookie(result.AccessToken, model.RememberMe);

            //var claims = new[]
            //{
            //        new Claim(ClaimTypes.Name, result.UserName),
            //        new Claim(ClaimTypes.NameIdentifier, result.UserName)
            //    };

            //var authTicket = new AuthenticationTicket(new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie), new AuthenticationProperties
            //{
            //    ExpiresUtc = result.Expires,
            //    IsPersistent = model.RememberMe,
            //    IssuedUtc = result.Issued,
            //    RedirectUri = returnUrl
            //});

            //byte[] userData = DataSerializers.Ticket.Serialize(authTicket);
            //byte[] protectedData = MachineKey.Protect(userData, new[] { "Microsoft.Owin.Security.Cookies.CookieAuthenticationMiddleware", DefaultAuthenticationTypes.ApplicationCookie, "v1" });
            //string protectedText = TextEncodings.Base64Url.Encode(protectedData);

            //Response.SetCookie(new HttpCookie("GymLog.Api.Auth")
            //{
            //    HttpOnly = true,
            //    Expires = result.Expires.UtcDateTime,
            //    Value = protectedText
            //});

            return View("LoginFailed");
        }

        [Authorize]
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();

            if (Response.Cookies["GymLog.Api.Auth"] != null)
            {
                var c = new HttpCookie("GymLog.Api.Auth") { Expires = DateTime.Now.AddDays(-1) };
                Response.Cookies.Add(c);
            }

            return RedirectToAction("Index", "Equipments");
        }
    }
}