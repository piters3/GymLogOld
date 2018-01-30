using GymLog.MVC.Exceptions;
using GymLog.MVC.Helpers;
using GymLog.MVC.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.DataHandler.Serializer;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace GymLog.MVC.Controllers
{
    public class AccountController : BaseApiController
    {
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var result = await ClientHelper.Instance.AuthenticateAsync<SignInResult>(model.UserName, model.Password);

                FormsAuthentication.SetAuthCookie(result.AccessToken, model.RememberMe);

                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, "GymLog.WebApi.Auth"),
                    new Claim(ClaimTypes.NameIdentifier, "GymLog.WebApi.Auth")
                };

                var authTicket = new AuthenticationTicket(new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie), new AuthenticationProperties
                {
                    ExpiresUtc = result.Expires,
                    IsPersistent = model.RememberMe,
                    IssuedUtc = result.Issued,
                    RedirectUri = returnUrl
                });

                byte[] userData = DataSerializers.Ticket.Serialize(authTicket);
                byte[] protectedData = MachineKey.Protect(userData, new[] { "Microsoft.Owin.Security.Cookies.CookieAuthenticationMiddleware", DefaultAuthenticationTypes.ApplicationCookie, "v1" });
                string protectedText = TextEncodings.Base64Url.Encode(protectedData);
                Response.SetCookie(new HttpCookie("GymLog.WebApi.Auth")
                {
                    HttpOnly = true,
                    Expires = result.Expires.UtcDateTime,
                    Value = protectedText
                });

                return RedirectToLocal(returnUrl);
            }
            catch (ApiException ex)
            {
                HandleBadRequest(ex);

                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                throw;
            }
        }


        public ActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await ClientHelper.Instance.PostAsync("/api/Account/Register", model);
                return RedirectToAction("Index", "Home");
            }
            catch (ApiException ex)
            {
                HandleBadRequest(ex);
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                throw;
            }
        }


        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            if (Response.Cookies["GymLog.WebApi.Auth"] != null)
            {
                var c = new HttpCookie("GymLog.WebApi.Auth") { Expires = DateTime.Now.AddDays(-1) };
                Response.Cookies.Add(c);
            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<ActionResult> Manage()
        {
            try
            {
                var userInfo = await ClientHelper.Instance.GetAsync<UserViewModel>("/api/Account/UserInfo", User.Identity.Name);
                return View(userInfo);
            }
            catch (ApiException ex)
            {
                HandleBadRequest(ex);
            }
            return View();
        }


        public async Task<ActionResult> Workouts()
        {
            try
            {
                var userWorkouts = await ClientHelper.Instance.GetAsync<IEnumerable<WorkoutViewModel>>("/api/Account/UserWorkouts", User.Identity.Name);
                return View(userWorkouts);
            }
            catch (ApiException ex)
            {
                HandleBadRequest(ex);
            }
            return View();
        }


        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}