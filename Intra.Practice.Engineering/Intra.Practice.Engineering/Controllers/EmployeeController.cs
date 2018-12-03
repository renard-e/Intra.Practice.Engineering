using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Intra.Practice.Engineering.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            JObject obj = JObject.Parse(TempData.Peek("client").ToString());

            System.Diagnostics.Debug.WriteLine(TempData.Peek("client").ToString());
            if (obj["group"].ToString() != "Employee")
                return RedirectToAction("Index", "Home");
            return View();
        }

        public IActionResult PushCalendar()
        {
            System.Diagnostics.Debug.WriteLine("SELECTEDDATE 1 = " + HttpContext.Request.Form["selecteddatestart"].ToString());
            System.Diagnostics.Debug.WriteLine("SELECTEDDATE 2 = " + HttpContext.Request.Form["selecteddateend"].ToString());
            System.Diagnostics.Debug.WriteLine("REASON = " + HttpContext.Request.Form["reason"].ToString());
            return RedirectToAction("Index", "Employee");
        }
    }
}