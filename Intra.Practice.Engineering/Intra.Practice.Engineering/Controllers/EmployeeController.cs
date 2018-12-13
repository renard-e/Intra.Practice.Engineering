using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Intra.Practice.Engineering.Data;
using Microsoft.EntityFrameworkCore;
using Intra.Practice.Engineering.Models;

namespace Intra.Practice.Engineering.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IntraContext _context;

        public EmployeeController(IntraContext context)
        {
            _context = context;
        }

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
            if (String.IsNullOrEmpty(HttpContext.Request.Form["selecteddatestart"].ToString()) || String.IsNullOrEmpty(HttpContext.Request.Form["selecteddateend"].ToString()) || String.IsNullOrEmpty(HttpContext.Request.Form["reason"].ToString()))
            {
                TempData["message"] = "Please fill all the blanks";
                return RedirectToAction("Index", "Employee");
            }
            if ((DbUsers.PostDayOff(HttpContext.Request.Form["selecteddatestart"].ToString(), HttpContext.Request.Form["selecteddateend"].ToString(), HttpContext.Request.Form["reason"].ToString(), (JObject.Parse(TempData.Peek("client").ToString())["email"]).ToString(), _context)) == false)
                System.Diagnostics.Debug.WriteLine("ERROR : can't post the day off");
            return RedirectToAction("Index", "Employee");
        }

        public IActionResult RequirementsList()
        {
            JObject obj = JObject.Parse(TempData.Peek("client").ToString());

            System.Diagnostics.Debug.WriteLine(TempData.Peek("client").ToString());
            if (obj["group"].ToString() != "Employee")
                return RedirectToAction("Index", "Home");
            JArray array = DbUsers.getRequirementsListFromUser((JObject.Parse(TempData.Peek("client").ToString())["email"]).ToString(), _context);
            ViewData["list"] = array;
            return View("RequirementsList");
        }

        public IActionResult RemoveItem(String Id)
        {
            JObject obj = JObject.Parse(TempData.Peek("client").ToString());

            System.Diagnostics.Debug.WriteLine(TempData.Peek("client").ToString());
            if (obj["group"].ToString() != "Employee")
                return RedirectToAction("Index", "Home");
            if (DbUsers.removeOneItemFromList((JObject.Parse(TempData.Peek("client").ToString())["email"]).ToString(), Id, _context) == false)
                System.Diagnostics.Debug.WriteLine("Error : can't remove Item");
            return RedirectToAction("RequirementsList", "Employee");
        }
    }
}