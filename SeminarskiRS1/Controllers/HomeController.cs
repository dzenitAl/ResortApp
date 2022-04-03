using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Data.EF;
using Data.EFModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SeminarskiRS1.Models;

namespace SeminarskiRS1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private UserManager<Korisnik> _userManager;
        private SignInManager<Korisnik> _signInManager;
        private MojDbContext mojDbContext;
        public HomeController(ILogger<HomeController> logger, UserManager<Korisnik> userManager, SignInManager<Korisnik> signInManager, MojDbContext _mojDbContext)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            mojDbContext = _mojDbContext;
        }


        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User); //logovani korisnik
            if(user != null)
            {
                return View();
            }
            else
            {
                return Redirect("/identity/account/login");
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
