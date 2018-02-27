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
                

                Handelaar Handelaar01 = new Handelaar("Restaurant Lekker", "lekker@gmail.com", "Met deze bon kan u lekker komen eten in ons restaurant genaamd Restaurant Lekker.", "BE 458 110 637", "password", @"images\handelaar\1\thumb.jpg", "straat", "huisnummer", 0101, "STAD");
                Handelaar Handelaar02 = new Handelaar("Bontinck", "bontinck@gmail.com", "Met deze bon kan u onze met passie gemaakte dessertjes komen proeven.", "BE 476 452 406", "password", @"images\handelaar\2\thumb.jpg", "straat", "huisnummer", 0101, "STAD");
                Handelaar Handelaar03 = new Handelaar("Schets", "schets@gmail.com", "Alle lokale bieren zijn hier te vinden! Er kan ook plaatselijk geproefd worden.", "BE 260 147 662", "password", @"images\handelaar\3\thumb.jpg", "straat", "huisnummer", 0101, "STAD");
                Handelaar Handelaar04 = new Handelaar("De Coninck's", "coninck@gmail.com", "De lekkerste cocktails zijn hier te vinden. Alleen hier te vinden tegen een goed prijs!", "BE 568 718 486", "password", @"images\handelaar\4\thumb.jpg", "straat", "huisnummer", 0101, "STAD");
                Handelaar Handelaar05 = new Handelaar("Wijnproeverij Handelaar01", "Handelaar01@gmail.com", "Met deze bon kan je bij wijnproeverij Handelaar01 genieten van een gezellige avond. Je zal er meer uitleg krijgen over de verschillende soorten wijnen en van elke soort mogen proeven, allen vergezeld met een passend hapje. Eens de sessie over is kan met de bon, wijn gekocht worden. Enkele merken die je hier kan verwachten zijn: Francis Ford Coppola, Franschhoek Cellar, Fushs Reinhardt, Gran Sasso, Grande Provence, Guadalupe, Guillamen I Muri, ..."
                    , "BE 305 678 557", "password", @"images\handelaar\5\thumb.jpg", "Lange Zoutstraat", "13", 9300, "Aalst");
                Handelaar Handelaar06 = new Handelaar("'t Sandwichke", "sandwich@gmail.com", "Voor al uw vegatarische noden kan u bij ons terecht.", "BE 360 874 067", "password", @"images\handelaar\6\thumb.jpg", "straat", "huisnummer", 0101, "STAD");
                Handelaar Handelaar07 = new Handelaar("McDonalds", "mc@gmail.com", "Voor een snelle hap moet u bij ons zijn!", "BE 686 045 577", "password", @"images\handelaar\7\thumb.jpg", "straat", "huisnummer", 0101, "STAD");
                Handelaar Handelaar08 = new Handelaar("SOS Piet", "sospiet@gmail.com", "Het echte restaurant van SOS Piet. Altijd de beste maatlijd voor een gezonde prijs!", "BE 035 212 186", "password", @"images\handelaar\8\thumb.jpg", "straat", "huisnummer", 0101, "STAD");
                Handelaar Handelaar09 = new Handelaar("CoBoSh", "cobosh@gmail.com", "Voor de beste wijnen moet je bij ons zijn! Hierbij kan altijd een hapje geserveerd worden.", "BE 570 261 327", "password", @"images\handelaar\9\thumb.jpg", "straat", "huisnummer", 0101, "STAD");
                

                Handelaar Handelaar11 = new Handelaar("Fitness Basic-Fit", "basicfit@gmail.com", "Bekenste fitness van België met vestigingen over het hele land.", "BE 652 760 204", "password", @"images\handelaar\10\thumb.jpg", "straat", "huisnummer", 0101, "STAD");

                Handelaar Handelaar21 = new Handelaar("Aalst", "aalst@gmail.com", "De recreatiedienst van Aalst staat in voor tal van speciale activiteiten.", "BE 656 564 542", "password", @"images\handelaar\11\thumb.jpg", "straat", "huisnummer", 0101, "STAD");
                Handelaar Handelaar22 = new Handelaar("Walibi", "walibi@gmail.com", "Een pretpark voor klein en groot.", "BE 557 481 167", "password", @"images\handelaar\12\thumb.jpg", "straat", "huisnummer", 0101, "STAD");
                Handelaar Handelaar23 = new Handelaar("NMBS", "trein@gmail.com", "De spoorwegdienst van België. Staakt liever dan te werken.", "BE 815 755 657", "password", @"images\handelaar\13\thumb.jpg", "straat", "huisnummer", 0101, "STAD");
                Handelaar Handelaar24 = new Handelaar("Disneyland Paris", "parijs@gmail.com", "Een van de grootste pretparken in Frankrijk.", "BE 802 726 432", "password", @"images\handelaar\14\thumb.jpg", "straat", "huisnummer", 0101, "STAD");
                Handelaar Handelaar25 = new Handelaar("Hamme", "hamme@gmail.com", "Stad Hamme", "BE 263 282 287", "password", @"images\handelaar\15\thumb.jpg", "straat", "huisnummer", 0101, "STAD");
                Handelaar Handelaar26 = new Handelaar("Breemdonk", "breemdonk@gmail.com", "Gemeente Breemdonk", "BE 703 431 007", "password", @"images\handelaar\16\thumb.jpg", "straat", "huisnummer", 0101, "STAD");

                Handelaar Handelaar31 = new Handelaar("Brenk", "brenk@gmail.com", "Stel zelf uw setje bloemen samen met deze bon.", "BE 680 614 508", "password", @"images\handelaar\17\thumb.jpg", "straat", "huisnummer", 0101, "STAD");
                Handelaar Handelaar32 = new Handelaar("De Mol", "molleken@gmail.com", "Heb je grond nodig voor in een pot", "BE 415 655 144", "password", @"images\handelaar\18\thumb.jpg", "straat", "huisnummer", 0101, "STAD");
                Handelaar Handelaar33 = new Handelaar("Schelfhout", "schelfhout@gmail.com", "Schelfhout, waar moet je andes zijn!", "BE 376 468 351", "password", @"images\handelaar\19\thumb.jpg", "straat", "huisnummer", 0101, "STAD");
                Handelaar Handelaar34 = new Handelaar("Liesje", "lies@gmail.com", "Lies, verkoopt ook wel een madelief", "BE 368 526 450", "password", @"images\handelaar\20\thumb.jpg", "straat", "huisnummer", 0101, "STAD");
                Handelaar Handelaar35 = new Handelaar("Funa Lima", "funa_lima@gmail.com", "Vissen, fonteinen, dieraccesoire...", "BE 146 730 153", "password", @"images\handelaar\21\thumb.jpg", "straat", "huisnummer", 0101, "STAD");
                Handelaar Handelaar36 = new Handelaar("Blub", "blub@gmail.com", "Blub, de winkel voor vis enthousiasten", "BE 227 103 604", "password", @"images\handelaar\22\thumb.jpg", "straat", "huisnummer", 0101, "STAD");
                Handelaar Handelaar37 = new Handelaar("GBontinck", "grasb@gmail.com", "Jaren ervaring in het snoeien van alle gazons", "BE 250 653 443", "password", @"images\handelaar\23\thumb.jpg", "straat", "huisnummer", 0101, "STAD");
                Handelaar Handelaar38 = new Handelaar("Aveve", "aveve@gmail.com", "Bij de boerenbond vind je altijd wat je zoekt", "BE 262 005 555", "password", @"images\handelaar\24\thumb.jpg", "straat", "huisnummer", 0101, "STAD");
                Handelaar Handelaar39 = new Handelaar("Groener Gras", "groengras@gmail.com", "Gazon voorzieningen voor iedereen die een groen gazon wil!", "BE 773 202 200", "password", @"images\handelaar\25\thumb.jpg", "straat", "huisnummer", 0101, "STAD");

                Handelaar Handelaar41 = new Handelaar("Pukkelpop", "ppk@gmail.com", "Tickets of coupons voor pukkelpok.", "BE 146 815 077", "password", @"images\handelaar\26\thumb.jpg", "straat", "huisnummer", 0101, "STAD");
                Handelaar Handelaar42 = new Handelaar("Bierfeesten", "bierfeesten@gmail.com", "De veste feesten in Lokeren: De Lokerse Bierfeesten!", "BE 146 815 077", "password", @"images\handelaar\26\thumb.jpg", "straat", "huisnummer", 0101, "STAD");
                Handelaar Handelaar43 = new Handelaar("Gentse Feesten", "feesten-gent@gmail.com", "Het grootste feest in Gent!", "BE 241 543 268", "password", @"images\handelaar\28\thumb.jpg", "straat", "huisnummer", 0101, "STAD");
                Handelaar Handelaar44 = new Handelaar("Gameforce", "games@gmail.com", "Grootste game beurs in België. Nu ook kortingsbonnen verkrijgbaar!", "BE 027 033 486", "password", @"images\handelaar\29\thumb.jpg", "straat", "huisnummer", 0101, "STAD");
                Handelaar Handelaar45 = new Handelaar("Garage Ferrari", "ferfer@gmail.com", "Beste cadeau voor een Ferrari liefhebber!", "BE 022 334 837", "password", @"images\handelaar\30\thumb.jpg", "straat", "huisnummer", 0101, "STAD");
                Handelaar Handelaar46 = new Handelaar("Facts", "facts@gmail.com", "Een van de grootste cosplay beurzen van België.", "BE 721 088 160", "password", @"images\handelaar\31\thumb.jpg", "straat", "huisnummer", 0101, "STAD");

                Handelaar Handelaar51 = new Handelaar("Restaurant Lekker", "lekker@gmail.com", "korte beschrijving", "BE 736 764 083", "password", @"images\handelaar\32\thumb.jpg", "straat", "huisnummer", 0101, "STAD");
                Handelaar Handelaar52 = new Handelaar("Restaurant Lekker", "lekker@gmail.com", "korte beschrijving", "BE 705 084 728", "password", @"images\handelaar\33\thumb.jpg", "straat", "huisnummer", 0101, "STAD");
                Handelaar Handelaar53 = new Handelaar("Restaurant Lekker", "lekker@gmail.com", "korte beschrijving", "BE 780 260 577", "password", @"images\handelaar\34\thumb.jpg", "straat", "huisnummer", 0101, "STAD");

                Handelaar Handelaar61 = new Handelaar("Restaurant Lekker", "lekker@gmail.com", "korte beschrijving", "BE 047 046 168", "password", @"images\handelaar\35\thumb.jpg", "straat", "huisnummer", 0101, "STAD");
                Handelaar Handelaar62 = new Handelaar("Restaurant Lekker", "lekker@gmail.com", "korte beschrijving", "BE 442 622 757", "password", @"images\handelaar\36\thumb.jpg", "straat", "huisnummer", 0101, "STAD");
                Handelaar Handelaar63 = new Handelaar("Restaurant Lekker", "lekker@gmail.com", "korte beschrijving", "BE 578 162 528", "password", @"images\handelaar\37\thumb.jpg", "straat", "huisnummer", 0101, "STAD");
                Handelaar Handelaar64 = new Handelaar("Restaurant Lekker", "lekker@gmail.com", "korte beschrijving", "BE 365 006 747", "password", @"images\handelaar\38\thumb.jpg", "straat", "huisnummer", 0101, "STAD");
                Handelaar Handelaar65 = new Handelaar("Restaurant Lekker", "lekker@gmail.com", "korte beschrijving", "BE 063 225 184", "password", @"images\handelaar\39\thumb.jpg", "straat", "huisnummer", 0101, "STAD");
                Handelaar Handelaar66 = new Handelaar("Restaurant Lekker", "lekker@gmail.com", "korte beschrijving", "BE 206 664 777", "password", @"images\handelaar\40\thumb.jpg", "straat", "huisnummer", 0101, "STAD");
                Handelaar Handelaar67 = new Handelaar("Restaurant Lekker", "lekker@gmail.com", "korte beschrijving", "BE 140 378 850", "password", @"images\handelaar\41\thumb.jpg", "straat", "huisnummer", 0101, "STAD");
                Handelaar Handelaar68 = new Handelaar("Restaurant Lekker", "lekker@gmail.com", "korte beschrijving", "BE 466 753 428", "password", @"images\handelaar\42\thumb.jpg", "straat", "huisnummer", 0101, "STAD");
                Handelaar Handelaar69 = new Handelaar("Restaurant Lekker", "lekker@gmail.com", "korte beschrijving", "BE 171 663 118", "password", @"images\handelaar\43\thumb.jpg", "straat", "huisnummer", 0101, "STAD");

                Handelaar Handelaar71 = new Handelaar("Restaurant Lekker", "lekker@gmail.com", "korte beschrijving", "BE 081 135 314", "password", @"images\handelaar\44\thumb.jpg", "straat", "huisnummer", 0101, "STAD");
                Handelaar Handelaar72 = new Handelaar("Restaurant Lekker", "lekker@gmail.com", "korte beschrijving", "BE 266 553 200", "password", @"images\handelaar\45\thumb.jpg", "straat", "huisnummer", 0101, "STAD");
                Handelaar Handelaar73 = new Handelaar("Restaurant Lekker", "lekker@gmail.com", "korte beschrijving", "BE 446 384 070", "password", @"images\handelaar\46\thumb.jpg", "straat", "huisnummer", 0101, "STAD");
                Handelaar Handelaar74 = new Handelaar("Restaurant Lekker", "lekker@gmail.com", "korte beschrijving", "BE 313 402 666", "password", @"images\handelaar\47\thumb.jpg", "straat", "huisnummer", 0101, "STAD");
                Handelaar Handelaar75 = new Handelaar("Restaurant Lekker", "lekker@gmail.com", "korte beschrijving", "BE 402 156 543", "password", @"images\handelaar\48\thumb.jpg", "straat", "huisnummer", 0101, "STAD");
                Handelaar Handelaar76 = new Handelaar("Restaurant Lekker", "lekker@gmail.com", "korte beschrijving", "BE 521 001 106", "password", @"images\handelaar\49\thumb.jpg", "straat", "huisnummer", 0101, "STAD");
                Handelaar Handelaar77 = new Handelaar("Restaurant Lekker", "lekker@gmail.com", "korte beschrijving", "BE 521 001 103", "password", @"images\handelaar\49\thumb.jpg", "straat", "huisnummer", 0101, "STAD");
                Handelaar Handelaar78 = new Handelaar("Restaurant Lekker", "lekker@gmail.com", "korte beschrijving", "BE 214 232 134", "password", @"images\handelaar\50\thumb.jpg", "straat", "huisnummer", 0101, "STAD");

                Handelaar Handelaar81 = new Handelaar("Restaurant Lekker", "lekker@gmail.com", "korte beschrijving", "BE 588 137 284", "password", @"images\handelaar\51\thumb.jpg", "straat", "huisnummer", 0101, "STAD");
                Handelaar Handelaar82 = new Handelaar("Restaurant Lekker", "lekker@gmail.com", "korte beschrijving", "BE 812 573 731", "password", @"images\handelaar\52\thumb.jpg", "straat", "huisnummer", 0101, "STAD");
                Handelaar Handelaar83 = new Handelaar("Restaurant Lekker", "lekker@gmail.com", "korte beschrijving", "BE 253 500 301", "password", @"images\handelaar\53\thumb.jpg", "straat", "huisnummer", 0101, "STAD");
                Handelaar Handelaar84 = new Handelaar("Restaurant Lekker", "lekker@gmail.com", "korte beschrijving", "BE 561 032 078", "password", @"images\handelaar\54\thumb.jpg", "straat", "huisnummer", 0101, "STAD");
                Handelaar Handelaar85 = new Handelaar("Restaurant Lekker", "lekker@gmail.com", "korte beschrijving", "BE 786 056 478", "password", @"images\handelaar\55\thumb.jpg", "straat", "huisnummer", 0101, "STAD");
                Handelaar Handelaar86 = new Handelaar("Restaurant Lekker", "lekker@gmail.com", "korte beschrijving", "BE 075 881 157", "password", @"images\handelaar\56\thumb.jpg", "straat", "huisnummer", 0101, "STAD");
                Handelaar Handelaar87 = new Handelaar("Restaurant Lekker", "lekker@gmail.com", "korte beschrijving", "BE 068 276 242", "password", @"images\handelaar\57\thumb.jpg", "straat", "huisnummer", 0101, "STAD");
                Handelaar Handelaar88 = new Handelaar("Restaurant Lekker", "lekker@gmail.com", "korte beschrijving", "BE 116 728 241", "password", @"images\handelaar\58\thumb.jpg", "straat", "huisnummer", 0101, "STAD");
                Handelaar Handelaar89 = new Handelaar("Restaurant Lekker", "lekker@gmail.com", "korte beschrijving", "BE 774 855 608", "password", @"images\handelaar\59\thumb.jpg", "straat", "huisnummer", 0101, "STAD");


                Handelaar Handelaar91 = new Handelaar("Generiek", "generiek@gmail.com", "generiek", "BE 774 123 518", "password", @"images\handelaar\59\thumb.jpg", "straat", "huisnummer", 0101, "STAD");

                Handelaar Handelaar10 = new Handelaar("ChaCha", "chacha@gmail.com", "Voor de beste wijnen moet je bij ons zijn! Hierbij kan altijd een hapje geserveerd worden.", "BE 570 261 847", "password", @"images\handelaar\60\thumb.jpg", "straat", "huisnummer", 0101, "STAD");

                var handelaars = new List<Handelaar>
                {
                    Handelaar01, Handelaar02, Handelaar03, Handelaar04, Handelaar05, Handelaar06, Handelaar07, Handelaar08, Handelaar09, Handelaar10, Handelaar11, Handelaar21, Handelaar22, Handelaar23, Handelaar24, Handelaar25, Handelaar26, Handelaar31, Handelaar32, Handelaar33, Handelaar34, Handelaar35, Handelaar36, Handelaar37, Handelaar38, Handelaar39, Handelaar41, Handelaar42, Handelaar43, Handelaar44, Handelaar45, Handelaar46, Handelaar51, Handelaar52, Handelaar53, Handelaar61, Handelaar62, Handelaar63, Handelaar64, Handelaar65, Handelaar66, Handelaar67, Handelaar68, Handelaar69, Handelaar71, Handelaar72, Handelaar73, Handelaar74, Handelaar75, Handelaar76, Handelaar77, Handelaar78, Handelaar81, Handelaar82, Handelaar83, Handelaar84, Handelaar85, Handelaar86, Handelaar87, Handelaar88, Handelaar89, Handelaar91
                };

                _dbContext.Handelaar.AddRange(handelaars);


                Bon bon01 = new Bon("Restaurant lekker", 1, 50, "3 sterren resaurant in het centrum van Aalst.", 17, @"images\bon\1\thumb.jpg", eten_drinken, "Arbeidstraat", "14", 9300, "Aalst", Handelaar01);
                Bon bon02 = new Bon("Dessertbar chez Bontinck", 1, 30, "Met passie gemaakte dessertjes in het mooie Schellebelle.", 242, @"images\bon\2\thumb.jpg", eten_drinken, "Paepestraat", "178", 9260, "Wichelen", Handelaar02);
                Bon bon03 = new Bon("Bierspecialist Schets", 1, 20, "Meer dan 70 Belgische bieren in een gezellige kroeg.", 42, @"images\bon\3\thumb.jpg", eten_drinken, "Ravensteinstraat", "50", 1000, "Brussel", Handelaar03);
                Bon bon04 = new Bon("De Coninck's cocktail", 1, 15, "Een VIP cocktailbar met live optredens van lokale muzikanten.", 24, @"images\bon\4\thumb.jpg", eten_drinken, "Sint-Pietersnieuwstraat", "124", 9000, "Gent", Handelaar04);
                Bon bon05 = new Bon("Wijnproeverij Handelaar01", 1, 75, "Keuze uit verschillende wijnen vergezeld met een hapje.", 41, @"images\bon\5\thumb.jpg", eten_drinken, "Arbeidstraat", "14", 9300, "Aalst", Handelaar05);
                Bon bon06 = new Bon("Veggiebar 't Sandwichke", 1, 30, "Het bewijs dat vegetarisch eten lekker kan zijn.", 45, @"images\bon\6\thumb.jpg", eten_drinken, "Arbeidstraat", "14", 9300, "Aalst", Handelaar06);
                Bon bon07 = new Bon("Fastfood McDonalds", 1, 5, "De keten met keuzes voor iedereen.", 246, @"images\bon\7\thumb.jpg", eten_drinken, "Arbeidstraat", "14", 9300, "Aalst", Handelaar07);
                Bon bon08 = new Bon("Restaurant SOS Piet", 1, 150, "5 sterren restaurant met de enige echte SOS Piet als kok.", 21, @"images\bon\8\thumb.jpg", eten_drinken, "Paepestraat", "178", 9260, "Wichelen", Handelaar08);
                Bon bon09 = new Bon("Wijnproeverij CoBoSh", 1, 75, "Keuze uit verschillende wijnen vergezeld met een hapje.", 47, @"images\bon\9\thumb.jpg", eten_drinken, "Arbeidstraat", "14", 9300, "Aalst", Handelaar09);
                Bon bon10 = new Bon("Wijnproeverij chacha", 1, 75, "Hapje drankje favoriet muziekje.", 22, @"images\bon\10\thumb.jpg", eten_drinken, "Ravensteinstraat", "50", 1000, "Brussel", Handelaar10);


                Bon bon11 = new Bon("Sanitas Wichelen", 1, 30, "Ideale fitness voor oud en jong", 83, @"images\bon\11\thumb.jpg", fitness, "Paepestraat", "178", 9260, "Wichelen", Handelaar11);
                Bon bon12 = new Bon("Fitness Basic-Fit Aalst", 1, 30, "Bekenste fitness van België met vestigingen over het hele land.", 75, @"images\bon\12\thumb.jpg", fitness, "Arbeidstraat", "14", 9300, "Aalst", Handelaar11);
                Bon bon13 = new Bon("Fitness Basic-Fit Gent", 1, 30, "Bekenste fitness van België met vestigingen over het hele land.", 8, @"images\bon\13\thumb.jpg", fitness, "Sint-Pietersnieuwstraat", "124", 9000, "Gent", Handelaar11);
                Bon bon14 = new Bon("Fitness Basic-Fit Brussel", 1, 30, "Bekenste fitness van België met vestigingen over het hele land.", 53, @"images\bon\14\thumb.jpg", fitness, "Ravensteinstraat", "50", 1000, "Brussel", Handelaar11);
                Bon bon15 = new Bon("Fitness Basic-Fit Brugge", 1, 30, "Bekenste fitness van België met vestigingen over het hele land.", 53, @"images\bon\15\thumb.jpg", fitness, "Maalse Steenweg", "50", 8310, "Brugge", Handelaar11);
                Bon bon16 = new Bon("Fitness Basic-Fit Sint-Truiden", 1, 30, "Bekenste fitness van België met vestigingen over het hele land.", 72, @"images\bon\16\thumb.jpg", fitness, "Luikersteenweg ", "40", 3800, "Sint-Truiden", Handelaar11);
                Bon bon17 = new Bon("Fitness Basic-Fit Wetteren", 1, 30, "Bekenste fitness van België met vestigingen over het hele land.", 65, @"images\bon\17\thumb.jpg", fitness, "Cooppallaan ", "40", 9230, "Wetteren", Handelaar11);
                Bon bon18 = new Bon("Fitness Basic-Fit Wichelen", 1, 30, "Bekenste fitness van België met vestigingen over het hele land.", 22, @"images\bon\18\thumb.jpg", fitness, "Paepestraat", "178", 9260, "Wichelen", Handelaar11);
                Bon bon19 = new Bon("Fitness Basic-Fit Lede", 1, 30, "Bekenste fitness van België met vestigingen over het hele land.", 75, @"images\bon\19\thumb.jpg", fitness, "Kasteeldreef", "15", 9340, "Lede", Handelaar11);


                Bon bon21 = new Bon("Nachtwandeling Aalst at night", 1, 10, "Geniet van de sterrenhemel in de mooie streken van Aalst (met gids).", 63, @"images\bon\11\thumb.jpg", uitstappen, "Arbeidstraat", "14", 9300, "Aalst", Handelaar01);
                Bon bon22 = new Bon("Dagje wallibi in Dendermonde", 25, 142, "Wat is er nu leuker dan een dagje wallibi met vrienden", 34, @"images\bon\22\thumb.jpg", uitstappen, "Mechelsesteenweg ", "138", 9200, "Dendermonde", Handelaar01);
                Bon bon23 = new Bon("Met de trein naar Oostende", 36, 159, "Spring zong er al over dus wat houd je tegen het te doen", 45, @"images\bon\23\thumb.jpg", uitstappen, "Torhoutsesteenweg", "611", 8400, "Oostende", Handelaar01);
                Bon bon24 = new Bon("Weekendje disneyland parijs", 29, 251, "Disneyland de bestemming voor groot en klein", 35, @"images\bon\24\thumb.jpg", uitstappen, "Leopoldlaan", "1", 1930, "Zaventem", Handelaar01);
                Bon bon25 = new Bon("De grotten van Han", 50, 264, "Het liedje zit ongetwijfeld al in je hoofd dus ga nu gewoon", 86, @"images\bon\25\thumb.jpg", uitstappen, "Rue Joseph Lamotte", "2", 5580, "Han-sur-Lesse", Handelaar01);
                Bon bon26 = new Bon("Historisch bezoek Breemdonk", 6, 185, "Voor de oorlog fanaten een must", 35, @"images\bon\26\thumb.jpg", uitstappen, "Brandstraat", "57", 2830, "Willebroek", Handelaar01);


                Bon bon31 = new Bon("Bloemencenter Brenk", 1, 35, "Stel zelf uw setje bloemen samen met deze bon.", 43, @"images\bon\12\thumb.jpg", huis_tuin, "Arbeidstraat", "14", 9300, "Aalst", Handelaar01);
                Bon bon32 = new Bon("Potgrond De Mol in Lede", 22, 233, "Heb je grond nodig voor in een pot", 68, @"images\bon\32\thumb.jpg", huis_tuin, "Kasteeldreef", "15", 9340, "Lede", Handelaar01);
                Bon bon33 = new Bon("Schelfhout Ten Aalst", 36, 345, "Schelfhout, waar moet je andes zijn!", 75, @"images\bon\33\thumb.jpg", huis_tuin, "Arbeidstraat", "14", 9300, "Aalst", Handelaar01);
                Bon bon34 = new Bon("Bloemetje liesje in Gent", 13, 468, "Lies, verkoopt ook wel een madelief ", 25, @"images\bon\34\thumb.jpg", huis_tuin, "Sint-Pietersnieuwstraat", "124", 9000, "Gent", Handelaar01);
                Bon bon35 = new Bon("Funa Lima tuincentrum Lede", 26, 232, "Vissen, fonteinen, dieraccesoire...", 14, @"images\bon\35\thumb.jpg", huis_tuin, "Kasteeldreef", "15", 9340, "Lede", Handelaar01);
                Bon bon36 = new Bon("Vijvervoorziening Blub", 30, 404, "Blub, de winkel voor vis enthousiasten", 35, @"images\bon\36\thumb.jpg", huis_tuin, "Arbeidstraat", "14", 9300, "Aalst", Handelaar01);
                Bon bon37 = new Bon("Grasmaaiers Bontinck", 25, 89, "Jaren ervaring in het snoeien", 76, @"images\bon\37\thumb.jpg", huis_tuin, "Paepestraat", "178", 9260, "Wichelen", Handelaar01);
                Bon bon38 = new Bon("Aveve boerenbond te Aalst", 31, 416, "Bij de boerenbond vind je altijd wat je zoekt", 75, @"images\bon\38\thumb.jpg", huis_tuin, "Arbeidstraat", "14", 9300, "Aalst", Handelaar01);
                Bon bon39 = new Bon("Groener Gras In Wetteren", 12, 526, "Gazon voorzieningen", 14, @"images\bon\39\thumb.jpg", huis_tuin, "Cooppallaan ", "40", 9230, "Wetteren", Handelaar01);

                Bon bon41 = new Bon("Pukkelpop weekend tickets", 21, 513, "Pukkelpop, dat moet je gedaan hebben", 57, @"images\bon\41\thumb.jpg", events, "Paepestraat", "178", 9260, "Wichelen", Handelaar01);
                Bon bon42 = new Bon("Lokerse bierfeesten", 44, 393, "Bierfanaten kunnen dit niet missen", 75, @"images\bon\42\thumb.jpg", events, "Kleine Dam", "1", 9160, "Lokeren", Handelaar01);
                Bon bon43 = new Bon("Gentse feesten eetfestijn", 42, 464, "Drinken en eten, meer moet dat niet zijn", 25, @"images\bon\43\thumb.jpg", events, "Sint-Pietersnieuwstraat", "124", 9000, "Gent", Handelaar01);
                Bon bon44 = new Bon("Gameforce in de Nekkerhalle", 38, 179, "Voor elke nerd wat wils", 14, @"images\bon\44\thumb.jpg", events, "Ravensteinstraat", "50", 1000, "Brussel", Handelaar01);
                Bon bon45 = new Bon("Drive A Ferrari Day", 8, 318, "Ideal geshenk voor een autofanaat", 38, @"images\bon\45\thumb.jpg", events, "Paepestraat", "178", 9260, "Wichelen", Handelaar01);
                Bon bon46 = new Bon("Facts: trein en eten", 34, 197, "Cosplay, eten en vervoer", 18, @"images\bon\46\thumb.jpg", events, "Sint-Pietersnieuwstraat", "124", 9000, "Gent", Handelaar01);

                Bon bon51 = new Bon("Makeup pallete Nude", 29, 374, "Het bekendste merk zijn palette", 67, @"images\bon\51\thumb.jpg", beauty, "Paepestraat", "178", 9260, "Wichelen", Handelaar01);
                Bon bon52 = new Bon("Ici Paris verwenbon", 15, 500, "Een parfum kan je nooit mee misdoen", 17, @"images\bon\52\thumb.jpg", beauty, "Arbeidstraat", "14", 9300, "Aalst", Handelaar01);
                Bon bon53 = new Bon("Lipstick Lover Aalst", 8, 404, "Voor de lippen lovers", 86, @"images\bon\53\thumb.jpg", beauty, "Arbeidstraat", "14", 9300, "Aalst", Handelaar01);

                Bon bon61 = new Bon("Sofa en Co", 18, 389, "Relax in een sofa van sofa en co", 71, @"images\bon\61\thumb.jpg", interieur, "Arbeidstraat", "14", 9300, "Aalst", Handelaar01);
                Bon bon62 = new Bon("Deba meubelen", 36, 202, "Verkoopt al uw interieur", 37, @"images\bon\62\thumb.jpg", interieur, "Arbeidstraat", "14", 9300, "Aalst", Handelaar01);
                Bon bon63 = new Bon("Ikea huisvoorzieningeb", 40, 375, "Meubelspiaclist sinds 74", 71, @"images\bon\63\thumb.jpg", interieur, "Paepestraat", "178", 9260, "Wichelen", Handelaar01);
                Bon bon64 = new Bon("Leenbakker", 34, 335, "Om te kopen, niet te lenen", 37, @"images\bon\64\thumb.jpg", interieur, "Sint-Pietersnieuwstraat", "124", 9000, "Gent", Handelaar01);
                Bon bon65 = new Bon("Salon Ballon Gent", 13, 352, "Sallon Ballon is een speciaalzaak te Gent", 76, @"images\bon\65\thumb.jpg", interieur, "Sint-Pietersnieuwstraat", "124", 9000, "Gent", Handelaar01);
                Bon bon66 = new Bon("Keukens Donald", 45, 244, "Al 8 jaar maak ik keukens alsof ze voor mezelf zijn", 46, @"images\bon\66\thumb.jpg", interieur, "Arbeidstraat", "14", 9300, "Aalst", Handelaar01);
                Bon bon67 = new Bon("Modern Gent", 31, 510, "Modern interieur hoeft niet duur te zijn", 75, @"images\bon\67\thumb.jpg", interieur, "Sint-Pietersnieuwstraat", "124", 9000, "Gent", Handelaar01);
                Bon bon68 = new Bon("Kunst & Kitch", 18, 186, "Een beetje kunst, een beetje kitch", 46, @"images\bon\68\thumb.jpg", interieur, "Paepestraat", "178", 9260, "Wichelen", Handelaar01);
                Bon bon69 = new Bon("Moderne interieur Gill", 5, 60, "Op maat gemaakt interieur tegen een zacht prijsje", 45, @"images\bon\69\thumb.jpg", interieur, "Arbeidstraat", "14", 9300, "Aalst", Handelaar01);

                Bon bon71 = new Bon("C&A Aalst", 24, 480, "De kledingwinkel in Aalst en omstreken", 42, @"images\bon\71\thumb.jpg", kledij, "Arbeidstraat", "14", 9300, "Aalst", Handelaar01);
                Bon bon72 = new Bon("AS Advantures", 48, 373, "Kledie om een avontuur mee aan te gaan", 47, @"images\bon\72\thumb.jpg", kledij, "Paepestraat", "178", 9260, "Wichelen", Handelaar01);
                Bon bon73 = new Bon("Ultra Wet", 41, 413, "De kledingspecialist voor droog en nat", 71, @"images\bon\73\thumb.jpg", kledij, "Arbeidstraat", "14", 9300, "Aalst", Handelaar01);
                Bon bon74 = new Bon("Holiday", 24, 489, "Voor al uw feestkledij", 17, @"images\bon\74\thumb.jpg", kledij, "Sint-Pietersnieuwstraat", "124", 9000, "Gent", Handelaar01);
                Bon bon75 = new Bon("Bram's Fashion", 23, 86, "Voor ieder wat wilds", 73, @"images\bon\75\thumb.jpg", kledij, "Paepestraat", "178", 9260, "Wichelen", Handelaar01);
                Bon bon76 = new Bon("Bontinck's Panthers", 30, 453, "Pants from Bontinck are dreams for legs", 72, @"images\bon\76\thumb.jpg", kledij, "Arbeidstraat", "14", 9300, "Aalst", Handelaar01);
                Bon bon77 = new Bon("Bre Bra", 29, 478, "Van A tot Z, u vindt het bij ons", 92, @"images\bon\77\thumb.jpg", kledij, "Sint-Pietersnieuwstraat", "124", 9000, "Gent", Handelaar01);
                Bon bon78 = new Bon("Pikantje", 34, 402, "Erotische kledingwinkel", 9, @"images\bon\78\thumb.jpg", kledij, "Arbeidstraat", "14", 9300, "Aalst", Handelaar01);

                Bon bon81 = new Bon("Fnac Aalst", 3, 377, "De multimedia specialist in Europa", 54, @"images\bon\81\thumb.jpg", multimedia, "Arbeidstraat", "14", 9300, "Aalst", Handelaar01);
                Bon bon82 = new Bon("Mediamarkt Dendermonde", 46, 433, "Electronica tegen een spot prijs", 45, @"images\bon\82\thumb.jpg", multimedia, "Mechelsesteenweg", "138", 9200, "Dendermonde", Handelaar01);
                Bon bon83 = new Bon("Van Den Borre Gent", 31, 181, "Koffiezets voor 12€", 15, @"images\bon\83\thumb.jpg", multimedia, "Sint-Pietersnieuwstraat", "124", 9000, "Gent", Handelaar01);
                Bon bon84 = new Bon("Bontinck IT brugge", 36, 539, "Een probleempje groot of klein, dan moet je bij IT Lennert zijn", 67, @"images\bon\84\thumb.jpg", multimedia, "Maalse Steenweg", "50", 8310, "Brugge", Handelaar01);
                Bon bon85 = new Bon("Schets Apple Premium", 49, 124, "U vindt alle laatste Apple producten hier", 78, @"images\bon\85\thumb.jpg", multimedia, "Paepestraat", "178", 9260, "Wichelen", Handelaar01);
                Bon bon86 = new Bon("Lab9 Aalst", 7, 340, "Officiele Apple reseller", 64, @"images\bon\86\thumb.jpg", multimedia, "Arbeidstraat", "14", 9300, "Aalst", Handelaar01);
                Bon bon87 = new Bon("De Conincks Screen Repair", 38, 536, "Een ongelukje is rap gebeurd", 75, @"images\bon\87\thumb.jpg", multimedia, "Paepestraat", "178", 9260, "Wichelen", Handelaar01);
                Bon bon88 = new Bon("Medion Custom", 40, 316, "Medion laptop op maat gemaakt", 24, @"images\bon\88\thumb.jpg", multimedia, "Sint-Pietersnieuwstraat", "124", 9000, "Gent", Handelaar01);
                Bon bon89 = new Bon("Dell Dinosaur", 27, 311, "MS Dos specialist", 30, @"images\bon\89\thumb.jpg", multimedia, "Paepestraat", "178", 9260, "Wichelen", Handelaar01);
                
                

                Bon bon91 = new Bon("Generieke cadeaubon", 1, 50, "Niet zeker welke bon u juist wilt, dan is deze generieke bon iets voor u!", 457, @"images\bon\9\thumb.jpg", generiek, "Arbeidstraat", "14", 9300, "Aalst", Handelaar01);


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
