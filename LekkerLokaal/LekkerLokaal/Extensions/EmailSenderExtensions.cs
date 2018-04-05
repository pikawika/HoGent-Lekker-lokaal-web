using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using LekkerLokaal.Services;

namespace LekkerLokaal.Services
{
    public static class EmailSenderExtensions
    {
        public static Task SendEmailConfirmationAsync(this IEmailSender emailSender, string email, string link, string voornaam)
        {
            return emailSender.SendEmailAsync(email, "Bevestig uw e-mailadres",
                $"<p>Hallo {voornaam}!</p>" +
                $"<p>Bedankt voor uw registratie bij Lekker Lokaal.</p>" +
                $"<p>Gelieve <a href='{HtmlEncoder.Default.Encode(link)}'>hier te klikken</a> om uw e-mailadres te bevestigen zodat uw account aangemaakt kan worden.</p>" +
                $"<p>Lukt dit niet? Druk dan op deze link: {HtmlEncoder.Default.Encode(link)} </p>" + 
                $"<p>Indien u niet degene bent die een account wou aanmaken bij Lekker Lokaal, mag u deze e-mail negeren. </p>" +
                $"<p>Met vriendelijke groeten, </p>" +
                $"<p>Het Lekker Lokaal team </p>");
        }
    }
}
