﻿using System;
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
    public class ManagerController : Controller
    {
        private readonly IntraContext _context;

        public ManagerController(IntraContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            JObject obj = JObject.Parse(TempData.Peek("client").ToString());

            System.Diagnostics.Debug.WriteLine(TempData.Peek("client").ToString());
            if (obj["group"].ToString() != "Manager")
                return RedirectToAction("Index", "Home");
            JArray array = DbUsers.getAllRequirementsList(_context);
            ViewData["list"] = array;
            return View();
        }

        public IActionResult changeStateRequest()
        {
            JObject obj = JObject.Parse(TempData.Peek("client").ToString());

            System.Diagnostics.Debug.WriteLine(TempData.Peek("client").ToString());
            if (obj["group"].ToString() != "Manager")
                return RedirectToAction("Index", "Home");
            if (!DbUsers.changeStateOneRequest(HttpContext.Request.Form["emailUser"].ToString(), HttpContext.Request.Form["idRequest"].ToString(), HttpContext.Request.Form["newState"].ToString(), _context))
                System.Diagnostics.Debug.WriteLine("ERROR : can't change state request");
            return RedirectToAction("Index", "Manager");
        }
    }
}