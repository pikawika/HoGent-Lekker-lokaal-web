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
            ViewBag.Top3Bonnen = _bonRepository.GetTop3(_bonRepository.GetAll()).ToList();

            ViewBag.AlleCategorien = _categorieRepository.GetAll().ToList();
            ViewBag.Top9CategorieMetAantal = _categorieRepository.GetTop9WithAmount();

            ViewBag.AlleHandelaars = _handelaarRepository.GetAll().ToList();

            return View();
        }

        public IActionResult About()
        {
            ViewBag.AlleCategorien = _categorieRepository.GetAll().ToList();
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Zoeken(string ZoekSoort = null, string ZoekKey = null, string Categorie = null, string Ligging = null, string MaxStartPrijs = null )
        {
            ViewBag.AlleCategorien = _categorieRepository.GetAll().ToList();

            if (!string.IsNullOrEmpty(ZoekKey))
            {
                switch (ZoekSoort)
                {
                    case "Alles":
                        ViewBag.GefilterdeBonnen = _bonRepository.GetAlles(ZoekKey, _bonRepository.GetAll());
                        break;
                    case "Ligging":
                        ViewBag.GefilterdeBonnen = _bonRepository.GetByLigging(ZoekKey, _bonRepository.GetAll());
                        break;
                    case "Naam":
                        ViewBag.GefilterdeBonnen = _bonRepository.GetByNaam(ZoekKey, _bonRepository.GetAll());
                        break;
                    case "Categorie":
                        ViewBag.GefilterdeBonnen = _bonRepository.GetByCategorie(ZoekKey, _bonRepository.GetAll());
                        break;
                    default:
                        ViewBag.GefilterdeBonnen = _bonRepository.GetAll();
                        break;
                }
                ViewBag.ZoekOpdracht = ZoekKey + " in " + ZoekSoort;

                if (!string.IsNullOrEmpty(Categorie) && Categorie != "*")
                {
                    string input = Categorie;
                    ViewBag.GefilterdeBonnen = _bonRepository.GetByCategorie(input, ViewBag.GefilterdeBonnen);
                    ViewBag.ZoekOpdracht = ViewBag.ZoekOpdracht + ", met categorie " + input;
                }
                if (!string.IsNullOrEmpty(Ligging) && Ligging != "*")
                {
                    string input = Ligging;
                    ViewBag.GefilterdeBonnen = _bonRepository.GetByLigging(input, ViewBag.GefilterdeBonnen);
                    ViewBag.ZoekOpdracht = ViewBag.ZoekOpdracht + ", met ligging " + input;
                }
                if (!string.IsNullOrEmpty(MaxStartPrijs))
                {
                    int input = int.Parse(MaxStartPrijs);
                    ViewBag.GefilterdeBonnen = _bonRepository.GetByPrijs(input, ViewBag.GefilterdeBonnen);
                    ViewBag.ZoekOpdracht = ViewBag.ZoekOpdracht + ", met maximum prijs " + input;
                }
            }
            else
            {
                ViewBag.GefilterdeBonnen = _bonRepository.GetAll();
                ViewBag.ZoekOpdracht = "Overzicht van alle bons";

                if (!string.IsNullOrEmpty(Categorie) && Categorie != "*")
                {
                    string input = Categorie;
                    ViewBag.GefilterdeBonnen = _bonRepository.GetByCategorie(input, ViewBag.GefilterdeBonnen);
                    ViewBag.ZoekOpdracht = ViewBag.ZoekOpdracht + ", met categorie " + input;
                }
                if (!string.IsNullOrEmpty(Ligging) && Ligging != "*")
                {
                    string input = Ligging;
                    ViewBag.GefilterdeBonnen = _bonRepository.GetByLigging(input, ViewBag.GefilterdeBonnen);
                    ViewBag.ZoekOpdracht = ViewBag.ZoekOpdracht + ", met ligging " + input;
                }
                if (!string.IsNullOrEmpty(MaxStartPrijs))
                {
                    int input = int.Parse(MaxStartPrijs);
                    ViewBag.GefilterdeBonnen = _bonRepository.GetByPrijs(input, ViewBag.GefilterdeBonnen);
                    ViewBag.ZoekOpdracht = ViewBag.ZoekOpdracht + ", met maximum prijs " + input;
                }

            }

            return View();
        }
    }
}
