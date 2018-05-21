using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LekkerLokaal.Models;
using LekkerLokaal.Models.AdminViewModels;
using LekkerLokaal.Models.Domain;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net.Mail;
using System.IO;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;
using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace LekkerLokaal.Controllers
{
    [Authorize(Roles = "admin")]
    [Route("[controller]/[action]")]
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IHandelaarRepository _handelaarRepository;
        private readonly IBonRepository _bonRepository;
        private readonly IGebruikerRepository _gebruikerRepository;
        private readonly ICategorieRepository _categorieRepository;
        private readonly IBestellijnRepository _bestellijnRepository;
        private readonly ILogger _logger;

        public AdminController(
            UserManager<ApplicationUser> userManager,
            ILogger<AdminController> logger,
            SignInManager<ApplicationUser> signInManager,
            IHandelaarRepository handelaarRepository,
            IBonRepository bonRepository,
            IGebruikerRepository gebruikerRepository,
            IBestellijnRepository bestellijnRepository,
            ICategorieRepository categorieRepository)
        {
            _userManager = userManager;
            _logger = logger;
            _signInManager = signInManager;
            _handelaarRepository = handelaarRepository;
            _gebruikerRepository = gebruikerRepository;
            _bonRepository = bonRepository;
            _bestellijnRepository = bestellijnRepository;
            _categorieRepository = categorieRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Uw e-mailadres en/of wachtwoord is fout. Gelieve het opnieuw te proberen");
                    return View(model);
                }
                else
                {
                    var claims = await _userManager.GetClaimsAsync(user);

                    if (!claims.Any(claimpje => claimpje.Value == "admin"))
                    {
                        ModelState.AddModelError(string.Empty, "U beschikt niet over de nodige rechten om u aan te melden op deze applicatie.");
                        return View(model);
                    }
                    var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: false, lockoutOnFailure: false);
                    if (result.Succeeded)
                    {
                        _logger.LogInformation("User logged in.");
                        return RedirectToAction("Dashboard");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Uw e-mailadres en/of wachtwoord is fout. Gelieve het opnieuw te proberen");
                        return View(model);
                    }
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");

            return RedirectToAction("Index");
        }

        [HttpGet]

        public IActionResult Dashboard()
        {
            return View(new DashboardViewModel(_handelaarRepository.getAantalHandelaarsverzoeken(), _bonRepository.getAantalBonverzoeken(), _bestellijnRepository.getVerkochtDezeMaand(), _bestellijnRepository.getGebruiktDezeMaand()));
        }

        [HttpGet]
        public IActionResult HandelaarsVerzoeken()
        {
            return View(new HandelaarsVerzoekenViewModel(_handelaarRepository.GetHandelaarsNogNietGoedgekeurd(_handelaarRepository.GetAll())));
        }

        [HttpGet]
        public IActionResult HandelaarVerzoekEvaluatie(int Id)
        {
            Handelaar geselecteerdeHandelaarEvaluatie = _handelaarRepository.GetByHandelaarIdNotAccepted(Id);
            if (geselecteerdeHandelaarEvaluatie == null)
            {
                return RedirectToAction("HandelaarsVerzoeken");
            }
            return View(new HandelaarBewerkViewModel(geselecteerdeHandelaarEvaluatie));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> VerwijderHandelaarVerzoek(HandelaarBewerkViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Emailadres);
                await _userManager.DeleteAsync(user);

                _handelaarRepository.Remove(model.HandelaarId);
                _handelaarRepository.SaveChanges();

                var filePath = @"wwwroot/images/handelaar/" + model.HandelaarId;
                Directory.Delete(filePath, true);

                var message = new MailMessage();
                message.From = new MailAddress("lekkerlokaalst@gmail.com");
                message.To.Add(model.Emailadres);
                message.Subject = "Uw verzoek om handelaar te worden op LekkerLokaal.be is geweigerd.";

                if (model.Opmerking != null)
                {
                    message.Body = String.Format("Beste medewerker van " + model.Naam + ", \n\n" +
                   "Uw recent verzoek om handelaar te worden bij LekkerLokaal.be is geweigerd. \n\n" +
                   model.Opmerking + "\n\n" +
                   "Als u denkt dat u alsnog recht heeft om handelaar te worden bij LekkerLokaal.be raden wij u aan een nieuw verzoek te versturen. \n\n" +
                   "Met vriendelijke groeten, \n" +
                  "Het Lekker Lokaal team");
                }
                else
                {
                    message.Body = String.Format("Beste medewerker van " + model.Naam + ", \n\n" +
                  "Uw recent verzoek om handelaar te worden bij LekkerLokaal.be is geweigerd. \n\n" +
                  "Als u denkt dat u alsnog recht heeft om handelaar te worden bij LekkerLokaal.be raden wij u aan een nieuw verzoek te versturen. \n\n" +
                  "Met vriendelijke groeten, \n\n" +
                  "Het Lekker Lokaal team");
                }

                var SmtpServer = new SmtpClient("smtp.gmail.com");
                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("lekkerlokaalst@gmail.com", "LokaalLekker123");
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(message);

                return RedirectToAction("HandelaarsVerzoeken");
            }
            return View(nameof(HandelaarVerzoekEvaluatie), model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AccepteerHandelaarVerzoek(HandelaarBewerkViewModel model)
        {
            if (ModelState.IsValid)
            {
                Handelaar handelaarInDB = _handelaarRepository.GetByHandelaarIdNotAccepted(model.HandelaarId);

                if (handelaarInDB.Naam != model.Naam)
                {
                    handelaarInDB.Naam = model.Naam;
                }

                if (handelaarInDB.Emailadres != model.Emailadres)
                {
                    handelaarInDB.Emailadres = model.Emailadres;
                }

                if (handelaarInDB.Beschrijving != model.Beschrijving)
                {
                    handelaarInDB.Beschrijving = model.Beschrijving;
                }

                if (handelaarInDB.BTW_Nummer != model.BTW_Nummer)
                {
                    handelaarInDB.BTW_Nummer = model.BTW_Nummer;
                }

                if (handelaarInDB.Straat != model.Straat)
                {
                    handelaarInDB.Straat = model.Straat;
                }

                if (handelaarInDB.Huisnummer != model.Huisnummer)
                {
                    handelaarInDB.Huisnummer = model.Huisnummer;
                }

                if (handelaarInDB.Postcode != model.Postcode)
                {
                    handelaarInDB.Postcode = model.Postcode;
                }

                if (handelaarInDB.Gemeente != model.Gemeente)
                {
                    handelaarInDB.Gemeente = model.Gemeente;
                }
                var user = await _userManager.FindByEmailAsync(model.Emailadres);
                user.EmailConfirmed = true;

                var wachtwoord = Guid.NewGuid().ToString();
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                await _userManager.ResetPasswordAsync(user, token, wachtwoord);


                handelaarInDB.Goedgekeurd = true;
                _handelaarRepository.SaveChanges();

                if (model.Afbeelding != null)
                {
                    var filePath = @"wwwroot/images/handelaar/" + model.HandelaarId + "/logo.jpg";
                    Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                    var fileStream = new FileStream(filePath, FileMode.Create);
                    await model.Afbeelding.CopyToAsync(fileStream);
                    fileStream.Close();
                }

                var message = new MailMessage();
                message.From = new MailAddress("lekkerlokaalst@gmail.com");
                message.To.Add(model.Emailadres);
                message.Subject = "Uw verzoek om handelaar te worden op LekkerLokaal.be is geaccepteerd!";

                if (model.Opmerking != null)
                {
                    message.Body = String.Format("Beste medewerker van " + model.Naam + ", \n\n" +
                   "Uw recent verzoek om handelaar te worden bij LekkerLokaal.be is geaccepteerd! \n\n" +
                   model.Opmerking + "\n\n" +
                   "Uw gegevens om aan te melden zijn: \n" +
                   "E-mailadres: " + model.Emailadres + "\n" +
                   "Wachtwoord: " + wachtwoord + "\n\n" +
                   "We bevelen u aan om bij uw eerste aanmelding uw wachtwoord te wijzigen. \n\n" +
                   "Met vriendelijke groeten, \n" +
                  "Het Lekker Lokaal team");
                }
                else
                {
                    message.Body = String.Format("Beste medewerker van " + model.Naam + ", \n\n" +
                   "Uw recent verzoek om handelaar te worden bij LekkerLokaal.be is geaccepteerd! \n\n" +
                   "Uw gegevens om aan te melden zijn: \n" +
                   "E-mailadres: " + model.Emailadres + "\n" +
                   "Wachtwoord: " + wachtwoord + "\n\n" +
                   "We bevelen u aan om bij uw eerste aanmelding uw wachtwoord te wijzigen. \n\n" +
                   "Met vriendelijke groeten, \n" +
                  "Het Lekker Lokaal team");
                }

                var SmtpServer = new SmtpClient("smtp.gmail.com");
                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("lekkerlokaalst@gmail.com", "LokaalLekker123");
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(message);

                return RedirectToAction("HandelaarsVerzoeken");
            }
            return View(nameof(HandelaarVerzoekEvaluatie), model);
        }

        [HttpGet]
        public IActionResult CadeaubonVerzoekEvaluatie(int Id)
        {
            Bon geselecteerdebonEvaluatie = _bonRepository.GetByBonIdNotAccepted(Id);
            if (geselecteerdebonEvaluatie == null)
            {
                return RedirectToAction("CadeaubonVerzoeken");
            }
            ViewData["categorieen"] = new SelectList(_categorieRepository.GetAll().Select(c => c.Naam));
            ViewData["aanbiedingen"] = Aanbiedingen();
            return View(new CadeaubonBerwerkViewModel(geselecteerdebonEvaluatie));
        }

        [HttpGet]
        public IActionResult HandelaarToevoegen()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> HandelaarToevoegen(ManueelNieuweHandelaarViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var wachtwoord = Guid.NewGuid().ToString();
                var result = await _userManager.CreateAsync(user, wachtwoord);
                if (result.Succeeded)
                {
                    await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "handelaar"));

                    Handelaar nieuweHandelaar = new Handelaar(model.Naam, model.Email, model.Omschrijving, model.BtwNummer, model.Straatnaam, model.Huisnummer, model.Postcode, model.Gemeente, true);
                    _handelaarRepository.Add(nieuweHandelaar);
                    _handelaarRepository.SaveChanges();

                    var filePath = @"wwwroot/images/handelaar/" + nieuweHandelaar.HandelaarId + "/logo.jpg";
                    Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                    var fileStream = new FileStream(filePath, FileMode.Create);
                    await model.Afbeelding.CopyToAsync(fileStream);
                    fileStream.Close();


                    var message = new MailMessage();
                    message.From = new MailAddress("lekkerlokaalst@gmail.com");
                    message.To.Add(model.Email);
                    message.Subject = "Uw verzoek om handelaar te worden op LekkerLokaal.be is geaccepteerd!";

                    if (model.Opmerking != null)
                    {
                        message.Body = String.Format("Beste medewerker van " + model.Naam + ", \n" +
                       "U werd toegevoegd als handelaar op LekkerLokaal.be! \n\n" +
                       model.Opmerking + "\n\n" +
                       "Uw gegevens om aan te melden zijn: \n" +
                       "E-mailadres: " + model.Email + "\n" +
                       "Wachtwoord: " + wachtwoord + "\n\n" +
                       "We bevelen u aan om bij uw eerste aanmelding uw wachtwoord te wijzigen. \n\n" +
                       "Met vriendelijke groeten, \n" +
                      "Het Lekker Lokaal team");
                    }
                    else
                    {
                        message.Body = String.Format("Beste medewerker van " + model.Naam + ", \n" +
                       "U werd toegevoegd als handelaar op LekkerLokaal.be! \n\n" +
                       "Uw gegevens om aan te melden zijn: \n" +
                       "E-mailadres: " + model.Email + "\n" +
                       "Wachtwoord: " + wachtwoord + "\n\n" +
                       "We bevelen u aan om bij uw eerste aanmelding uw wachtwoord te wijzigen. \n\n" +
                       "Met vriendelijke groeten, \n" +
                      "Het Lekker Lokaal team");
                    }

                    var SmtpServer = new SmtpClient("smtp.gmail.com");
                    SmtpServer.Port = 587;
                    SmtpServer.Credentials = new System.Net.NetworkCredential("lekkerlokaalst@gmail.com", "LokaalLekker123");
                    SmtpServer.EnableSsl = true;
                    SmtpServer.Send(message);

                    return RedirectToAction("Dashboard");
                }
            }
            return View(nameof(HandelaarToevoegen), model);
        }

        [HttpGet]
        public IActionResult HandelaarsOverzicht()
        {
            return View(new HandelaarsOverzichtViewModel(_handelaarRepository.GetHandelaarsGoedgekeurd(_handelaarRepository.GetAll())));
        }

        [HttpGet]
        public IActionResult HandelaarBewerken(int Id)
        {
            Handelaar geselecteerdeHandelaar = _handelaarRepository.GetByHandelaarId(Id);
            if (geselecteerdeHandelaar == null)
            {
                return RedirectToAction("HandelaarsOverzicht");
            }
            return View(new HandelaarBewerkViewModel(geselecteerdeHandelaar));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> HandelaarBewerken(HandelaarBewerkViewModel model)
        {
            if (ModelState.IsValid)
            {
                Handelaar handelaarInDB = _handelaarRepository.GetByHandelaarId(model.HandelaarId);

                if (handelaarInDB.Naam != model.Naam)
                {
                    handelaarInDB.Naam = model.Naam;
                }

                if (handelaarInDB.Emailadres != model.Emailadres)
                {
                    handelaarInDB.Emailadres = model.Emailadres;
                }

                if (handelaarInDB.Beschrijving != model.Beschrijving)
                {
                    handelaarInDB.Beschrijving = model.Beschrijving;
                }

                if (handelaarInDB.BTW_Nummer != model.BTW_Nummer)
                {
                    handelaarInDB.BTW_Nummer = model.BTW_Nummer;
                }

                if (handelaarInDB.Straat != model.Straat)
                {
                    handelaarInDB.Straat = model.Straat;
                }

                if (handelaarInDB.Huisnummer != model.Huisnummer)
                {
                    handelaarInDB.Huisnummer = model.Huisnummer;
                }

                if (handelaarInDB.Postcode != model.Postcode)
                {
                    handelaarInDB.Postcode = model.Postcode;
                }

                if (handelaarInDB.Gemeente != model.Gemeente)
                {
                    handelaarInDB.Gemeente = model.Gemeente;
                }

                _handelaarRepository.SaveChanges();

                if (model.Afbeelding != null)
                {
                    var filePath = @"wwwroot/images/handelaar/" + model.HandelaarId + "/logo.jpg";
                    Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                    var fileStream = new FileStream(filePath, FileMode.Create);
                    await model.Afbeelding.CopyToAsync(fileStream);
                    fileStream.Close();
                }

                return RedirectToAction("HandelaarsOverzicht");
            }
            return View(nameof(HandelaarBewerken), model);
        }

        [HttpGet]
        public IActionResult CadeaubonVerzoeken()
        {
            //implement
            return View(new CadeaubonVerzoekenViewModel(_bonRepository.GetBonNogNietGoedgekeurd(_bonRepository.GetAll())));
        }

        [HttpGet]
        public IActionResult CadeaubonToevoegen()
        {
            ViewData["categorieen"] = new SelectList(_categorieRepository.GetAll().Select(c => c.Naam));
            ViewData["aanbiedingen"] = Aanbiedingen();
            return View();
        }

        private SelectList Aanbiedingen()
        {
            var Aanbiedingen = new List<Aanbieding>();
            foreach (Aanbieding aanbieding in Enum.GetValues(typeof(Aanbieding)))
            {
                Aanbiedingen.Add(aanbieding);
            }
            return new SelectList(Aanbiedingen);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CadeaubonToevoegen(ManueelNieuweBonViewModel model)
        {
            if (ModelState.IsValid)
            {
                Bon nieuweBon = new Bon(model.Naam, model.MinimumPrijs, model.Maximumprijs, model.Beschrijving, 0, @"temp", _categorieRepository.GetByNaam(model.Categorie), model.Straatnaam, model.Huisnummer, model.Postcode, model.Gemeente, _handelaarRepository.GetByHandelaarId(model.HandelaarID), model.Aanbieding, true);
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


                return RedirectToAction("CadeaubonOverzicht");

            }
            ViewData["categorieen"] = new SelectList(_categorieRepository.GetAll().Select(c => c.Naam));
            ViewData["aanbiedingen"] = Aanbiedingen();
            return View(nameof(CadeaubonToevoegen), model);
        }


        [HttpGet]
        public IActionResult CadeaubonOverzicht()
        {
            return View(new CadeaubonOverzichtViewModel(_bonRepository.GetAllGoedgekeurd()));
        }

        [HttpGet]
        public IActionResult CadeaubonBewerken(int Id)
        {
            Bon geselecteerdeBon = _bonRepository.GetByBonId(Id);
            if (geselecteerdeBon == null)
            {
                return RedirectToAction("CadeaubonOverzicht");
            }
            ViewData["categorieen"] = new SelectList(_categorieRepository.GetAll().Select(c => c.Naam));
            ViewData["aanbiedingen"] = Aanbiedingen();
            return View(new CadeaubonBerwerkViewModel(geselecteerdeBon));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CadeaubonBewerken(CadeaubonBerwerkViewModel model)
        {
            if (ModelState.IsValid)
            {
                Bon bonInDb = _bonRepository.GetByBonId(model.BonId);

                if (bonInDb.Naam != model.Naam)
                {
                    bonInDb.Naam = model.Naam;
                }

                if (bonInDb.Beschrijving != model.Beschrijving)
                {
                    bonInDb.Beschrijving = model.Beschrijving;
                }

                if (bonInDb.MinPrijs != model.MinimumPrijs)
                {
                    bonInDb.MinPrijs = model.MinimumPrijs;
                }

                if (bonInDb.MaxPrijs != model.Maximumprijs)
                {
                    bonInDb.MaxPrijs = model.Maximumprijs;
                }

                if (bonInDb.Categorie.Naam != model.Categorie)
                {
                    bonInDb.Categorie = _categorieRepository.GetByNaam(model.Categorie);
                }

                if (bonInDb.Straat != model.Straatnaam)
                {
                    bonInDb.Straat = model.Straatnaam;
                }

                if (bonInDb.Huisnummer != model.Huisnummer)
                {
                    bonInDb.Huisnummer = model.Huisnummer;
                }

                if (bonInDb.Postcode != model.Postcode)
                {
                    bonInDb.Postcode = model.Postcode;
                }

                if (bonInDb.Gemeente != model.Gemeente)
                {
                    bonInDb.Gemeente = model.Gemeente;
                }

                if (bonInDb.Aanbieding != model.Aanbieding)
                {
                    bonInDb.Aanbieding = model.Aanbieding;
                }

                _bonRepository.SaveChanges();

                if (model.Thumbnail != null)
                {
                    var filePath = @"wwwroot/" + bonInDb.Afbeelding + "thumb.jpg";
                    Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                    var fileStream = new FileStream(filePath, FileMode.Create);
                    await model.Thumbnail.CopyToAsync(fileStream);
                    fileStream.Close();
                }

                if (model.Afbeeldingen != null)
                {
                    System.IO.DirectoryInfo di = new DirectoryInfo(@"wwwroot/" + bonInDb.Afbeelding + "Afbeeldingen/");

                    foreach (FileInfo file in di.GetFiles())
                    {
                        file.Delete();
                    }

                    for (int i = 0; i < model.Afbeeldingen.Count; i++)
                    {
                        var filePath = @"wwwroot/" + bonInDb.Afbeelding + "Afbeeldingen/" + (i + 1) + ".jpg";
                        Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                        var fileStream = new FileStream(filePath, FileMode.Create);
                        await model.Afbeeldingen[i].CopyToAsync(fileStream);
                        fileStream.Close();
                    }
                }

                return RedirectToAction("CadeaubonOverzicht");
            }
            return View(nameof(CadeaubonBewerken), model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult VerwijderCadeaubonVerzoek(CadeaubonBerwerkViewModel model)
        {
            if (ModelState.IsValid)
            {
                Bon bonInDb = _bonRepository.GetByBonIdNotAccepted(model.BonId);
                _bonRepository.Remove(model.BonId);
                _bonRepository.SaveChanges();

                var filePath = @"wwwroot/" + bonInDb.Afbeelding;
                Directory.Delete(filePath, true);


                var emailadres = bonInDb.Handelaar.Emailadres;

                var message = new MailMessage();
                message.From = new MailAddress("lekkerlokaalst@gmail.com");
                message.To.Add(emailadres);
                message.Subject = "Uw verzoek om een nieuwe bon toe te voegen op LekkerLokaal.be is geweigerd.";

                if (model.Opmerking != null)
                {
                    message.Body = String.Format("Beste medewerker van " + model.naamHandelaar + ", \n\n" +
                   "Uw recent verzoek om een bon toe te voegen bij LekkerLokaal.be is geweigerd. \n\n" +
                   model.Opmerking + "\n\n" +
                   "Als u denkt dat u alsnog recht heeft om deze bon toe te voegen bij LekkerLokaal.be raden wij u aan een nieuw verzoek te versturen. \n\n" +
                   "Met vriendelijke groeten, \n" +
                  "Het Lekker Lokaal team");
                }
                else
                {
                    message.Body = String.Format("Beste medewerker van " + model.naamHandelaar + ", \n\n" +
                    "Uw recent verzoek om een bon toe te voegen bij LekkerLokaal.be is geweigerd. \n\n" +
                    "Als u denkt dat u alsnog recht heeft om deze bon toe te voegen bij LekkerLokaal.be raden wij u aan een nieuw verzoek te versturen. \n\n" +
                    "Met vriendelijke groeten, \n" +
                   "Het Lekker Lokaal team");
                }

                var SmtpServer = new SmtpClient("smtp.gmail.com");
                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("lekkerlokaalst@gmail.com", "LokaalLekker123");
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(message);

                return RedirectToAction("CadeaubonVerzoeken");
            }
            return View(nameof(HandelaarVerzoekEvaluatie), model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AccepteerCadeaubonVerzoek(CadeaubonBerwerkViewModel model)
        {
            if (ModelState.IsValid)
            {
                Bon bonInDb = _bonRepository.GetByBonIdNotAccepted(model.BonId);

                if (bonInDb.Naam != model.Naam)
                {
                    bonInDb.Naam = model.Naam;
                }

                if (bonInDb.Beschrijving != model.Beschrijving)
                {
                    bonInDb.Beschrijving = model.Beschrijving;
                }

                if (bonInDb.MinPrijs != model.MinimumPrijs)
                {
                    bonInDb.MinPrijs = model.MinimumPrijs;
                }

                if (bonInDb.MaxPrijs != model.Maximumprijs)
                {
                    bonInDb.MaxPrijs = model.Maximumprijs;
                }

                if (bonInDb.Categorie.Naam != model.Categorie)
                {
                    bonInDb.Categorie = _categorieRepository.GetByNaam(model.Categorie);
                }

                if (bonInDb.Straat != model.Straatnaam)
                {
                    bonInDb.Straat = model.Straatnaam;
                }

                if (bonInDb.Huisnummer != model.Huisnummer)
                {
                    bonInDb.Huisnummer = model.Huisnummer;
                }

                if (bonInDb.Postcode != model.Postcode)
                {
                    bonInDb.Postcode = model.Postcode;
                }

                if (bonInDb.Gemeente != model.Gemeente)
                {
                    bonInDb.Gemeente = model.Gemeente;
                }

                if (bonInDb.Aanbieding != model.Aanbieding)
                {
                    bonInDb.Aanbieding = model.Aanbieding;
                }

                bonInDb.Goedgekeurd = true;

                _bonRepository.SaveChanges();

                if (model.Thumbnail != null)
                {
                    var filePath = @"wwwroot/" + bonInDb.Afbeelding + "thumb.jpg";
                    Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                    var fileStream = new FileStream(filePath, FileMode.Create);
                    await model.Thumbnail.CopyToAsync(fileStream);
                    fileStream.Close();
                }

                if (model.Afbeeldingen != null)
                {
                    System.IO.DirectoryInfo di = new DirectoryInfo(@"wwwroot/" + bonInDb.Afbeelding + "Afbeeldingen/");

                    foreach (FileInfo file in di.GetFiles())
                    {
                        file.Delete();
                    }

                    for (int i = 0; i < model.Afbeeldingen.Count; i++)
                    {
                        var filePath = @"wwwroot/" + bonInDb.Afbeelding + "Afbeeldingen/" + (i + 1) + ".jpg";
                        Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                        var fileStream = new FileStream(filePath, FileMode.Create);
                        await model.Afbeeldingen[i].CopyToAsync(fileStream);
                        fileStream.Close();
                    }
                }


                var emailadres = bonInDb.Handelaar.Emailadres;

                var message = new MailMessage();
                message.From = new MailAddress("lekkerlokaalst@gmail.com");
                message.To.Add(emailadres);
                message.Subject = "Uw verzoek om een nieuwe bon toe te voegen op LekkerLokaal.be is geaccepteerd!";

                if (model.Opmerking != null)
                {
                    message.Body = String.Format("Beste medewerker van " + model.naamHandelaar + ", \n\n" +
                   "Uw recent verzoek om een bon toe te voegen bij LekkerLokaal.be is geaccepteerd. \n\n" +
                   model.Opmerking + "\n\n" +
                   "Met vriendelijke groeten, \n" +
                  "Het Lekker Lokaal team");
                }
                else
                {
                    message.Body = String.Format("Beste medewerker van " + model.naamHandelaar + ", \n\n" +
                    "Uw recent verzoek om een bon toe te voegen bij LekkerLokaal.be is geaccepteerd. \n\n" +
                    "Met vriendelijke groeten, \n" +
                   "Het Lekker Lokaal team");
                }

                var SmtpServer = new SmtpClient("smtp.gmail.com");
                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("lekkerlokaalst@gmail.com", "LokaalLekker123");
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(message);

                return RedirectToAction("CadeaubonOverzicht");
            }
            return View(nameof(CadeaubonVerzoekEvaluatie), model);
        }

        [HttpGet]
        public IActionResult LayoutSliderIndex()
        {
            return View(new CadeaubonOverzichtViewModel(_bonRepository.GetBonnenAanbiedingSlider(_bonRepository.GetAllGoedgekeurd())));
        }

        [HttpGet]
        public IActionResult LayoutAanbiedingen()
        {
            return View(new CadeaubonOverzichtViewModel(_bonRepository.GetBonnenAanbiedingStandaardEnSlider(_bonRepository.GetAllGoedgekeurd())));
        }

        [HttpGet]
        public IActionResult VerkochteCadeaubonBekijken(int Id)
        {
            GeneratePDF(Id);
            return View(new VerkochteCadeaubonBekijkenViewModel(_bestellijnRepository.GetById(Id)));
        }

        [HttpGet]
        public IActionResult VerkochteCadeaubonnen()
        {
            return View(new OverzichtVerkochteBonnenViewModel(_bestellijnRepository.getVerkochteBonnen()));
        }

        [HttpGet]
        public IActionResult GebruikteCadeaubonnen()
        {
            return View(new OverzichtGebruikteBonnenViewModel(_bestellijnRepository.getGebruikteBonnen()));
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

        public void GeneratePDF(int Id)
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
            var pdf = new Document(PageSize.A5.Rotate(), 81, 225, 25, 0);
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
            Paragraph geschonkenDoor = new Paragraph("Geschonken door: " + gebruiker.Voornaam, arial18);
            Paragraph geldig = new Paragraph(geldigheid, arial18);

            bedrag.Alignment = Element.ALIGN_LEFT;

            naamHandelaar.Alignment = Element.ALIGN_LEFT;
            geschonkenDoor.Alignment = Element.ALIGN_LEFT;
            geldig.Alignment = Element.ALIGN_LEFT;

            PdfWriter writer = PdfWriter.GetInstance(pdf, new FileStream(@"wwwroot/pdf/c_" + bestellijn.QRCode + ".pdf", FileMode.Create));
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

    }
}