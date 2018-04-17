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

        public IActionResult ZoekVerkochteCadeaubon()
        {
            return View();
        }

        public IActionResult HandelaarsVerzoeken()
        {
            return View();
        }

        public IActionResult HandelaarToevoegen()
        {
            return View();
        }

        public IActionResult HandelaarsOverzicht()
        {
            return View();
        }

        public IActionResult HandelaarBewerken()
        {
            return View();
        }
                

        public IActionResult CadeaubonVerzoeken()
        {
            return View();
        }

        public IActionResult CadeaubonToevoegen()
        {
            return View();
        }

        public IActionResult CadeaubonOverzicht()
        {
            return View();
        }

        public IActionResult CadeaubonBewerken()
        {
            return View();
        }

        public IActionResult LayoutSliderIndex()
        {
            return View();
        }

        public IActionResult LayoutAanbiedingen()
        {
            return View();
        }


    }
}