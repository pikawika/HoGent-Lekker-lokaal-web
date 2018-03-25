using iTextSharp.text;
using iTextSharp.text.pdf;
using LekkerLokaal.Models.Domain;
using LekkerLokaal.Models.WinkelwagenViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LekkerLokaal.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly ICategorieRepository _categorieRepository;

        public CheckoutController(ICategorieRepository categorieRepository)
        {
            _categorieRepository = categorieRepository;
        }

        public IActionResult Index(string checkoutId, string returnUrl = null)
        {
            ViewData["AlleCategorien"] = _categorieRepository.GetAll().ToList();
            switch (checkoutId)
            {
                case "Gast":
                    return RedirectToAction(nameof(CheckoutController.BonAanmaken), "Checkout");
                case "Nieuw":
                    return RedirectToAction(nameof(AccountController.Register), "Account", new { ReturnUrl = returnUrl });
                case "LogIn":
                    return RedirectToAction(nameof(AccountController.Login), "Account", new { ReturnUrl = returnUrl });
                default:
                    return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

        public IActionResult BonAanmaken(Winkelwagen winkelwagen)
        {
            ViewData["AlleCategorien"] = _categorieRepository.GetAll().ToList();
            ViewData["Totaal"] = winkelwagen.TotaleWaarde;
            ViewData["Aantal"] = winkelwagen.AantalBonnen;
            return View(nameof(BonAanmaken));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BonAanmaken(BonAanmakenViewModel model, string returnUrl = null)
        {
            ViewData["AlleCategorien"] = _categorieRepository.GetAll().ToList();
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                string message = String.Format("Hey, " + "{0}\n" + " {1}" + " stuurt je een cadeaubon! \n", model.NaamOntvanger, model.UwNaam);
                var doc1 = new Document(PageSize.A4);
                var filePath = @"wwwroot/pdf";
                PdfWriter.GetInstance(doc1, new FileStream(filePath + "/Doc1.pdf", FileMode.Create));
                doc1.Open();
                doc1.Add(new Paragraph(message));
                doc1.Close();
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            return View(model);
        }
    }
}