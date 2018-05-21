using System;
using LekkerLokaal.Models.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace LekkerLokaal.Models.AdminViewModels
{
    public class VerkochteCadeaubonBekijkenViewModel
    {
        [DataType(DataType.Text)]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Aanmaakdatum")]
        public string Aanmaakdatum { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Naam van de cadeaubon")]
        public string NaamCadeauBon { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Datum van gebruik")]
        public string GebruikDatum { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Geldigheid")]
        public string Status { get; set; }

        public string StatusClass { get; }

        [DataType(DataType.Text)]
        [Display(Name = "HandelaarNaam")]
        public string HandelaarNaam { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Prijs")]
        public decimal Prijs { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Naam van de zender")]
        public string NaamZender { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Naam van de ontvanger")]
        public string NaamOntvanger { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "E-mailadres ontvanger")]
        public string EmailOntvanger { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "E-mailadres zender")]
        public string EmailZender { get; set; }




        public VerkochteCadeaubonBekijkenViewModel(BestelLijn bon)
        {
            Id = bon.BestelLijnId ;
            HandelaarNaam = bon.Handelaar.Naam;
            NaamCadeauBon = bon.Bon.Naam;
            Prijs = bon.Prijs;
            Aanmaakdatum = bon.AanmaakDatum.ToString("dd/MM/yyyy");
            Status = bon.Geldigheid.ToString();
            GebruikDatum = bon.GebruikDatum.ToString("dd/MM/yyyy");
            NaamZender = bon.VerzenderNaam;
            NaamOntvanger = bon.OntvangerNaam;
            EmailOntvanger = bon.OntvangerEmail;
            EmailZender = bon.VerzenderEmail;

            switch (bon.Geldigheid)
            {
                case Geldigheid.Gebruikt:
                    StatusClass = "label-success";
                    break;
                case Geldigheid.Geldig:
                    StatusClass = "label-primary";
                    break;
                case Geldigheid.Verlopen:
                    StatusClass = "label-danger";
                    break;
                default:
                    StatusClass = "label-primary";
                    break;
            }

        }

        public VerkochteCadeaubonBekijkenViewModel()
        {

        }

    }
}
