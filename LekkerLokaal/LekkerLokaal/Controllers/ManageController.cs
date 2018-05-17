using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using LekkerLokaal.Models;
using LekkerLokaal.Models.ManageViewModels;
using LekkerLokaal.Services;
using LekkerLokaal.Models.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Net.Mail;
using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;
using System.Security.Claims;

namespace LekkerLokaal.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class ManageController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;
        private readonly UrlEncoder _urlEncoder;
        private readonly ICategorieRepository _categorieRepository;
        private readonly IGebruikerRepository _gebruikerRepository;
        private readonly IBestellingRepository _bestellingRepository;
        private readonly IBestellijnRepository _bestellijnRepository;
        private readonly IHandelaarRepository _handelaarRepository;
        private readonly IBonRepository _bonRepository;

        private const string AuthenticatorUriFormat = "otpauth://totp/{0}:{1}?secret={2}&issuer={0}&digits=6";
        private const string RecoveryCodesKey = nameof(RecoveryCodesKey);

        public ManageController(
          UserManager<ApplicationUser> userManager,
          SignInManager<ApplicationUser> signInManager,
          IEmailSender emailSender,
          ILogger<ManageController> logger,
          UrlEncoder urlEncoder,
          ICategorieRepository categorieRepository,
          IGebruikerRepository gebruikerRepository,
          IBestellingRepository bestellingRepository,
          IBestellijnRepository bestellijnRepository,
          IHandelaarRepository handelaarRepository,
          IBonRepository bonRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _logger = logger;
            _urlEncoder = urlEncoder;
            _categorieRepository = categorieRepository;
            _gebruikerRepository = gebruikerRepository;
            _bestellingRepository = bestellingRepository;
            _bestellijnRepository = bestellijnRepository;
            _handelaarRepository = handelaarRepository;
            _bonRepository = bonRepository;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewData["AlleCategorien"] = _categorieRepository.GetAll().ToList();
            ViewData["Geslacht"] = Geslachten();

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var gebruiker = _gebruikerRepository.GetBy(user.Email);

            var model = new IndexViewModel
            {
                Username = user.UserName,
                Email = user.Email,
                Voornaam = gebruiker.Voornaam,
                Familienaam = gebruiker.Familienaam,
                Geslacht = gebruiker.Geslacht,
                IsEmailConfirmed = user.EmailConfirmed,
                StatusMessage = StatusMessage
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> IndexHandelaar()
        {
            ViewData["AlleCategorien"] = _categorieRepository.GetAll().ToList();

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var handelaar = _handelaarRepository.GetByEmail(user.Email);

            var model = new IndexHandelaarViewModel
            {
                Naam = handelaar.Naam,
                Email = user.Email,
                Beschrijving = handelaar.Beschrijving,
                BTW_Nummer = handelaar.BTW_Nummer,
                Straat = handelaar.Straat,
                Gemeente = handelaar.Gemeente,
                Postcode = handelaar.Postcode,
                IsEmailConfirmed = user.EmailConfirmed,
                StatusMessage = StatusMessage
            };

            return View(model);
        }


        [HttpGet]
        public IActionResult CadeaubonVerzoekToevoegen()
        {
            ViewData["AlleCategorien"] = _categorieRepository.GetAll().ToList();
            ViewData["categorieen"] = new SelectList(_categorieRepository.GetAll().Select(c => c.Naam));
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CadeaubonVerzoekToevoegen(CadeaubonVerzoekToevoegenViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
                }
                var handelaar = _handelaarRepository.GetByEmail(user.Email);
                Bon nieuweBon = new Bon(model.Naam, model.MinimumPrijs, model.Maximumprijs, model.Beschrijving, 0, @"temp", _categorieRepository.GetByNaam(model.Categorie), model.Straatnaam, model.Huisnummer, model.Postcode, model.Gemeente, _handelaarRepository.GetByHandelaarId(handelaar.HandelaarId), Aanbieding.Geen, false);
                _bonRepository.Add(nieuweBon);
                _bonRepository.SaveChanges();

                nieuweBon.Afbeelding = @"images\bon\" + nieuweBon.BonId + @"\";
                _bonRepository.SaveChanges();

                var filePath = @"wwwroot/images/bon/" + nieuweBon.BonId + "/thumb.jpg";
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                var fileStream = new FileStream(filePath, FileMode.Create);
                await model.Thumbnail.CopyToAsync(fileStream);
                fileStream.Close();

                for (int i = 0; i < model.Afbeeldingen.Count; i++)
                {
                    filePath = @"wwwroot/images/bon/" + nieuweBon.BonId + "/Afbeeldingen/" + (i + 1) + ".jpg";
                    Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                    fileStream = new FileStream(filePath, FileMode.Create);
                    await model.Afbeeldingen[i].CopyToAsync(fileStream);
                    fileStream.Close();
                }
                ViewData["AlleCategorien"] = _categorieRepository.GetAll().ToList();
                ViewData["categorieen"] = new SelectList(_categorieRepository.GetAll().Select(c => c.Naam));
                return RedirectToAction(nameof(HomeController.Index), "Home");

            }
            ViewData["AlleCategorien"] = _categorieRepository.GetAll().ToList();
            ViewData["categorieen"] = new SelectList(_categorieRepository.GetAll().Select(c => c.Naam));
            return View(nameof(CadeaubonVerzoekToevoegen), model);
        }




        private SelectList Geslachten()
        {
            var geslachten = new List<Geslacht>();
            foreach (Geslacht geslacht in Enum.GetValues(typeof(Geslacht)))
            {
                geslachten.Add(geslacht);
            }
            return new SelectList(geslachten);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(IndexViewModel model)
        {
            ViewData["AlleCategorien"] = _categorieRepository.GetAll().ToList();
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var gebruiker = _gebruikerRepository.GetBy(user.Email);

            var email = user.Email;

            var voornaam = gebruiker.Voornaam;
            if (model.Voornaam != voornaam)
            {
                gebruiker.Voornaam = model.Voornaam;
                _gebruikerRepository.SaveChanges();
            }

            var familienaam = gebruiker.Familienaam;
            if (model.Familienaam != familienaam)
            {
                gebruiker.Familienaam = model.Familienaam;
                _gebruikerRepository.SaveChanges();
            }

            var geslacht = gebruiker.Geslacht;
            if (model.Geslacht != geslacht)
            {
                gebruiker.Geslacht = model.Geslacht;
                _gebruikerRepository.SaveChanges();
            }

            StatusMessage = "Uw gegevens werden succesvol bijgewerkt.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> IndexHandelaar(IndexHandelaarViewModel model)
        {
            ViewData["AlleCategorien"] = _categorieRepository.GetAll().ToList();
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var handelaar = _handelaarRepository.GetByEmail(user.Email);

            var beschrijving = handelaar.Beschrijving;
            if (model.Beschrijving != beschrijving)
            {
                handelaar.Beschrijving = model.Beschrijving;
                _handelaarRepository.SaveChanges();
            }

            var btwnummer = handelaar.BTW_Nummer;
            if (model.BTW_Nummer != btwnummer)
            {
                handelaar.BTW_Nummer = model.BTW_Nummer;
                _handelaarRepository.SaveChanges();
            }

            var straat = handelaar.Straat;
            if (model.Straat != straat)
            {
                handelaar.Straat = model.Straat;
                _handelaarRepository.SaveChanges();
            }

            var gemeente = handelaar.Gemeente;
            if (model.Gemeente != gemeente)
            {
                handelaar.Gemeente = model.Gemeente;
                _handelaarRepository.SaveChanges();
            }

            var postcode = handelaar.Postcode;
            if (model.Postcode != postcode)
            {
                handelaar.Postcode = model.Postcode;
                _handelaarRepository.SaveChanges();
            }

            StatusMessage = "Uw gegevens werden succesvol bijgewerkt.";
            return RedirectToAction(nameof(IndexHandelaar));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendVerificationEmail(IndexViewModel model)
        {
            ViewData["AlleCategorien"] = _categorieRepository.GetAll().ToList();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
            var email = user.Email;

            var gebruiker = _gebruikerRepository.GetBy(email);

            await _emailSender.SendEmailConfirmationAsync(email, callbackUrl, gebruiker.Voornaam);

            StatusMessage = "Er is een nieuwe bevestigingsmail naar uw e-mailadres verzonden.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> ChangePassword()
        {
            ViewData["AlleCategorien"] = _categorieRepository.GetAll().ToList();

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var hasPassword = await _userManager.HasPasswordAsync(user);
            if (!hasPassword)
            {
                return RedirectToAction(nameof(SetPassword));
            }

            var model = new ChangePasswordViewModel { StatusMessage = StatusMessage };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            ViewData["AlleCategorien"] = _categorieRepository.GetAll().ToList();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                AddErrors(changePasswordResult);
                return View(model);
            }

            IList<Claim> claims = await _userManager.GetClaimsAsync(user);
            if (claims.Any(c => c.Value == "handelaar"))
            {
                var wachtwoord = model.NewPassword;

                var handelaar = _handelaarRepository.GetByEmail(user.Email);

                _handelaarRepository.SaveChanges();
            }

            await _signInManager.SignInAsync(user, isPersistent: false);
            _logger.LogInformation("User changed their password successfully.");
            StatusMessage = "Uw wachtwoord is succesvol gewijzigd.";

            return RedirectToAction(nameof(ChangePassword));
        }

        [HttpGet]
        public async Task<IActionResult> SetPassword()
        {
            ViewData["AlleCategorien"] = _categorieRepository.GetAll().ToList();

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var hasPassword = await _userManager.HasPasswordAsync(user);

            if (hasPassword)
            {
                return RedirectToAction(nameof(ChangePassword));
            }

            var model = new SetPasswordViewModel { StatusMessage = StatusMessage };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetPassword(SetPasswordViewModel model)
        {
            ViewData["AlleCategorien"] = _categorieRepository.GetAll().ToList();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var addPasswordResult = await _userManager.AddPasswordAsync(user, model.NewPassword);
            if (!addPasswordResult.Succeeded)
            {
                AddErrors(addPasswordResult);
                return View(model);
            }

            await _signInManager.SignInAsync(user, isPersistent: false);
            StatusMessage = "Uw wachtwoord werd opnieuw ingesteld.";

            return RedirectToAction(nameof(SetPassword));
        }

        [HttpGet]
        public async Task<IActionResult> ExternalLogins()
        {
            ViewData["AlleCategorien"] = _categorieRepository.GetAll().ToList();

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var model = new ExternalLoginsViewModel { CurrentLogins = await _userManager.GetLoginsAsync(user) };
            model.OtherLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync())
                .Where(auth => model.CurrentLogins.All(ul => auth.Name != ul.LoginProvider))
                .ToList();
            model.ShowRemoveButton = await _userManager.HasPasswordAsync(user) || model.CurrentLogins.Count > 1;
            model.StatusMessage = StatusMessage;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LinkLogin(string provider)
        {
            ViewData["AlleCategorien"] = _categorieRepository.GetAll().ToList();
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            // Request a redirect to the external login provider to link a login for the current user
            var redirectUrl = Url.Action(nameof(LinkLoginCallback));
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl, _userManager.GetUserId(User));
            return new ChallengeResult(provider, properties);
        }

        [HttpGet]
        public async Task<IActionResult> LinkLoginCallback()
        {
            ViewData["AlleCategorien"] = _categorieRepository.GetAll().ToList();

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var info = await _signInManager.GetExternalLoginInfoAsync(user.Id);
            if (info == null)
            {
                throw new ApplicationException($"Unexpected error occurred loading external login info for user with ID '{user.Id}'.");
            }

            var result = await _userManager.AddLoginAsync(user, info);
            if (!result.Succeeded)
            {
                throw new ApplicationException($"Unexpected error occurred adding external login for user with ID '{user.Id}'.");
            }

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            StatusMessage = "De externe login werd succesvol toegevoegd.";
            return RedirectToAction(nameof(ExternalLogins));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveLogin(RemoveLoginViewModel model)
        {
            ViewData["AlleCategorien"] = _categorieRepository.GetAll().ToList();

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var result = await _userManager.RemoveLoginAsync(user, model.LoginProvider, model.ProviderKey);
            if (!result.Succeeded)
            {
                throw new ApplicationException($"Unexpected error occurred removing external login for user with ID '{user.Id}'.");
            }

            await _signInManager.SignInAsync(user, isPersistent: false);
            StatusMessage = "De externe login werd succesvol verwijderd.";
            return RedirectToAction(nameof(ExternalLogins));
        }

        [HttpGet]
        public async Task<IActionResult> PersoonlijkeBestellingen()
        {
            ViewData["AlleCategorien"] = _categorieRepository.GetAll().ToList();
            ViewData["bestelling"] = null;
            var user = await _userManager.GetUserAsync(User);
            var gebruiker = _gebruikerRepository.GetBy(user.Email);
            ICollection<Bestelling> bestellingen = new HashSet<Bestelling>();
            if (gebruiker.Bestellingen.Count != 0 && gebruiker.Bestellingen != null)
            {
                foreach (Bestelling b in gebruiker.Bestellingen)
                {
                    var bestelling = _bestellingRepository.GetBy(b.BestellingId);
                    if (bestelling.BestelLijnen.All(bl => bl.Geldigheid != Geldigheid.Ongeldig))
                        bestellingen.Add(bestelling);
                }
                ViewData["bestelling"] = bestellingen;
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> DetailBestelling(int id)
        {
            ViewData["AlleCategorien"] = _categorieRepository.GetAll().ToList();
            ViewData["bestellijnen"] = null;

            var user = await _userManager.GetUserAsync(User);
            var gebruiker = _gebruikerRepository.GetBy(user.Email);

            if (_bestellingRepository.GetBy(id) != null)
            {
                var bestelling = _bestellingRepository.GetBy(id);
                var gebruiker2 = _gebruikerRepository.GetByBestellingId(id);

                if (gebruiker == gebruiker2)
                {
                    ICollection<BestelLijn> bestellijnen = new HashSet<BestelLijn>();
                    foreach (BestelLijn bl in bestelling.BestelLijnen)
                    {
                        bestellijnen.Add(_bestellijnRepository.GetById(bl.BestelLijnId));
                    }
                    ViewData["bestellijnen"] = bestellijnen;
                    ViewData["bestelid"] = bestelling.BestellingId;
                }
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BonAanmaken(int Id)
        {
            ViewData["AlleCategorien"] = _categorieRepository.GetAll().ToList();
            if (ModelState.IsValid)
            {
                var bonPath = @"wwwroot/pdf";
                
                var bestellijn = _bestellijnRepository.GetById(Id);
                var bon = _bonRepository.GetByBonId(bestellijn.Bon.BonId);
                var handelaar = _handelaarRepository.GetByHandelaarId(bon.Handelaar.HandelaarId);

                string waarde = String.Format("Bedrag: € " + bestellijn.Prijs);
                string verval = bestellijn.AanmaakDatum.AddYears(1).ToString("dd/MM/yyyy");
                string geldigheid = String.Format("Geldig tot: " + verval);
                var doc1 = new Document(PageSize.A5);
                Paragraph p1 = new Paragraph(waarde);
                Paragraph p2 = new Paragraph(geldigheid);
                GenerateQR(bestellijn.QRCode);
                var imageURL = @"wwwroot/images/temp/" + bestellijn.QRCode + ".png";
                iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(imageURL);
                jpg.ScaleToFit(140f, 140f);
                var logoURL = @"wwwroot/images/logo.png";
                var logoURLHandelaar = @"wwwroot" + handelaar.GetLogoPath();
                iTextSharp.text.Image logoLL = iTextSharp.text.Image.GetInstance(logoURL);
                iTextSharp.text.Image logoHandelaar = iTextSharp.text.Image.GetInstance(logoURLHandelaar);
                Paragraph naamBon = new Paragraph("Bon: " + bon.Naam);

                logoLL.SetAbsolutePosition(30, 515);
                logoLL.ScalePercent(50f);
                logoHandelaar.ScalePercent(10f);

                jpg.Alignment = Element.ALIGN_CENTER;
                naamBon.Alignment = Element.ALIGN_CENTER;
                p1.Alignment = Element.ALIGN_CENTER;
                p2.Alignment = Element.ALIGN_CENTER;
                logoHandelaar.Alignment = Element.ALIGN_RIGHT;

                
                PdfWriter.GetInstance(doc1, new FileStream(bonPath + "/Doc1.pdf", FileMode.Create));

                doc1.Open();
                doc1.Add(logoLL);
                doc1.Add(logoHandelaar);
                doc1.Add(naamBon);
                doc1.Add(p1);
                doc1.Add(p2);
                doc1.Add(jpg);
                doc1.Close();

                System.IO.File.Delete(imageURL);

                var user = _userManager.GetUserAsync(User);
                var gebruiker = _gebruikerRepository.GetBy(user.Result.Email);

                string to = String.Format("lekkerlokaalst@gmail.com");
                MailMessage message = new MailMessage();
                message.From = new MailAddress("lekkerlokaalst@gmail.com");
                message.To.Add(to);
                message.Subject = "Uw cadeaubon van Lekker Lokaal.";

                message.Body = String.Format("Beste "+ gebruiker.Voornaam + " " + gebruiker.Familienaam + System.Environment.NewLine + System.Environment.NewLine + "U hebt uw cadeaubon opnieuw opgevraagd." + System.Environment.NewLine + "U vindt deze in bijlage." + System.Environment.NewLine + System.Environment.NewLine + "Met vriendelijke groeten," + System.Environment.NewLine + "Het Lekker Lokaal team");


                var attachment = new Attachment(@"wwwroot/pdf/doc1.pdf");
                attachment.Name = "cadeaubon.pdf";
                message.Attachments.Add(attachment);
                var SmtpServer = new SmtpClient("smtp.gmail.com");
                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("lekkerlokaalst@gmail.com", "LokaalLekker123");
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(message);
                attachment.Dispose();
                //System.IO.File.Delete(@"wwwroot/pdf/doc1.pdf");

                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            return View();
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

        [HttpPost]
        public async Task<IActionResult> Bon(int Id)
        {
            ViewData["AlleCategorien"] = _categorieRepository.GetAll().ToList();
            
            if (ModelState.IsValid)
            {

                var bestellijn = _bestellijnRepository.GetById(Id);
                var bon = _bonRepository.GetByBonId(bestellijn.Bon.BonId);
                var handelaar = _handelaarRepository.GetByHandelaarId(bon.Handelaar.HandelaarId);
                var user = _userManager.GetUserAsync(User);
                var gebruiker = _gebruikerRepository.GetBy(user.Result.Email);

                ViewData["path"] = @"/pdf/c_" + bestellijn.QRCode + ".pdf";

                string waarde = String.Format("€ " + bestellijn.Prijs.ToString());
                string verval = bestellijn.AanmaakDatum.AddYears(1).ToString("dd/MM/yyyy");
                string geldigheid = String.Format("Geldig tot: " + verval);
                var pdf = new Document(PageSize.A5.Rotate(), 81,225,25,0);
                //Paragraph bedrag = new Paragraph(waarde);
                //Paragraph p2 = new Paragraph(geldigheid);
                GenerateQR(bestellijn.QRCode);
                var imageURL = @"wwwroot/images/temp/" + bestellijn.QRCode + ".png";
                iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(imageURL);
                jpg.ScaleToFit(145f, 145f);
                var logoURL = @"wwwroot/images/logo.png";
                var logoURLHandelaar = @"wwwroot" + handelaar.GetLogoPath();
                var kadoURL = @"wwwroot/images/kado.jpg";
                iTextSharp.text.Image kado = iTextSharp.text.Image.GetInstance(kadoURL);
                iTextSharp.text.Image logoLL = iTextSharp.text.Image.GetInstance(logoURL);
                iTextSharp.text.Image logoHandelaar = iTextSharp.text.Image.GetInstance(logoURLHandelaar);
                //Paragraph naamBon = new Paragraph("Bon: " + bon.Naam);

                logoLL.SetAbsolutePosition(20,15);
                logoLL.ScaleToFit(188f,100f);
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
                Paragraph geschonkenDoor = new Paragraph("Geschonken door: " + gebruiker.Voornaam, arial18);
                Paragraph geldig = new Paragraph(geldigheid, arial18);

                bedrag.Alignment = Element.ALIGN_LEFT;
                
                naamHandelaar.Alignment = Element.ALIGN_LEFT;
                geschonkenDoor.Alignment = Element.ALIGN_LEFT;
                geldig.Alignment = Element.ALIGN_LEFT;




                PdfWriter writer = PdfWriter.GetInstance(pdf, new FileStream(@"wwwroot/pdf/c_" + bestellijn.QRCode +".pdf", FileMode.Create));
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

                //System.IO.File.Delete(@"wwwroot/pdf/c_" + bestellijn.QRCode + ".pdf");
            }

            return View();
        }


        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private string FormatKey(string unformattedKey)
        {
            var result = new StringBuilder();
            int currentPosition = 0;
            while (currentPosition + 4 < unformattedKey.Length)
            {
                result.Append(unformattedKey.Substring(currentPosition, 4)).Append(" ");
                currentPosition += 4;
            }
            if (currentPosition < unformattedKey.Length)
            {
                result.Append(unformattedKey.Substring(currentPosition));
            }

            return result.ToString().ToLowerInvariant();
        }

        #endregion
    }
}
