using iTextSharp.text;
using iTextSharp.text.pdf;
using LekkerLokaal.Filters;
using LekkerLokaal.Models;
using LekkerLokaal.Models.Domain;
using LekkerLokaal.Models.WinkelwagenViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
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

        public CheckoutController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, ICategorieRepository categorieRepository, IGebruikerRepository gebruikerRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _categorieRepository = categorieRepository;
            _gebruikerRepository = gebruikerRepository;
        }

        public IActionResult Index(string checkoutId, string returnUrl = null)
        {
            ViewData["AlleCategorien"] = _categorieRepository.GetAll().ToList();
            switch (checkoutId)
            {
                case "Gast":
                    //voor te testen
                    //return RedirectToAction(nameof(CheckoutController.BonAanmaken), "Checkout");
                    return RedirectToAction(nameof(CheckoutController.Bedankt), "Checkout");
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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BonAanmaken(Winkelwagen winkelwagen, BonAanmakenViewModel model, string returnUrl = null)
        {
            ViewData["AlleCategorien"] = _categorieRepository.GetAll().ToList();
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                string bon = String.Format("Hey, " + "{0}\n" + "{1}" + " stuurt je een cadeaubon! \n", model.NaamOntvanger, model.UwNaam);
                string boodschap = String.Format("{0}", model.Boodschap);
                var doc1 = new Document(PageSize.A4);
                Paragraph p1 = new Paragraph(bon);
                Paragraph p2 = new Paragraph(boodschap);
                p1.Alignment = Element.ALIGN_CENTER;
                p2.Alignment = Element.ALIGN_CENTER;
                var bonPath = @"wwwroot/pdf";
                PdfWriter.GetInstance(doc1, new FileStream(bonPath + "/Doc1.pdf", FileMode.Create));
                doc1.Open();
                doc1.Add(p1);
                doc1.Add(p2);
                doc1.Close();

                string[] to = { model.EmailOntvanger, model.UwEmail };
                MailMessage message = new MailMessage();
                message.From = new MailAddress("lekkerlokaalst@gmail.com");

                foreach (var m in to)
                {
                    message.To.Add(m);
                }

                message.Subject = "Uw order van Lekker Lokaal";
                message.Body = String.Format("Dit zijn de cadeaubonnen die u bestelde.");

                var attachment = new Attachment(@"wwwroot/pdf/doc1.pdf");
                attachment.Name = "cadeaubon.pdf";
                message.Attachments.Add(attachment);
                var SmtpServer = new SmtpClient("smtp.gmail.com");
                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("lekkerlokaalst@gmail.com", "LokaalLekker123");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(message);
                attachment.Dispose();
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            return View(model);
        }

        public IActionResult Bedankt(Winkelwagen winkelwagen)
        {
            ViewData["AlleCategorien"] = _categorieRepository.GetAll().ToList();
            ViewData["Totaal"] = winkelwagen.TotaleWaarde;
            ViewData["Aantal"] = winkelwagen.AantalBonnen;

            var gebruiker = _gebruikerRepository.GetBy("lekkerlokaal");
            if (_signInManager.IsSignedIn(User))
            {
                var user = _userManager.GetUserAsync(User);
                if (_gebruikerRepository.GetBy(user.Result.Email) != null)
                    gebruiker = _gebruikerRepository.GetBy(user.Result.Email);
            }

            gebruiker.PlaatsBestelling(winkelwagen);
            _gebruikerRepository.SaveChanges();

            return View();
        }
    }
}