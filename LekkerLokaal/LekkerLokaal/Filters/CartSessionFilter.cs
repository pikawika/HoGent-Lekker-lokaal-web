using LekkerLokaal.Models.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LekkerLokaal.Filters
{
    [AttributeUsageAttribute(AttributeTargets.All, AllowMultiple = false)]
    public class CartSessionFilter : ActionFilterAttribute
    {
        private readonly IBonRepository _bonRepository;
        private Winkelwagen _winkelwagen;

        public CartSessionFilter(IBonRepository bonRepository)
        {
            _bonRepository = bonRepository;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _winkelwagen = ReadCartFromSession(context.HttpContext);
            context.ActionArguments["winkelwagen"] = _winkelwagen;
            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            WriteCartToSession(_winkelwagen, context.HttpContext);
            base.OnActionExecuted(context);
        }

        private Winkelwagen ReadCartFromSession(HttpContext context)
        {
            Winkelwagen winkelwagen = context.Session.GetString("winkelwagen") == null ?
                new Winkelwagen() : JsonConvert.DeserializeObject<Winkelwagen>(context.Session.GetString("winkelwagen"));
            foreach (var l in winkelwagen.WinkelwagenLijnen)
                l.Bon = _bonRepository.GetByBonId(l.Bon.BonId);
            return winkelwagen;
        }

        private void WriteCartToSession(Winkelwagen winkelwagen, HttpContext context)
        {
            context.Session.SetString("winkelwagen", JsonConvert.SerializeObject(winkelwagen));
        }
    }
}
