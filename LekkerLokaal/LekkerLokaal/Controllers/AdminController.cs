using System;
using System.Collections.Generic;
using System.Linq;
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

namespace LekkerLokaal.Controllers
{
    [Authorize(Roles = "admin")]
    [Route("[controller]/[action]")]
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IHandelaarRepository _handelaarRepository;
        private readonly ILogger _logger;

        public AdminController(
            UserManager<ApplicationUser> userManager,
            ILogger<AdminController> logger,
            SignInManager<ApplicationUser> signInManager,
            IHandelaarRepository handelaarRepository)
        {
            _userManager = userManager;
            _logger = logger;
            _signInManager = signInManager;
            _handelaarRepository = handelaarRepository;
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
                var claims = await _userManager.GetClaimsAsync(await _userManager.FindByEmailAsync(model.Email));

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
            return View(new DashboardViewModel(_handelaarRepository.getAantalHandelaarsverzoeken()));
        }

        [HttpGet]
        public IActionResult VerkochteCadeaubonnen()
        {
            return View();
        }

        [HttpGet]
        public IActionResult UitbetaaldeCadeaubonnen()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ZoekHandelaar()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ZoekCadeaubon()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ZoekVerkochteCadeaubon()
        {
            return View();
        }

        [HttpGet]
        public IActionResult HandelaarsVerzoeken()
        {
            return View(new HandelaarsVerzoekenViewModel(_handelaarRepository.GetHandelaarsNogNietGoedgekeurd(_handelaarRepository.GetAll())));
        }

        [HttpGet]
        public IActionResult HandelaarVerzoekEvaluatie(int Id)
        {
            Handelaar geselecteerdeHandelaarEvaluatie = _handelaarRepository.GetByHandelaarId(Id);
            return View(new HandelaarEvaluatieViewModel(geselecteerdeHandelaarEvaluatie));
        }

        [HttpPost]
        public IActionResult VerwijderHandelaarVerzoek(HandelaarEvaluatieViewModel model)
        {
            if (ModelState.IsValid)
            {
                var message = new MailMessage();
                message.From = new MailAddress("lekkerlokaalst@gmail.com");
                message.To.Add(model.Emailadres);
                message.Subject = "Uw verzoek om handelaar te worden op LekkerLokaal.be is geweigerd.";

                if (model.Opmerking != null)
                {
                    message.Body = String.Format("Beste, \n" +
                   "Uw recent verzoek om handelaar te worden bij LekkerLokaal.be is geweigerd. \n\n" +
                   model.Opmerking +
                   "Als u denkt dat u alsnog recht heeft om handelaar te worden bij LekkerLokaal.be raden wij u aan een nieuw verzoek te versturen. \n\n" +
                   "Met vriendelijke groet, \n" +
                  "Het Lekker Lokaal team");
                }
                else
                {
                    message.Body = String.Format("Beste, \n" +
                  "Uw recent verzoek om handelaar te worden bij LekkerLokaal.be is geweigerd. \n\n" +
                  "Als u denkt dat u alsnog recht heeft om handelaar te worden bij LekkerLokaal.be raden wij u aan een nieuw verzoek te versturen. \n\n" +
                  "Met vriendelijke groet, \n\n" +
                  "Het Lekker Lokaal team");
                }

                var SmtpServer = new SmtpClient("smtp.gmail.com");
                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("lekkerlokaalst@gmail.com", "LokaalLekker123");
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(message);

                


                return RedirectToAction("Dashboard");
            }
            return View(nameof(HandelaarVerzoekEvaluatie), model);
        }

        [HttpGet]
        public IActionResult CadeaubonVerzoekEvaluatie()
        {
            return View();
        }

        [HttpGet]
        public IActionResult HandelaarToevoegen()
        {
            return View();
        }

        [HttpGet]
        public IActionResult HandelaarsOverzicht()
        {
            return View();
        }

        [HttpGet]
        public IActionResult HandelaarBewerken()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CadeaubonVerzoeken()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CadeaubonToevoegen()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CadeaubonOverzicht()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CadeaubonBewerken()
        {
            return View();
        }

        [HttpGet]
        public IActionResult LayoutSliderIndex()
        {
            return View();
        }

        [HttpGet]
        public IActionResult LayoutAanbiedingen()
        {
            return View();
        }

    }
}