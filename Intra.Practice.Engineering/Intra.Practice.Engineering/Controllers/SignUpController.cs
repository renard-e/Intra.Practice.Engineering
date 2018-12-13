using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using Intra.Practice.Engineering.Models;
using Intra.Practice.Engineering.Data;

namespace Intra.Practice.Engineering.Controllers
{
    public class SignUpController : Controller
    {
        private readonly IntraContext _context;

        public SignUpController(IntraContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            JObject obj = JObject.Parse(TempData.Peek("client").ToString());

            System.Diagnostics.Debug.WriteLine("RECUPERER = \n" + obj.ToString());
            if (obj["group"].ToString() == "Employee")
                return RedirectToAction("Index", "Employee");
            if (obj["group"].ToString() == "Manager")
                return RedirectToAction("Index", "Manager");
            return View();
        }

        public IActionResult SignUpSubmit()
        {
            if (String.IsNullOrEmpty(HttpContext.Request.Form["email"].ToString()) || String.IsNullOrEmpty(HttpContext.Request.Form["passwd"].ToString()) && String.IsNullOrEmpty(HttpContext.Request.Form["confirmpasswd"].ToString()))
            {
                TempData["message"] = "Please fill all the blanks";
                return RedirectToAction("Index", "SignUp");
            }
            if (HttpContext.Request.Form["passwd"].ToString() != HttpContext.Request.Form["confirmpasswd"].ToString())
            {
                TempData["message"] = "Password and Confirm Password must be the same";
                return RedirectToAction("Index", "SignUp");
            }
            if (DbUsers.SignUpFunc(HttpContext.Request.Form["email"].ToString(), HttpContext.Request.Form["passwd"].ToString(), HttpContext.Request.Form["group"].ToString(), _context) == false)
            {
                TempData["message"] = "User already exist";
                return RedirectToAction("Index", "SignUp");
            }
            TempData["message"] = "";
            return RedirectToAction("Index", "SignIn");
        }
    }
}