﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Intra.Practice.Engineering.Controllers
{
    public class SignInController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SignInSubmit()
        {
            System.Diagnostics.Debug.WriteLine("-------------------------------------\nemail : " + HttpContext.Request.Form["email"] + "\npassword : " + HttpContext.Request.Form["passwd"] + "\n-------------------------------------");
            return View("../Home/Index");
        }
    }
}