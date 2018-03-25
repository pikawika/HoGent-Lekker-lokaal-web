using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LekkerLokaal.Filters;
using LekkerLokaal.Models.WinkelwagenViewModels;
using LekkerLokaal.Models.Domain;
using Microsoft.AspNetCore.Mvc;

namespace LekkerLokaal.Controllers
{
    [ServiceFilter(typeof(CartSessionFilter))]
    public class WinkelwagenController : Controller
    {
        private readonly ICategorieRepository _categorieRepository;
        private readonly IBonRepository _bonRepository;

        public WinkelwagenController(ICategorieRepository categorieRepository, IBonRepository bonRepository)
        {
            _categorieRepository = categorieRepository;
            _bonRepository = bonRepository;
        }

        public IActionResult Index(Winkelwagen winkelwagen)
        {
            ViewData["Navbar"] = "None";
            ViewData["AlleCategorien"] = _categorieRepository.GetAll().ToList();
            return View(new IndexViewModel(winkelwagen.WinkelwagenLijnen, winkelwagen.TotaleWaarde));
        }

        [HttpGet]
        public IActionResult Add(int Id, decimal Prijs, int Aantal, Winkelwagen winkelwagen)
        {
            Bon bon = _bonRepository.GetByBonId(Id);
            if (bon != null)
            {
                winkelwagen.VoegLijnToe(bon, Aantal, Prijs);
            }
            return RedirectToAction(nameof(Index), "Home");
        }

        [HttpPost]
        public IActionResult Remove(int id, decimal prijs, Winkelwagen winkelwagen)
        {
            Bon bon = _bonRepository.GetByBonId(id);
            winkelwagen.VerwijderLijn(bon, prijs);
            TempData["message"] = $"Bon {bon.Naam} met bedrag € {prijs} werd verwijderd uit uw winkelwagen.";
            return PartialView("IndexPartialItemsLijst", new IndexViewModel(winkelwagen.WinkelwagenLijnen, winkelwagen.TotaleWaarde));
        }

        [HttpPost]
        public IActionResult Plus(int id, decimal prijs, Winkelwagen winkelwagen)
        {
            winkelwagen.VerhoogAantal(id, prijs);
            return PartialView("IndexPartialItemsLijst", new IndexViewModel(winkelwagen.WinkelwagenLijnen, winkelwagen.TotaleWaarde));
        }

        [HttpPost]
        public IActionResult Min(int id, decimal prijs, Winkelwagen winkelwagen)
        {
            winkelwagen.VerlaagAantal(id, prijs);
            return PartialView("IndexPartialItemsLijst", new IndexViewModel(winkelwagen.WinkelwagenLijnen, winkelwagen.TotaleWaarde));
        }
    }
}