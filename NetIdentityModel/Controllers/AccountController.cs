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
        private readonly RoleManager<IdentityRole> _roleManager;  
        private readonly ILogger<HomeController> _logger; //responsável	por	registrar mensagens de Log e exibi-las.

        public AccountController(
                                UserManager<ApplicationUser> userManager,
                                SignInManager<ApplicationUser> signInManager,
                                RoleManager<IdentityRole> roleManager,
                                ILogger<HomeController> logger
                                ) : base()
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _logger = logger;
        }


        //Account/Login
        [HttpGet]
        public IActionResult Login() 
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto model) 
        {
            if (ModelState.IsValid) 
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded) 
                {
                    return RedirectToAction("index", "home");
                }
                ModelState.AddModelError(string.Empty, "Login Inválido");
            }
            return View(model);
        }


        //Account/Register
        [HttpGet]
        public IActionResult Register()
        {
            //add processo para já pegar user viewdata
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto model)
        {      
            if (ModelState.IsValid) 
            {
                var user = new ApplicationUser { UserExtended = model.UserExtended, Cpf = model.Cpf, UserName = model.Email, Email = model.Email};
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded) 
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("index", "home");
                }
                foreach (var error in result.Errors) //nao mostra o erro para usuario ver sobre isso /melhorar separar tambem
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        //Account/Logout
        [HttpGet]
        public async Task<IActionResult> Logout() 
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        //Account/Roles/List
        [HttpGet]
        public IActionResult ListRoles() 
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }




        //Account/Roles
        [HttpGet]
        public IActionResult CreateRole() 
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleDto model) 
        {
            if (ModelState.IsValid) 
            {
                IdentityRole identityRole = new IdentityRole { Name = model.RoleName  };
                IdentityResult result = await _roleManager.CreateAsync(identityRole);
                if (result.Succeeded) return RedirectToAction(nameof(AccountController.ListRoles), "Account");
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View();
        }
    }
}