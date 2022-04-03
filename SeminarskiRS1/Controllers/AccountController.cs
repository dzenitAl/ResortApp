using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Data.EFModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Data.EF;

namespace SeminarskiRS1.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {

        private  UserManager<Korisnik> _userManager;
        private readonly SignInManager<Korisnik> _signInManager;
        private MojDbContext dbContext;

        public AccountController(UserManager<Korisnik> userManager, SignInManager<Korisnik> signInManager, MojDbContext _dbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            dbContext = _dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }


    }
}
