using Data.EF;
using Data.EFModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace SeminarskiRS1.Helper
{
    public static class Autentifikacija
    {
        public static Korisnik LogiraniKorisnik(this HttpContext httpContext)
        {
            //Preuzimamo DbContext preko app services
            MojDbContext db = httpContext.RequestServices.GetService<MojDbContext>();

            //Preuzimamo userManager preko app services
            UserManager<Korisnik> userManager = httpContext.RequestServices.GetService<UserManager<Korisnik>>();

            if (httpContext.User == null)
                return null;

            //TrenutniKorisnikID
            string userId = userManager.GetUserId(httpContext.User);

            Korisnik k = db.Korisnik.Where(s => s.Id == userId)
                .Include(s => s.Admin)
                .Include(s => s.Klijent)
                .SingleOrDefault();

            return k;
        }
    }
}
