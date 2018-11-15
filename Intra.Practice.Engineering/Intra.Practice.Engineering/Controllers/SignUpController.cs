using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Intra.Practice.Engineering.Controllers
{
    public class SignUpController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SignUpSubmit()
        {
            System.Diagnostics.Debug.WriteLine("-------------------------------------\n" + HttpContext.Request.Form["email"] + "\n-------------------------------------");
            System.Diagnostics.Debug.WriteLine("EMAIL SESSION = " + ((ClientServer)TempData["client"]).getEmail());
            return View("../Home/Index");
        }
    }
}