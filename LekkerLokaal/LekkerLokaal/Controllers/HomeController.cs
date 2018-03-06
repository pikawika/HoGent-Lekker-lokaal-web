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

        public IActionResult Zoeken(string zoekKey, string zoekField)
        {
            ViewBag.AlleCategorien = _categorieRepository.GetAll().ToList();
            ViewBag.GefilterdeBonnen = _bonRepository.GetAll().ToList();

            switch (zoekField)
            {
                //case "Alles":
                //    ViewBag.GefilterdeBonnen = _bonRepository.GetAlles(zoekKey);
                //    break;
                case "Ligging":
                    ViewBag.GefilterdeBonnen = _bonRepository.GetByLigging(zoekKey);
                    break;
                case "Naam":
                    ViewBag.GefilterdeBonnen = _bonRepository.GetByNaam(zoekKey);
                    break;
                case "Categorie":
                    ViewBag.GefilterdeBonnen = _bonRepository.GetByCategorie(zoekKey);
                    break;
            }

            return View();
        }
    }
}
