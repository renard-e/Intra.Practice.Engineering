using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Intra.Practice.Engineering.Controllers
{
    public class SignUpController : Controller
    {
        public IActionResult Index()
        {
            System.Diagnostics.Debug.WriteLine("---------------TEMP DATA : \n" + TempData["client"] + "\n--------------------------------");
            JObject obj = JObject.Parse(TempData["client"].ToString());

            System.Diagnostics.Debug.WriteLine("RECUPERER = " + obj["email"]);
            System.Diagnostics.Debug.WriteLine("RECUPERER = " + obj["group"]);

            return View();
        }

        public IActionResult SignUpSubmit()
        {
            if (String.IsNullOrEmpty(HttpContext.Request.Form["email"].ToString()) || String.IsNullOrEmpty(HttpContext.Request.Form["passwd"].ToString()) && String.IsNullOrEmpty(HttpContext.Request.Form["confirmpasswd"].ToString()))
                return RedirectToAction("Index", "SignUp");
            return RedirectToAction("Index", "Home");
        }
    }
}