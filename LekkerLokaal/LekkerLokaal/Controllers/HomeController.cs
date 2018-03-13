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
            ViewBag.AlleBonnen = _bonRepository.GetAll().ToList();
            ViewBag.Top3Bonnen = _bonRepository.GetTop3().ToList();
            ViewData["AlleBonnen"] = _bonRepository.GetAll().ToList();
            ViewData["Top3Bonnen"] = _bonRepository.GetTop3().ToList();
            ViewData["AlleCategorien"] = _categorieRepository.GetAll().ToList();
            ViewData["Top9CategorieMetAantal"] = _categorieRepository.GetTop9WithAmount();
            ViewData["AlleHandelaars"] = _handelaarRepository.GetAll().ToList();
            ViewBag.Top9CategorieMetAantal = _categorieRepository.GetTop9WithAmount();

            ViewBag.AlleHandelaars = _handelaarRepository.GetAll().ToList();

            return View();
        }

        public IActionResult About()
        {
            ViewData["AlleCategorien"] = _categorieRepository.GetAll().ToList();
            return View();
        }

        public IActionResult Error()
        {
            ViewData["AlleCategorien"] = _categorieRepository.GetAll().ToList();
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Zoeken(string ZoekSoort = null, string ZoekKey = null)
        {
            ViewData["AlleCategorien"] = _categorieRepository.GetAll().ToList();
            if (ZoekSoort != null && ZoekKey != null)
            {
                switch (ZoekSoort)
                {
                    case "Alles":
                        ViewBag.GefilterdeBonnen = _bonRepository.GetAlles(ZoekKey);
                        break;
                    case "Ligging":
                        ViewBag.GefilterdeBonnen = _bonRepository.GetByLigging(ZoekKey);
                        break;
                    case "Naam":
                        ViewBag.GefilterdeBonnen = _bonRepository.GetByNaam(ZoekKey);
                        break;
                    case "Categorie":
                        ViewBag.GefilterdeBonnen = _bonRepository.GetByCategorie(ZoekKey);
                        break;
                    default:
                        ViewBag.GefilterdeBonnen = _bonRepository.GetAll();
                        break;
                }
                ViewBag.ZoekOpdracht = ZoekKey + " in " + ZoekSoort;
            }
            else
            {
                ZoekSoort = HttpContext.Request.Form["ZoekSoort"];
                ZoekKey = HttpContext.Request.Form["ZoekKey"];

                switch (ZoekSoort)
                {
                    case "Alles":
                        ViewBag.GefilterdeBonnen = _bonRepository.GetAlles(ZoekKey);
                        break;
                    case "Ligging":
                        ViewBag.GefilterdeBonnen = _bonRepository.GetByLigging(ZoekKey);
                        break;
                    case "Naam":
                        ViewBag.GefilterdeBonnen = _bonRepository.GetByNaam(ZoekKey);
                        break;
                    case "Categorie":
                        ViewBag.GefilterdeBonnen = _bonRepository.GetByCategorie(ZoekKey);
                        break;
                    default:
                        ViewBag.GefilterdeBonnen = _bonRepository.GetAll();
                        break;
                }

                ViewBag.ZoekOpdracht = ZoekKey + " in " + ZoekSoort;
            }
            return View();
        }
    }
}
