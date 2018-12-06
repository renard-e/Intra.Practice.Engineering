using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Intra.Practice.Engineering.Controllers
{
    public class ManagerController : Controller
    {
        public IActionResult Index()
        {
            JObject obj = JObject.Parse(TempData.Peek("client").ToString());

            System.Diagnostics.Debug.WriteLine(TempData.Peek("client").ToString());
            if (obj["group"].ToString() != "Manager")
                return RedirectToAction("Index", "Home");
            JArray array = DbUsers.getAllRequirementsList("ListDayOff.json");
            ViewData["list"] = array;
            return View();
        }
    }
}