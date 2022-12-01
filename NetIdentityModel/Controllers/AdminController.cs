using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NetIdentityModel.Models;
using NetIdentityModel.Models.AccountDto;
using System.Diagnostics;

namespace NetIdentityModel.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;  
        private readonly ILogger<HomeController> _logger; //responsável	por	registrar mensagens de Log e exibi-las.

        public AdminController(
                                UserManager<ApplicationUser> userManager,
                                RoleManager<IdentityRole> roleManager,
                                ILogger<HomeController> logger
                                ) : base()
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }



        //Admin/EditRoles
        [HttpGet]
        public async Task<IActionResult> EditRole(string id) 
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role ==null) 
            {
                ViewBag.ErrorMessage = $"Role com Id = {id} não foi localizada";
                return View("NotFound");
            }
            var model = new EditRoleDto { Id = role.Id, RoleName = role.Name };
            var listaUsuarios = _userManager.Users.ToList();
            foreach(var user in listaUsuarios) 
            {
                if (await _userManager.IsInRoleAsync(user,role.Name)) 
                {
                    model.Users.Add(user.UserName);
                }
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleDto model)
        {
            var role = await _roleManager.FindByIdAsync(model.Id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role com Id = {model.Id} não foi encontrada";
                return View("NotFound");
            }
            else {
                role.Name = model.RoleName;
                var result = await _roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);
            }
        }



        [HttpGet]
        public async Task<IActionResult> EditUsersInRole(string roleId) 
        {
            ViewBag.roleId = roleId;
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role ==null) 
            {
                ViewBag.ErrorMessage = $"Role com Id = {roleId} não foi encontrada";
                return View("NotFound");
            }
            var model = new List<UserRoleDto>();
            var listaUsuarios = _userManager.Users.ToList();
            foreach (var user in listaUsuarios)
            {
                var userRoleViewModel = new UserRoleDto
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userRoleViewModel.IsSelected = true;
                }
                else
                {
                    userRoleViewModel.IsSelected = false;
                }
                model.Add(userRoleViewModel);
            }
            return View();
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
                if (result.Succeeded) return RedirectToAction(nameof(AdminController.ListRoles), "Admin");
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View();
        }
    }
}