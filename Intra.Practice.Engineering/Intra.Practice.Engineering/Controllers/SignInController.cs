using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Intra.Practice.Engineering.Controllers
{
    public class SignInController : Controller
    {
        public IActionResult Index()
        {
            JObject obj = JObject.Parse(TempData.Peek("client").ToString());

            System.Diagnostics.Debug.WriteLine("RECUPERER SIGNIN = \n" + obj.ToString());
            if (obj["group"].ToString() == "Employee")
                return RedirectToAction("Index", "Employee");
            if (obj["group"].ToString() == "Manager")
                return RedirectToAction("Index", "Manager");
            return View();
        }

        public IActionResult SignInSubmit()
        {
            TempData["message"] = "";
            if (String.IsNullOrEmpty(HttpContext.Request.Form["email"].ToString()) || String.IsNullOrEmpty(HttpContext.Request.Form["passwd"].ToString()))
            {
                TempData["message"] = "Please fill all the blanks";
                return RedirectToAction("Index", "SignIn");
            }
            String group = DbUsers.SignInFunc(HttpContext.Request.Form["email"].ToString(), HttpContext.Request.Form["passwd"].ToString());
            if (group == "undefined")
            {
                TempData["message"] = "Bad User name or Password";
                return RedirectToAction("Index", "SignIn");
            }
            if (group == "Employee")
            {
                JObject obj = JObject.Parse(TempData.Peek("client").ToString());

                obj["group"] = "Employee";
                obj["email"] = HttpContext.Request.Form["email"].ToString();
                TempData["client"] = obj.ToString();
                return RedirectToAction("Index", "Employee");
            }
            if (group == "Manager")
            {
                JObject obj = JObject.Parse(TempData.Peek("client").ToString());

                obj["group"] = "Manager";
                obj["email"] = HttpContext.Request.Form["email"].ToString();
                TempData["client"] = obj.ToString();
                return RedirectToAction("Index", "Manager");
            }
            return RedirectToAction("Index", "Home");
        }
    }
}