using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Intra.Practice.Engineering.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Intra.Practice.Engineering.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            JObject obj = new JObject();

            obj.Add("email", "");
            obj.Add("group", "undefined");
            TempData["client"] = obj.ToString();
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
