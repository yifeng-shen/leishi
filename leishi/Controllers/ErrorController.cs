﻿using leishi.Web.Core;
using System.Web.Mvc;

namespace leishi.Web.Controllers
{
    public class ErrorController : CoreController
    {
        // GET: Error
        public ActionResult Index()
        {
            return View();
        }
    }
}