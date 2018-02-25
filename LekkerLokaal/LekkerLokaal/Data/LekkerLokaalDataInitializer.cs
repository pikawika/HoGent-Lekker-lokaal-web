using Microsoft.AspNetCore.Identity;
using LekkerLokaal.Models.Domain;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using LekkerLokaal.Models;

namespace LekkerLokaal.Data
{
    public class LekkerLokaalDataInitializer
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public LekkerLokaalDataInitializer(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task InitializeData()
        {
            _dbContext.Database.EnsureDeleted();
            if (_dbContext.Database.EnsureCreated())
            {
                Categorie eten_drinken = new Categorie("Eten & drinken");
                Categorie events = new Categorie("Events");
                Categorie beauty = new Categorie("Beauty");
                Categorie fitness = new Categorie("Fitness");
                Categorie interieur = new Categorie("Interieur");
                Categorie kledij = new Categorie("Kledij");
                Categorie shopping = new Categorie("Shopping");
                Categorie uitstappen = new Categorie("Uitstappen");
                Categorie huis_tuin = new Categorie("Huis & Tuin");
                Categorie generiek = new Categorie("Generiek");

                var categories = new List<Categorie>
                {
                    eten_drinken, events, beauty, fitness, interieur, kledij, shopping, uitstappen, huis_tuin, generiek
                };
                _dbContext.Categorieen.AddRange(categories);

                Bon bon01 = new Bon("Restaurant lekker", 50, "3 sterren resaurant in het centrum van Aalst.", 0, "string01", eten_drinken);
                Bon bon02 = new Bon("Dessertbar chez Bontinck", 30, "Met passie gemaakte dessertjes in het mooie Schellebelle.", 0, "string02", eten_drinken);
                Bon bon03 = new Bon("Bierspecialist Schets", 20, "Meer dan 70 Belgische bieren in een gezellige kroeg.", 0, "string03", eten_drinken);
                Bon bon04 = new Bon("De Coninck's cocktail", 15, "Een VIP cocktailbar met live optredens van lokale muzikanten.", 0, "string04", eten_drinken);

                Bon bon05 = new Bon("Wijnproeverij BraLenBre", 75, "Keuze uit verschillende wijnen vergezeld met een hapje.", 0, "string05", eten_drinken);
                Bon bon06 = new Bon("Veggiebar 't Sandwichke", 30, "Het bewijs dat vegetarisch eten lekker kan zijn.", 0, "string06", eten_drinken);
                Bon bon07 = new Bon("Fastfood McDonalds", 5, "De keten met keuzes voor iedereen.", 0, "string07", eten_drinken);
                Bon bon08 = new Bon("Restaurant SOS Piet", 150, "5 sterren restaurant met de enige echte SOS Piet als kok.", 0, "string08", eten_drinken);

                Bon bon09 = new Bon("Generieke cadeaubon", 50, "Niet zeker welke bon u juist wilt, dan is deze generieke bon iets voor u!", 0, "string09", generiek);
                Bon bon10 = new Bon("Fitness Basic-Fit", 30, "Bekenste fitness van België met vestigingen over het hele land.", 0, "string10", fitness);
                Bon bon11 = new Bon("Nachtwandeling Aalst at night", 10, "Geniet van de sterrenhemel in de mooie streken van Aalst (met gids).", 0, "string11", uitstappen);

                var bonnen = new List<Bon>
                {
                    bon01, bon02, bon03, bon04, bon05, bon06, bon07, bon08, bon09, bon10, bon11
                };

                _dbContext.Bonnen.AddRange(bonnen);

                Klant user01 = new Klant("Brent", "Schets", Geslacht.Man, "brent@schets.com");
                Klant user02 = new Klant("Bram", "De Coninck", Geslacht.Man, "bram@deconinck.com");
                Klant user03 = new Klant("Lennert", "Bontinck", Geslacht.Man, "lennert@bontinck.com");

                var gebruikers = new List<Klant>
                {
                    user01, user02, user03
                };

                _dbContext.Klanten.AddRange(gebruikers);

                Handelaar handelaar = new Handelaar("Wijnproverij BraLenBre", "bralenbre@gmail.com", "Lange Zoutstraat", 9300, "Aalst", "Met deze bon kan je bij wijnproeverij BraLenBre genieten van een gezellige avond. Je zal er meer uitleg krijgen over de verschillende soorten wijnen en van elke soort mogen proeven, allen vergezeld met een passend hapje. Eens de sessie over is kan met de bon, wijn gekocht worden. Enkele merken die je hier kan verwachten zijn: Francis Ford Coppola, Franschhoek Cellar, Fushs Reinhardt, Gran Sasso, Grande Provence, Guadalupe, Guillamen I Muri, ..."
                    , "BE 999 999 999", "@Test01", "string101");

                var handelaars = new List<Handelaar>
                {
                    handelaar
                };

                _dbContext.Handelaars.AddRange(handelaars);

                await CreateUser("admin@sportsstore.be", "admin@sportsstore.be", "P@ssword1!", "Admin");
                _dbContext.SaveChanges();
            }
        }

        private async Task CreateUser(string userName, string email, string password, string role)
        {
            var user = new ApplicationUser { UserName = userName, Email = email };
            await _userManager.CreateAsync(user, password);
            await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, role));
        }
    }
}
