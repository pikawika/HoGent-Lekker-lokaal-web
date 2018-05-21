using iTextSharp.text;
using iTextSharp.text.pdf;
using LekkerLokaal.Filters;
using LekkerLokaal.Models;
using LekkerLokaal.Models.Domain;
using LekkerLokaal.Models.WinkelwagenViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QRCoder;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
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
        private readonly IHandelaarRepository _handelaarRepository;

        public CheckoutController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, ICategorieRepository categorieRepository, IGebruikerRepository gebruikerRepository, IBonRepository bonRepository, IBestellingRepository bestellingRepository, IBestellijnRepository bestellijnRepository, IHandelaarRepository handelaarRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _categorieRepository = categorieRepository;
            _gebruikerRepository = gebruikerRepository;
            _bonRepository = bonRepository;
            _bestellingRepository = bestellingRepository;
            _bestellijnRepository = bestellijnRepository;
            _handelaarRepository = handelaarRepository;
        }

        public IActionResult Index(string checkoutId, string returnUrl = null)
        {
            ViewData["AlleCategorien"] = _categorieRepository.GetAll().ToList();
            switch (checkoutId)
            {
                case "Gast":
                    return RedirectToAction(nameof(CheckoutController.BestellingPlaatsen), "Checkout");
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

                maakBonAan(bestelLijn);

                if ((index + 1) == bestellijnen.Count)
                    return RedirectToAction(nameof(CheckoutController.Betaling), "Checkout", new { Id = bestelling.BestellingId });
                return RedirectToAction(nameof(CheckoutController.BonAanmaken), "Checkout", new { index = index + 1 });
            }

            return View(model);
        }

        public void GenerateQR(string qrcode)
        {
            var bonPath = @"wwwroot/images/temp/";
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrcode, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);
            qrCodeImage.Save(bonPath + qrcode + ".png", ImageFormat.Png);
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

            return RedirectToAction(nameof(CheckoutController.BonAanmaken), "Checkout", new { index = 0 });
        }

        public IActionResult Betaling(int Id)
        {
            ViewData["AlleCategorien"] = _categorieRepository.GetAll().ToList();
            ViewData["Index"] = Id;

            return View();
        }

        public IActionResult BonnenBruikbaarMaken(int Id, Winkelwagen winkelwagen)
        {
            winkelwagen.MaakLeeg();
            Bestelling bestelling = _bestellingRepository.GetBy(Id);
            ICollection<BestelLijn> bestellijnen = HaalBestellijnenOp(bestelling);
            bestellijnen.ToList().ForEach(bl => bl.Geldigheid = Geldigheid.Geldig);
            IList<BestelLijn> bestellijn = bestellijnen.ToList();
            _gebruikerRepository.SaveChanges();
            //ViewData["bestellijnen"] = bestellijn;

            return RedirectToAction(nameof(CheckoutController.Bedankt), "Checkout", new { Id });
        }

        public IActionResult Bedankt(int Id)
        {
            ViewData["AlleCategorien"] = _categorieRepository.GetAll().ToList();

            Bestelling bestelling = _bestellingRepository.GetBy(Id);


            ICollection<BestelLijn> bestellijnen = HaalBestellijnenOp(bestelling);
            IList<BestelLijn> bestellijn = bestellijnen.ToList();
            ViewData["bestellijnen"] = bestellijn;
            
            VerstuurMails(bestelling);

            return View();
        }

        private void maakBonAan(BestelLijn bestelLijn)
        {
            var bon = _bonRepository.GetByBonId(bestelLijn.Bon.BonId);
            var handelaar = _handelaarRepository.GetByHandelaarId(bon.Handelaar.HandelaarId);

            string waarde = String.Format("€ " + bestelLijn.Prijs);
            string verval = bestelLijn.AanmaakDatum.AddYears(1).ToString("dd/MM/yyyy");
            string geldigheid = String.Format("Geldig tot: " + verval);
            var pdf = new Document(PageSize.A5.Rotate(), 81, 225, 25, 0);
            GenerateQR(bestelLijn.QRCode);
            var imageURL = @"wwwroot/images/temp/" + bestelLijn.QRCode + ".png";
            iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(imageURL);
            jpg.ScaleToFit(145f, 145f);
            var logoURL = @"wwwroot/images/logo.png";
            var logoURLHandelaar = @"wwwroot" + handelaar.GetLogoPath();
            var kadoURL = @"wwwroot/images/kado.jpg";
            iTextSharp.text.Image kado = iTextSharp.text.Image.GetInstance(kadoURL);
            iTextSharp.text.Image logoLL = iTextSharp.text.Image.GetInstance(logoURL);
            iTextSharp.text.Image logoHandelaar = iTextSharp.text.Image.GetInstance(logoURLHandelaar);

            logoLL.SetAbsolutePosition(20, 15);
            logoLL.ScaleToFit(188f, 100f);
            logoHandelaar.ScaleToFit(188f, 100f);
            logoHandelaar.SetAbsolutePosition(410, 15);
            jpg.SetAbsolutePosition(225, 10);
            kado.SetAbsolutePosition(65, 161);

            iTextSharp.text.Font arial = FontFactory.GetFont("Arial", 23);
            iTextSharp.text.Font arial18 = FontFactory.GetFont("Arial", 14);

            Paragraph bedrag = new Paragraph(waarde, arial);
            bedrag.SpacingAfter = 50;
            Paragraph naamHandelaar = new Paragraph(bon.Naam, arial);
            naamHandelaar.SpacingAfter = 0;
            Paragraph geschonkenDoor = new Paragraph("Geschonken door: " + bestelLijn.VerzenderNaam, arial18);
            Paragraph geldig = new Paragraph(geldigheid, arial18);
            bedrag.Alignment = Element.ALIGN_LEFT;
            naamHandelaar.Alignment = Element.ALIGN_LEFT;
            geschonkenDoor.Alignment = Element.ALIGN_LEFT;
            geldig.Alignment = Element.ALIGN_LEFT;

            PdfWriter writer = PdfWriter.GetInstance(pdf, new FileStream(@"wwwroot/pdf/c_" + bestelLijn.QRCode + ".pdf", FileMode.Create));
            pdf.Open();
            pdf.Add(logoLL);
            pdf.Add(logoHandelaar);
            pdf.Add(naamHandelaar);
            pdf.Add(bedrag);
            pdf.Add(geschonkenDoor);
            pdf.Add(geldig);
            pdf.Add(jpg);
            pdf.Add(kado);
            pdf.Close();

            System.IO.File.Delete(imageURL);
        }

        private void VerstuurMails(Bestelling bestelling)
        {
            ICollection<BestelLijn> bestellijnen = HaalBestellijnenOp(bestelling);
            IList<BestelLijn> bestellijn = bestellijnen.ToList();

            MailMessage message = new MailMessage();
            message.From = new MailAddress("lekkerlokaalst@gmail.com");
            message.To.Add(bestellijn[0].VerzenderEmail);
            message.Subject = "Uw order van Lekker Lokaal.";
            message.Body = String.Format("Beste " + bestellijn[0].VerzenderNaam + ", " + System.Environment.NewLine + System.Environment.NewLine +
                "Bedankt voor uw order op Lekker Lokaal." + System.Environment.NewLine +
                "In bijlage vindt u de gekochte cadeaubonnen." + System.Environment.NewLine + System.Environment.NewLine +
                "Met vriendelijke groeten," + System.Environment.NewLine + "Het Lekker Lokaal team");
            var attachment = new Attachment(@"wwwroot/favicon.ico");
            attachment.Dispose();
            for (int i = 0; i < bestellijnen.Count; i++)
            {
                attachment = new Attachment(@"wwwroot/pdf/c_" + bestellijn[i].QRCode + ".pdf");
                attachment.Name = "cadeaubon.pdf";
                message.Attachments.Add(attachment);

            }
            var SmtpServer = new SmtpClient("smtp.gmail.com");
            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("lekkerlokaalst@gmail.com", "LokaalLekker123");
            SmtpServer.EnableSsl = true;
            SmtpServer.Send(message);
            attachment = new Attachment(@"wwwroot/favicon.ico");
            attachment.Dispose();

            for (int i = 0; i < bestellijnen.Count; i++)
            {
                if (bestellijn[i].OntvangerEmail != null && bestellijn[i].OntvangerEmail != "")
                {
                    string boodschap = "";
                    if (bestellijn[i].Boodschap != null && bestellijn[i].Boodschap != "")
                    {
                        boodschap = String.Format("Met deze peroonlijke boodschap: " + System.Environment.NewLine +
                        bestellijn[i].Boodschap + System.Environment.NewLine + System.Environment.NewLine);
                    }
                    MailMessage message2 = new MailMessage();
                    message2.From = new MailAddress("lekkerlokaalst@gmail.com");
                    message2.To.Add(bestellijn[i].OntvangerEmail);
                    message2.Subject = bestellijn[i].VerzenderNaam + " stuurt u een cadeaubon van Lekker Lokaal.";
                    message2.Body = String.Format(

                        "Beste " + bestellijn[i].OntvangerNaam + ", " + System.Environment.NewLine + System.Environment.NewLine +
                        bestellijn[i].VerzenderNaam + " heeft voor u een cadeaubon gekocht." + System.Environment.NewLine + System.Environment.NewLine +

                        boodschap +

                        "In bijlage vindt u de cadeaubon." + System.Environment.NewLine + System.Environment.NewLine +
                        "Met vriendelijke groeten," + System.Environment.NewLine + "Het Lekker Lokaal team");

                    attachment = new Attachment(@"wwwroot/pdf/c_" + bestellijn[i].QRCode + ".pdf");
                    attachment.Name = "cadeaubon.pdf";
                    message2.Attachments.Add(attachment);
                    var SmtpServer2 = new SmtpClient("smtp.gmail.com");
                    SmtpServer2.Port = 587;
                    SmtpServer2.Credentials = new System.Net.NetworkCredential("lekkerlokaalst@gmail.com", "LokaalLekker123");
                    SmtpServer2.EnableSsl = true;
                    SmtpServer2.Send(message2);
                    attachment.Dispose();
                    
                }
            }

            //for (int i = 0; i < bestellijnen.Count; i++)
            //{
            //    System.IO.File.Delete(@"wwwroot/pdf/c_" + bestellijn[i].QRCode + ".pdf");
            //}
        }

    }
}