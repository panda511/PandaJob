﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PandaJob.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Login() 
        {
            return View();
        }
    }
}