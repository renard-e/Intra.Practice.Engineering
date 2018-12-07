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
            JArray array = DbUsers.getAllRequirementsList();
            ViewData["list"] = array;
            return View();
        }

        public IActionResult changeStateRequest(String user, String id, String newState)
        {
            JObject obj = JObject.Parse(TempData.Peek("client").ToString());

            System.Diagnostics.Debug.WriteLine(TempData.Peek("client").ToString());
            if (obj["group"].ToString() != "Manager")
                return RedirectToAction("Index", "Home");

            System.Diagnostics.Debug.WriteLine("USER = " + user);
            System.Diagnostics.Debug.WriteLine("ID = " + id);
            System.Diagnostics.Debug.WriteLine("newState = " + newState);
            return RedirectToAction("Index", "Manager");
        }
    }
}