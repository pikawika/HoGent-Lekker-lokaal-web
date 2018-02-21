using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LekkerLokaal.Models.Domain
{
    interface Geregistreerd
    {
        string Wachtwoord { get; set; }
        string Afbeelding { get; set; }

        void MeldAan();
        void MeldAf();
        void VeranderWachtwoord();
        void VeranderEmailadres();
    }
}
