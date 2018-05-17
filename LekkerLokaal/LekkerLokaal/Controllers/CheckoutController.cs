using iTextSharp.text;
using iTextSharp.text.pdf;
using LekkerLokaal.Filters;
using LekkerLokaal.Models;
using LekkerLokaal.Models.Domain;
using LekkerLokaal.Models.WinkelwagenViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace LekkerLokaal.Controllers
{
    [ServiceFilter(typeof(CartSessionFilter))]
    public class CheckoutController : Controller
    {
        private readonly ICategorieRepository _categorieRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IGebruikerRepository _gebruikerRepository;
        private readonly IBonRepository _bonRepository;
        private readonly IBestellingRepository _bestellingRepository;
        private readonly IBestellijnRepository _bestellijnRepository;

        public CheckoutController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, ICategorieRepository categorieRepository, IGebruikerRepository gebruikerRepository, IBonRepository bonRepository, IBestellingRepository bestellingRepository, IBestellijnRepository bestellijnRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _categorieRepository = categorieRepository;
            _gebruikerRepository = gebruikerRepository;
            _bonRepository = bonRepository;
            _bestellingRepository = bestellingRepository;
            _bestellijnRepository = bestellijnRepository;
        }

        public IActionResult Index(string checkoutId, string returnUrl = null)
        {
            ViewData["AlleCategorien"] = _categorieRepository.GetAll().ToList();
            switch (checkoutId)
            {
                case "Gast":
                    return RedirectToAction(nameof(CheckoutController.BestellingPlaatsen), "Checkout");
                    //voor te testen
                    //return RedirectToAction(nameof(CheckoutController.Bedankt), "Checkout");
                case "Nieuw":
                    return RedirectToAction(nameof(AccountController.Register), "Account", new { ReturnUrl = returnUrl });
                case "LogIn":
                    return RedirectToAction(nameof(AccountController.Login), "Account", new { ReturnUrl = returnUrl });
                default:
                    return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

        [HttpGet]
        public async Task<IActionResult> BonAanmaken(int index)
        {
            ViewData["AlleCategorien"] = _categorieRepository.GetAll().ToList();

            Gebruiker gebruiker = await HaalGebruikerOp();
            Bestelling bestelling = _bestellingRepository.GetBy(gebruiker.Bestellingen.Last().BestellingId);
            ICollection<BestelLijn> bestellijnen = HaalBestellijnenOp(bestelling);

            ViewData["Bestelling"] = bestelling;
            ViewData["Bestellijnen"] = bestellijnen;
            ViewData["Index"] = index;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BonAanmaken(int index, BonAanmakenViewModel model)
        {
            ViewData["AlleCategorien"] = _categorieRepository.GetAll().ToList();
            ViewData["Index"] = index;
            var gebruiker = await HaalGebruikerOp();
            Bestelling bestelling = _bestellingRepository.GetBy(gebruiker.Bestellingen.Last().BestellingId);
            ViewData["Bestelling"] = bestelling;
            IList<BestelLijn> bestellijnen = HaalBestellijnenOp(bestelling).ToList();
            ViewData["Bestellijnen"] = bestellijnen;
            
            if (ModelState.IsValid)
            {
                BestelLijn bestelLijn = bestellijnen[(int)index];
                bestelLijn.VerzenderNaam = model.UwNaam;
                bestelLijn.VerzenderEmail = model.UwEmail;
                bestelLijn.OntvangerNaam = model.NaamOntvanger;
                if (model.Boodschap != null && model.Boodschap != "")
                    bestelLijn.Boodschap = model.Boodschap;
                if (model.EmailOntvanger != null && model.EmailOntvanger != "")
                    bestelLijn.OntvangerEmail = model.EmailOntvanger;
                _bestellijnRepository.SaveChanges();

                if ((index + 1) == bestellijnen.Count)
                    return RedirectToAction(nameof(CheckoutController.Bedankt), "Checkout", new { Id = bestelling.BestellingId });
                return RedirectToAction(nameof(CheckoutController.BonAanmaken), "Checkout", new { index = index + 1 });
            }

            return View(model);
        }

        private async Task<Gebruiker> HaalGebruikerOp()
        {
            var gebruiker = _gebruikerRepository.GetBy("lekkerlokaal");
            if (_signInManager.IsSignedIn(User))
            {
                var user = await _userManager.GetUserAsync(User);
                if (_gebruikerRepository.GetBy(user.Email) != null)
                    gebruiker = _gebruikerRepository.GetBy(user.Email);
            }
            return gebruiker;
        }

        private ICollection<BestelLijn> HaalBestellijnenOp(Bestelling bestelling)
        {
            ICollection<BestelLijn> bestellijnen = new HashSet<BestelLijn>();

            foreach (BestelLijn bl in bestelling.BestelLijnen)
            {
                bestellijnen.Add(_bestellijnRepository.GetById(bl.BestelLijnId));
            }

            return bestellijnen;
        }

        public async Task<IActionResult> BestellingPlaatsen(Winkelwagen winkelwagen)
        {
            ViewData["AlleCategorien"] = _categorieRepository.GetAll().ToList();

            var gebruiker = await HaalGebruikerOp();
            gebruiker.PlaatsBestelling(winkelwagen);
            _gebruikerRepository.SaveChanges();
            _bonRepository.SaveChanges();

            return RedirectToAction(nameof(CheckoutController.BonAanmaken), "Checkout", new { index = 0 } );
        }

        public IActionResult Bedankt(int Id, Winkelwagen winkelwagen)
        {
            ViewData["AlleCategorien"] = _categorieRepository.GetAll().ToList();
            winkelwagen.MaakLeeg();

            Bestelling bestelling = _bestellingRepository.GetBy(Id);
            ICollection<BestelLijn> bestellijnen = HaalBestellijnenOp(bestelling);
            bestellijnen.ToList().ForEach(bl => bl.Geldigheid = Geldigheid.Geldig);

            _gebruikerRepository.SaveChanges();

            return View();
        }
    }
}