using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Intra.Practice.Engineering.Models;

namespace Intra.Practice.Engineering.Controllers
{
    public class HomeController : Controller
    {
        private static ClientServer client = new ClientServer();

        public IActionResult Index()
        {
            //client.setEmail("MERDE"); // ici ca boucle inf 
            //TempData["client"] = client;
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
