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
                Categorie eten_drinken = new Categorie("Eten & drinken", "fa-cutlery");
                Categorie fitness = new Categorie("Fitness", "fa-bolt");
                Categorie uitstappen = new Categorie("Uitstappen", "fa-plane");
                Categorie huis_tuin = new Categorie("Huis & Tuin", "fa-home");

                Categorie events = new Categorie("Events", "fa-calendar");
                Categorie beauty = new Categorie("Beauty", "fa-female");
                Categorie interieur = new Categorie("Interieur", "fa-image");
                Categorie kledij = new Categorie("Kledij", "fa-umbrella");
                Categorie multimedia = new Categorie("Multimedia", "fa-shopping-cart");
                Categorie generiek = new Categorie("Generiek", "fa-gift");

                var categories = new List<Categorie>
                {
                    eten_drinken, events, beauty, fitness, interieur, kledij, multimedia, uitstappen, huis_tuin, generiek
                };
                _dbContext.Categorie.AddRange(categories);


                Handelaar BraLenBre = new Handelaar("Wijnproverij BraLenBre", "bralenbre@gmail.com", "Lange Zoutstraat", 9300, "Aalst", "Met deze bon kan je bij wijnproeverij BraLenBre genieten van een gezellige avond. Je zal er meer uitleg krijgen over de verschillende soorten wijnen en van elke soort mogen proeven, allen vergezeld met een passend hapje. Eens de sessie over is kan met de bon, wijn gekocht worden. Enkele merken die je hier kan verwachten zijn: Francis Ford Coppola, Franschhoek Cellar, Fushs Reinhardt, Gran Sasso, Grande Provence, Guadalupe, Guillamen I Muri, ..."
                    , "BE 999 999 999", "@Test01", "string101");

                var handelaars = new List<Handelaar>
                {
                    BraLenBre
                };

                _dbContext.Handelaar.AddRange(handelaars);


                Bon bon01 = new Bon("Restaurant lekker", 1, 50, "3 sterren resaurant in het centrum van Aalst.", 17, @"images\bon\1\thumb.jpg", eten_drinken, "straat", "huisnummer", 9260, "Schellebelle", BraLenBre);
                Bon bon02 = new Bon("Dessertbar chez Bontinck", 1, 30, "Met passie gemaakte dessertjes in het mooie Schellebelle.", 242, @"images\bon\2\thumb.jpg", eten_drinken, "straat", "huisnummer", 9260, "Schellebelle", BraLenBre);
                Bon bon03 = new Bon("Bierspecialist Schets", 1, 20, "Meer dan 70 Belgische bieren in een gezellige kroeg.", 42, @"images\bon\3\thumb.jpg", eten_drinken, "straat", "huisnummer", 9260, "Schellebelle", BraLenBre);
                Bon bon04 = new Bon("De Coninck's cocktail", 1, 15, "Een VIP cocktailbar met live optredens van lokale muzikanten.", 24, @"images\bon\4\thumb.jpg", eten_drinken, "straat", "huisnummer", 9260, "Schellebelle", BraLenBre);
                Bon bon05 = new Bon("Wijnproeverij BraLenBre", 1, 75, "Keuze uit verschillende wijnen vergezeld met een hapje.", 41, @"images\bon\5\thumb.jpg", eten_drinken, "straat", "huisnummer", 9260, "Schellebelle", BraLenBre);
                Bon bon06 = new Bon("Veggiebar 't Sandwichke", 1, 30, "Het bewijs dat vegetarisch eten lekker kan zijn.", 45, @"images\bon\6\thumb.jpg", eten_drinken, "straat", "huisnummer", 9260, "Schellebelle", BraLenBre);
                Bon bon07 = new Bon("Fastfood McDonalds", 1, 5, "De keten met keuzes voor iedereen.", 246, @"images\bon\7\thumb.jpg", eten_drinken, "straat", "huisnummer", 9260, "Schellebelle", BraLenBre);
                Bon bon08 = new Bon("Restaurant SOS Piet", 1, 150, "5 sterren restaurant met de enige echte SOS Piet als kok.", 21, @"images\bon\8\thumb.jpg", eten_drinken, "straat", "huisnummer", 9260, "Schellebelle", BraLenBre);
                Bon bon09 = new Bon("Wijnproeverij CoBoSh", 1, 75, "Keuze uit verschillende wijnen vergezeld met een hapje.", 47, @"images\bon\9\thumb.jpg", eten_drinken, "straat", "huisnummer", 9260, "Schellebelle", BraLenBre);
                Bon bon10 = new Bon("Wijnproeverij CoBoSh", 1, 75, "Keuze uit verschillende wijnen vergezeld met een hapje.", 22, @"images\bon\10\thumb.jpg", eten_drinken, "straat", "huisnummer", 9260, "Schellebelle", BraLenBre);


                Bon bon11 = new Bon("Fitness Basic-Fit", 1, 30, "Bekenste fitness van België met vestigingen over het hele land.", 83, @"images\bon\11\thumb.jpg", fitness, "straat", "huisnummer", 9260, "Schellebelle", BraLenBre);
                Bon bon12 = new Bon("Fitness Basic-Fit Aalst", 1, 30, "Bekenste fitness van België met vestigingen over het hele land.", 75, @"images\bon\12\thumb.jpg", fitness, "straat", "huisnummer", 9260, "Schellebelle", BraLenBre);
                Bon bon13 = new Bon("Fitness Basic-Fit Gent", 1, 30, "Bekenste fitness van België met vestigingen over het hele land.", 8, @"images\bon\13\thumb.jpg", fitness, "straat", "huisnummer", 9260, "Schellebelle", BraLenBre);
                Bon bon14 = new Bon("Fitness Basic-Fit Brussel", 1, 30, "Bekenste fitness van België met vestigingen over het hele land.", 53, @"images\bon\14\thumb.jpg", fitness, "straat", "huisnummer", 9260, "Schellebelle", BraLenBre);
                Bon bon15 = new Bon("Fitness Basic-Fit Brugge", 1, 30, "Bekenste fitness van België met vestigingen over het hele land.", 53, @"images\bon\15\thumb.jpg", fitness, "straat", "huisnummer", 9260, "Schellebelle", BraLenBre);
                Bon bon16 = new Bon("Fitness Basic-Fit Sint-Truiden", 1, 30, "Bekenste fitness van België met vestigingen over het hele land.", 72, @"images\bon\16\thumb.jpg", fitness, "straat", "huisnummer", 9260, "Schellebelle", BraLenBre);
                Bon bon17 = new Bon("Fitness Basic-Fit ", 1, 30, "Bekenste fitness van België met vestigingen over het hele land.", 65, @"images\bon\17\thumb.jpg", fitness, "straat", "huisnummer", 9260, "Schellebelle", BraLenBre);
                Bon bon18 = new Bon("Fitness Basic-Fit", 1, 30, "Bekenste fitness van België met vestigingen over het hele land.", 22, @"images\bon\18\thumb.jpg", fitness, "straat", "huisnummer", 9260, "Schellebelle", BraLenBre);
                Bon bon19 = new Bon("Fitness Basic-Fit", 1, 30, "Bekenste fitness van België met vestigingen over het hele land.", 75, @"images\bon\19thumb.jpg", fitness, "straat", "huisnummer", 9260, "Schellebelle", BraLenBre);


                Bon bon21 = new Bon("Nachtwandeling Aalst at night", 1, 10, "Geniet van de sterrenhemel in de mooie streken van Aalst (met gids).", 63, @"images\bon\11\thumb.jpg", uitstappen, "straat", "huisnummer", 9260, "Schellebelle", BraLenBre);
                Bon bon22 = new Bon("Dagje wallibi in Dendermonde", 25, 142, "Wat is er nu leuker dan een dagje wallibi met vrienden", 34, @"images\bon\22\thumb.jpg", uitstappen, "straat", "huisnummer", 9260, "Schellebelle", BraLenBre);
                Bon bon23 = new Bon("Met de trein naar Oostende", 36, 159, "Spring zong er al over dus wat houd je tegen het te doen", 45, @"images\bon\23\thumb.jpg", uitstappen, "straat", "huisnummer", 9260, "Schellebelle", BraLenBre);
                Bon bon24 = new Bon("Weekendje disneyland parijs", 29, 251, "Disneyland de bestemming voor groot en klein", 35, @"images\bon\24\thumb.jpg", uitstappen, "straat", "huisnummer", 9260, "Schellebelle", BraLenBre);
                Bon bon25 = new Bon("De grotten van Ham", 50, 264, "Het liedje zit ongetwijfeld al in je hoofd dus ga nu gewoon", 86, @"images\bon\25\thumb.jpg", uitstappen, "straat", "huisnummer", 9260, "Schellebelle", BraLenBre);
                Bon bon26 = new Bon("Historisch bezoek Breemdonk", 6, 185, "Voor de oorlog fanaten een must", 35, @"images\bon\26\thumb.jpg", uitstappen, "straat", "huisnummer", 9260, "Schellebelle", BraLenBre);

                Bon bon31 = new Bon("Bloemencenter Brenk", 1, 35, "Stel zelf uw setje bloemen samen met deze bon.", 43, @"images\bon\12\thumb.jpg", huis_tuin, "straat", "huisnummer", 9260, "Schellebelle", BraLenBre);
                Bon bon32 = new Bon("Potgrond De Mol in Lede", 22, 233, "Heb je grond nodig voor in een pot", 68, @"images\bon\32\thumb.jpg", huis_tuin, "straat", "huisnummer", 9260, "Schellebelle", BraLenBre);
                Bon bon33 = new Bon("Schelfhout Ten Aalst", 36, 345, "Schelfhout, waar moet je andes zijn!", 75, @"images\bon\33\thumb.jpg", huis_tuin, "straat", "huisnummer", 9260, "Schellebelle", BraLenBre);
                Bon bon34 = new Bon("Bloemetje liesje in Gent", 13, 468, "Lies, verkoopt ook wel een madelief ", 25, @"images\bon\34\thumb.jpg", huis_tuin, "straat", "huisnummer", 9260, "Schellebelle", BraLenBre);
                Bon bon35 = new Bon("Funa Lima tuincentrum Lede", 26, 232, "Vissen, fonteinen, dieraccesoire...", 14, @"images\bon\35\thumb.jpg", huis_tuin, "straat", "huisnummer", 9260, "Schellebelle", BraLenBre);
                Bon bon36 = new Bon("Vijvervoorziening Blub", 30, 404, "Blub, de winkel voor vis enthousiasten", 35, @"images\bon\36\thumb.jpg", huis_tuin, "straat", "huisnummer", 9260, "Schellebelle", BraLenBre);
                Bon bon37 = new Bon("Grasmaaiers Bontinck", 25, 89, "Jaren ervaring in het snoeien", 76, @"images\bon\37\thumb.jpg", huis_tuin, "straat", "huisnummer", 9260, "Schellebelle", BraLenBre);
                Bon bon38 = new Bon("Aveve boerenbond te Aalst", 31, 416, "Bij de boerenbond vind je altijd wat je zoekt", 75, @"images\bon\38\thumb.jpg", huis_tuin, "straat", "huisnummer", 9260, "Schellebelle", BraLenBre);
                Bon bon39 = new Bon("Groener Gras In Wetteren", 12, 526, "Gazon voorzieningen", 14, @"images\bon\39\thumb.jpg", huis_tuin, "straat", "huisnummer", 9260, "Schellebelle", BraLenBre);

                Bon bon41 = new Bon("Pukkelpop weekend tickets", 21, 513, "Pukkelpop, dat moet je gedaan hebben", 57, @"images\bon\41\thumb.jpg", events, "straat", "huisnummer", 9260, "Schellebelle", BraLenBre);
                Bon bon42 = new Bon("Lokerse bierfeesten", 44, 393, "Bierfanaten kunnen dit niet missen", 75, @"images\bon\42\thumb.jpg", events, "straat", "huisnummer", 9260, "Schellebelle", BraLenBre);
                Bon bon43 = new Bon("Gentse feesten eetfestijn", 42, 464, "Drinken en eten, meer moet dat niet zijn", 25, @"images\bon\43\thumb.jpg", events, "straat", "huisnummer", 9260, "Schellebelle", BraLenBre);
                Bon bon44 = new Bon("Gameforce in de Nekkerhalle", 38, 179, "Voor elke nerd wat wils", 14, @"images\bon\44\thumb.jpg", events, "straat", "huisnummer", 9260, "Schellebelle", BraLenBre);
                Bon bon45 = new Bon("Drive A Ferrari Day", 8, 318, "Ideal geshenk voor een autofanaat", 38, @"images\bon\45\thumb.jpg", events, "straat", "huisnummer", 9260, "Schellebelle", BraLenBre);
                Bon bon46 = new Bon("Facts: trein en eten", 34, 197, "Cosplay, eten en vervoer", 18, @"images\bon\46\thumb.jpg", events, "straat", "huisnummer", 9260, "Schellebelle", BraLenBre);

                Bon bon51 = new Bon("Makeup pallete Nude", 29, 374, "Het bekendste merk zijn palette", 67, @"images\bon\51\thumb.jpg", beauty, "straat", "huisnummer", 9260, "Schellebelle", BraLenBre);
                Bon bon52 = new Bon("Ici Paris verwenbon", 15, 500, "Een parfum kan je nooit mee misdoen", 17, @"images\bon\52\thumb.jpg", beauty, "straat", "huisnummer", 9260, "Schellebelle", BraLenBre);
                Bon bon53 = new Bon("Lipstick Lover Aalst", 8, 404, "Voor de lippen lovers", 86, @"images\bon\53\thumb.jpg", beauty, "straat", "huisnummer", 9260, "Schellebelle", BraLenBre);

                Bon bon61 = new Bon("Sofa en Co", 18, 389, "Relax in een sofa van sofa en co", 71, @"images\bon\61\thumb.jpg", interieur, "straat", "huisnummer", 9260, "Schellebelle", BraLenBre);
                Bon bon62 = new Bon("Deba meubelen", 36, 202, "Verkoopt al uw interieur", 37, @"images\bon\62\thumb.jpg", interieur, "straat", "huisnummer", 9260, "Schellebelle", BraLenBre);
                Bon bon63 = new Bon("Ikea huisvoorzieningeb", 40, 375, "Meubelspiaclist sinds 74", 71, @"images\bon\63\thumb.jpg", interieur, "straat", "huisnummer", 9260, "Schellebelle", BraLenBre);
                Bon bon64 = new Bon("Leenbakker", 34, 335, "Om te kopen, niet te lenen", 37, @"images\bon\64\thumb.jpg", interieur, "straat", "huisnummer", 9260, "Schellebelle", BraLenBre);
                Bon bon65 = new Bon("Salon Ballon Gent", 13, 352, "Sallon Ballon is een speciaalzaak te Gent", 76, @"images\bon\65\thumb.jpg", interieur, "straat", "huisnummer", 9260, "Schellebelle", BraLenBre);
                Bon bon66 = new Bon("Keukens Donald", 45, 244, "Al 8 jaar maak ik keukens alsof ze voor mezelf zijn", 46, @"images\bon\66\thumb.jpg", interieur, "straat", "huisnummer", 9260, "Schellebelle", BraLenBre);
                Bon bon67 = new Bon("Modern Gent", 31, 510, "Modern interieur hoeft niet duur te zijn", 75, @"images\bon\67\thumb.jpg", interieur, "straat", "huisnummer", 9260, "Schellebelle", BraLenBre);
                Bon bon68 = new Bon("Kunst & Kitch", 18, 186, "Een beetje kunst, een beetje kitch", 46, @"images\bon\68\thumb.jpg", interieur, "straat", "huisnummer", 9260, "Schellebelle", BraLenBre);
                Bon bon69 = new Bon("Moderne interieur Gill", 5, 60, "Op maat gemaakt interieur tegen een zacht prijsje", 45, @"images\bon\69\thumb.jpg", interieur, "straat", "huisnummer", 9260, "Schellebelle", BraLenBre);

                Bon bon71 = new Bon("C&A Aalst", 24, 480, "De kledingwinkel in Aalst en omstreken", 42, @"images\bon\71\thumb.jpg", kledij, "straat", "huisnummer", 9260, "Schellebelle", BraLenBre);
                Bon bon72 = new Bon("AS Advantures", 48, 373, "Kledie om een avontuur mee aan te gaan", 47, @"images\bon\72\thumb.jpg", kledij, "straat", "huisnummer", 9260, "Schellebelle", BraLenBre);
                Bon bon73 = new Bon("Ultra Wet", 41, 413, "De kledingspecialist voor droog en nat", 71, @"images\bon\73\thumb.jpg", kledij, "straat", "huisnummer", 9260, "Schellebelle", BraLenBre);
                Bon bon74 = new Bon("Holiday", 24, 489, "Voor al uw feestkledij", 17, @"images\bon\74\thumb.jpg", kledij, "straat", "huisnummer", 9260, "Schellebelle", BraLenBre);
                Bon bon75 = new Bon("Bram's Fashion", 23, 86, "Voor ieder wat wilds", 73, @"images\bon\75\thumb.jpg", kledij, "straat", "huisnummer", 9260, "Schellebelle", BraLenBre);
                Bon bon76 = new Bon("Bontinck's Panthers", 30, 453, "Pants from Bontinck are dreams for legs", 72, @"images\bon\76\thumb.jpg", kledij, "straat", "huisnummer", 9260, "Schellebelle", BraLenBre);
                Bon bon77 = new Bon("Bre Bra", 29, 478, "Van A tot Z, u vindt het bij ons", 92, @"images\bon\77\thumb.jpg", kledij, "straat", "huisnummer", 9260, "Schellebelle", BraLenBre);
                Bon bon78 = new Bon("Pikantje", 34, 402, "Erotische kledingwinkel", 9, @"images\bon\78\thumb.jpg", kledij, "straat", "huisnummer", 9260, "Schellebelle", BraLenBre);

                Bon bon81 = new Bon("Fnac Aalst", 3, 377, "De multimedia specialist in Europa", 54, @"images\bon\81\thumb.jpg", multimedia, "straat", "huisnummer", 9260, "Schellebelle", BraLenBre);
                Bon bon82 = new Bon("Mediamarkt Dendermonde", 46, 433, "Electronica tegen een spot prijs", 45, @"images\bon\82\thumb.jpg", multimedia, "straat", "huisnummer", 9260, "Schellebelle", BraLenBre);
                Bon bon83 = new Bon("Van Den Borre Gent", 31, 181, "Koffiezets voor 12€", 15, @"images\bon\83\thumb.jpg", multimedia, "straat", "huisnummer", 9260, "Schellebelle", BraLenBre);
                Bon bon84 = new Bon("Bontinck IT brugge", 36, 539, "Een probleempje groot of klein, dan moet je bij IT Lennert zijn", 67, @"images\bon\84\thumb.jpg", multimedia, "straat", "huisnummer", 9260, "Schellebelle", BraLenBre);
                Bon bon85 = new Bon("Schets Apple Premium", 49, 124, "U vindt alle laatste Apple producten hier", 78, @"images\bon\85\thumb.jpg", multimedia, "straat", "huisnummer", 9260, "Schellebelle", BraLenBre);
                Bon bon86 = new Bon("Lab9 Aalst", 7, 340, "Officiele Apple reseller", 64, @"images\bon\86\thumb.jpg", multimedia, "straat", "huisnummer", 9260, "Schellebelle", BraLenBre);
                Bon bon87 = new Bon("De Conincks Screen Repair", 38, 536, "Een ongelukje is rap gebeurd", 75, @"images\bon\87\thumb.jpg", multimedia, "straat", "huisnummer", 9260, "Schellebelle", BraLenBre);
                Bon bon88 = new Bon("Medion Custom", 40, 316, "Medion laptop op maat gemaakt", 24, @"images\bon\88\thumb.jpg", multimedia, "straat", "huisnummer", 9260, "Schellebelle", BraLenBre);
                Bon bon89 = new Bon("Dell Dinosaur", 27, 311, "MS Dos specialist", 30, @"images\bon\89\thumb.jpg", multimedia, "straat", "huisnummer", 9260, "Schellebelle", BraLenBre);
                
                

                Bon bon91 = new Bon("Generieke cadeaubon", 1, 50, "Niet zeker welke bon u juist wilt, dan is deze generieke bon iets voor u!", 457, @"images\bon\9\thumb.jpg", generiek, "straat", "huisnummer", 9260, "Schellebelle", BraLenBre);


                var bonnen = new List<Bon>
                {
                   bon01, bon02, bon03, bon04, bon05, bon06, bon07, bon08, bon09, bon10, bon11, bon12, bon13, bon14, bon15, bon16, bon17, bon18, bon19, bon21, bon22, bon23, bon24, bon25, bon26, bon31, bon32, bon33, bon34, bon35, bon36, bon37, bon38, bon39, bon41, bon42, bon43, bon44, bon45, bon46, bon51, bon52, bon53, bon61, bon62, bon63, bon64, bon65, bon66, bon67, bon68, bon69, bon71, bon72, bon73, bon74, bon75, bon76, bon77, bon78, bon81, bon82, bon83, bon84, bon85, bon86, bon87, bon88, bon89, bon91
                };

                _dbContext.Bon.AddRange(bonnen);

                Persoon user01 = new Persoon("Brent", "Schets", Geslacht.Man, "brent@schets.com");
                Persoon user02 = new Persoon("Bram", "De Coninck", Geslacht.Man, "bram@deconinck.com");
                Persoon user03 = new Persoon("Lennert", "Bontinck", Geslacht.Man, "lennert@bontinck.com");

                var personen = new List<Persoon>
                {
                    user01, user02, user03
                };

                _dbContext.Persoon.AddRange(personen);



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
