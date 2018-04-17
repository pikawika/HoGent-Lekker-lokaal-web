using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace LekkerLokaal.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        public IActionResult VerkochteCadeaubonnen()
        {
            return View();
        }

        public IActionResult UitbetaaldeCadeaubonnen()
        {
            return View();
        }
    }
}