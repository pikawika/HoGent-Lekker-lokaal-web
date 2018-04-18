using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LekkerLokaal.Models;
using LekkerLokaal.Models.AdminViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LekkerLokaal.Controllers
{
    [Authorize(Roles = "admin")]
    [Route("[controller]/[action]")]
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;

        public AdminController(
            UserManager<ApplicationUser> userManager,
            ILogger<AdminController> logger,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _logger = logger;
            _signInManager = signInManager;
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
            return View();
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
        public IActionResult ZoekVerkochteCadeaubon()
        {
            return View();
        }

        [HttpGet]
        public IActionResult HandelaarsVerzoeken()
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