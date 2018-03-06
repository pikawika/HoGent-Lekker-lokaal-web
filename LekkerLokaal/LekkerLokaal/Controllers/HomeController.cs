using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LekkerLokaal.Models;
using LekkerLokaal.Models.Domain;
using Microsoft.AspNetCore.Authorization;
using LekkerLokaal.Models.HomeViewModels;

namespace LekkerLokaal.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBonRepository _bonRepository;
        private readonly ICategorieRepository _categorieRepository;
        private readonly IHandelaarRepository _handelaarRepository;
        public HomeController(IBonRepository bonRepository, ICategorieRepository categorieRepository, IHandelaarRepository handelaarRepository)
        {
            _bonRepository = bonRepository;
            _categorieRepository = categorieRepository;
            _handelaarRepository = handelaarRepository;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "Home";

            ViewBag.AlleBonnen = _bonRepository.GetAll().ToList();
            ViewBag.Top3Bonnen = _bonRepository.GetTop3().ToList();

            ViewBag.AlleCategorien = _categorieRepository.GetAll().ToList();
            ViewBag.Top9CategorieMetAantal = _categorieRepository.GetTop9WithAmount();

            ViewBag.AlleHandelaars = _handelaarRepository.GetAll().ToList();

            return View();
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


        [HttpGet]
        [AllowAnonymous]
        public IActionResult Zoeken()
        {
            ViewBag.AlleCategorien = _categorieRepository.GetAll().ToList();

            ViewBag.GefilterdeBonnen = _bonRepository.GetAll().ToList();
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Zoeken(ZoekenViewModel model)
        {
            ViewBag.AlleCategorien = _categorieRepository.GetAll().ToList();

            ViewBag.GefilterdeBonnen = _bonRepository.GetAll().ToList();
            switch (model.ZoekSoort)
            {
                //case "Alles":
                //    ViewBag.GefilterdeBonnen = _bonRepository.GetAlles(zoekKey);
                //    break;
                case "Ligging":
                    ViewBag.GefilterdeBonnen = _bonRepository.GetByLigging(model.ZoekKey);
                    break;
                case "Naam":
                    ViewBag.GefilterdeBonnen = _bonRepository.GetByNaam(model.ZoekKey);
                    break;
                case "Categorie":
                    ViewBag.GefilterdeBonnen = _bonRepository.GetByCategorie(model.ZoekKey);
                    break;
            }

            return View("~/Views/Home/Zoeken.cshtml");
        }
    }
}
