using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Data.EFModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace SeminarskiRS1.Helper
{
    public class AutorizacijaAttribute : TypeFilterAttribute
    {
        public AutorizacijaAttribute(bool dozvoljenoKlijentu, bool dozvoljenoAdminu)
            : base(typeof(MyAuthorizeImpl))
        {
            Arguments = new object[] { dozvoljenoKlijentu, dozvoljenoAdminu };
        }
    }


    public class MyAuthorizeImpl : IActionFilter
    {
        public MyAuthorizeImpl(bool dozvoljenoKlijentu, bool dozvoljenoAdminu)
        {
            _dozvoljenoKlijentu = dozvoljenoKlijentu;
            _dozvoljenoAdminu = dozvoljenoAdminu;
        }
        private readonly bool _dozvoljenoKlijentu;
        private readonly bool _dozvoljenoAdminu;

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext httpContext = filterContext.HttpContext;

            Korisnik k = httpContext.LogiraniKorisnik();

            if (k == null)
            {
                if (filterContext.Controller is Controller controller)
                {
                    controller.TempData["PorukaError"] = "Niste logirani";
                }
                filterContext.Result = new RedirectResult("/Identity/Account/Login");
                return;
            }

            //KretanjePoSistemu.Save(httpContext);

            //klijenti mogu pristupiti 
            if (_dozvoljenoKlijentu && k.Klijent != null)
            {
                return;//ok - ima pravo pristupa
            }

            if (_dozvoljenoAdminu && k.Admin != null)
            {
                return;//ok - ima pravo pristupa
            }

            if (filterContext.Controller is Controller c1)
            {
                c1.ViewData["PorukaError"] = "Nemate pravo pristupa";
            }
            filterContext.Result = new RedirectResult("/");
        }
    }
}

