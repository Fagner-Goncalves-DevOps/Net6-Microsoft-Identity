using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NetIdentityModel.Models;
using NetIdentityModel.Models.AccountDto;
using System.Diagnostics;

namespace NetIdentityModel.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;  //sem uso no momento
        private readonly ILogger<HomeController> _logger;

        public AccountController(
                                ILogger<HomeController> logger,
                                UserManager<ApplicationUser> userManager,
                                SignInManager<ApplicationUser> signInManager,
                                RoleManager<IdentityRole> roleManager //sem uso no momento
                                ) : base()
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager; //sem uso no momento
            _logger = logger;
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterDto model)
        {
            return View();
        }

    }
}