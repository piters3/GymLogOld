﻿using System.Web.Mvc;

namespace GymLog.MVC.Controllers
{
    public class HomeController : BaseApiController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}