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
            try
            {
                Debug.WriteLine(TempData.Peek("client").ToString());
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("ERROR : " + ex.ToString());
                JObject obj = new JObject();

                obj.Add("email", "");
                obj.Add("group", "undefined");
                TempData["client"] = obj.ToString();
            }
            TempData["message"] = "";
            return View();
        }

        public IActionResult Logout()
        {
            JObject obj = new JObject();

            obj.Add("email", "");
            obj.Add("group", "undefined");
            TempData["client"] = obj.ToString();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
