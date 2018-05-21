using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LekkerLokaal.Models;
using LekkerLokaal.Models.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LekkerLokaal.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class MobieleAppController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IHandelaarRepository _handelaarRepository;
        private readonly IBestellijnRepository _bestellijnRepository;
        private readonly IBonRepository _bonRepository;

        public MobieleAppController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IHandelaarRepository handelaarRepository,
            IBestellijnRepository bestellijnRepository,
            IBonRepository bonRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _handelaarRepository = handelaarRepository;
            _bestellijnRepository = bestellijnRepository;
            _bonRepository = bonRepository;
        }

        //GET voor aanmelden van een handelaar
        [HttpGet("{id}/{ww}", Name = "MeldHandelaarAan")]
        public async Task<Object> MeldHandelaarAan(string id, string ww)
        {
            if (_handelaarRepository.GetByEmail(id) != null)
            {
                var handelaar = _handelaarRepository.GetByEmail(id);
                var user = await _userManager.FindByEmailAsync(handelaar.Emailadres);

                if (await _userManager.CheckPasswordAsync(user, ww))
                {
                    return new
                    {
                        handelaar.HandelaarId,
                        handelaar.BTW_Nummer,
                        handelaar.Beschrijving,
                        handelaar.Emailadres,
                        handelaar.Naam
                    };
                }
            }
            return null;
        }

        //GET voor cadeaubon
        [HttpGet("{id}", Name = "HaalCadeaubonOp")]
        public Object HaalCadeaubonOp(string id)
        {
            if (_bestellijnRepository.GetBy(id) != null)
            {
                var bestellijn = _bestellijnRepository.GetBy(id);
                if (bestellijn != null)
                {
                    var bon = _bonRepository.GetByBonId(bestellijn.Bon.BonId);
                    var handelaar = _handelaarRepository.GetByHandelaarId(bon.Handelaar.HandelaarId);
                    return new
                    {
                        bestellijn.BestelLijnId,
                        bon.Naam,
                        bestellijn.Prijs,
                        bestellijn.AanmaakDatum,
                        handelaar.HandelaarId,
                        handelaar.Emailadres,
                        bestellijn.Geldigheid
                    };
                }
            }
            return null;
        }

        //PUT voor cadeaubon
        [HttpPut("{id}", Name = "WerkCadeaubonBij")]
        public void WerkCadeaubonBij(int id, [FromBody] CadeaubonModel model)
        {
            if (_bestellijnRepository.GetById(id) != null)
            {
                var bestellijn = _bestellijnRepository.GetById(id);
                if (bestellijn != null)
                {
                    bestellijn.Handelaar = _handelaarRepository.GetByHandelaarId(model.HandelaarId);
                    bestellijn.Geldigheid = model.bepaalGeldigheid();
                    if (bestellijn.Geldigheid == Geldigheid.Gebruikt)
                        bestellijn.GebruikDatum = DateTime.Today;
                    _bestellijnRepository.SaveChanges();
                }
            }
        }

        //Model voor de PUT
        public class CadeaubonModel
        {
            public int HandelaarId { get; set; }
            public int Geldigheid { get; set; }

            public Geldigheid bepaalGeldigheid()
            {
                switch (Geldigheid)
                {
                    case 0:
                        return Models.Domain.Geldigheid.Geldig;
                    case 1:
                        return Models.Domain.Geldigheid.Ongeldig;
                    case 2:
                        return Models.Domain.Geldigheid.Verlopen;
                    case 3:
                        return Models.Domain.Geldigheid.Gebruikt;
                }

                return Models.Domain.Geldigheid.Ongeldig;
            }
        }

    }
}
