using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LekkerLokaal.Models;
using LekkerLokaal.Models.Domain;

namespace LekkerLokaal.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBonRepository _bonRepository;
        public HomeController(IBonRepository bonRepository)
        {
            _bonRepository = bonRepository;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "Home";
            return View(_bonRepository.GetAll().ToList());
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
