using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NetIdentityModel.Models;
using NetIdentityModel.Models.AccountDto;
using System.Data;
using System.Diagnostics;

namespace NetIdentityModel.Controllers
{
    [Authorize(Roles = "Admin")]
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

        //Admin/Roles/List
        [HttpGet]
        public IActionResult ListRoles()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }
        //Admin/Roles/List users
        [HttpGet]
        public IActionResult ListUsers() 
        {
            var users = _userManager.Users;
            return View(users);
        }


        //Admin/Roles
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
                IdentityRole identityRole = new IdentityRole { Name = model.RoleName };
                IdentityResult result = await _roleManager.CreateAsync(identityRole);
                if (result.Succeeded) return RedirectToAction(nameof(AdminController.ListRoles), "Admin");
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View();
        }

        //Admin/EditRoles
        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role com Id = {id} não foi localizada";
                return View("NotFound");
            }
            var model = new EditRoleDto { Id = role.Id, RoleName = role.Name };
            var listaUsuarios = _userManager.Users.ToList();
            foreach (var user in listaUsuarios)
            {
                if (await _userManager.IsInRoleAsync(user, role.Name))
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
            else
            {
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


        //Admin/EditUser
        [HttpGet]
        public async Task<IActionResult> EditUser(string id) 
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"Usuário com Id = {id} não foi encontrado";
                return View("NotFound");
            }
            // GetClaimsAsync retorna a lista de Claims
            var userClaims = await _userManager.GetClaimsAsync(user);
            // GetRolesAsync retorna a lista de Roles
            var userRoles = await _userManager.GetRolesAsync(user);
            var model = new EditUserDto
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                Cpf = user.Cpf,
                UserExtended = user.UserExtended,
                Claims = userClaims.Select(c => c.Value).ToList(),
                Roles = userRoles
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserDto model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"Usuário com Id = {model.Id} não foi encontrado";
                return View("NotFound");
            }
            else
            {
                user.Email = model.Email;
                user.UserName = model.UserName;
                user.Cpf = user.Cpf;
                user.UserExtended = user.UserExtended;
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListUsers");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);
            }
        }


        //Admin/Edit user in role
        [HttpGet]
        public async Task<IActionResult> EditUsersInRole(string roleId) 
        {
            ViewBag.roleId = roleId;

            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role com Id = {roleId} não foi encontrada";
                return View("NotFound");
            }
            var model = new List<UserRoleDto>();
            var listaUsuarios = _userManager.Users.ToList();
            foreach (var user in listaUsuarios)
            {
                var UserRoleDto = new UserRoleDto
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    UserRoleDto.IsSelected = true;
                }
                else
                {
                    UserRoleDto.IsSelected = false;
                }
                model.Add(UserRoleDto);
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(List<UserRoleDto> model, string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role com Id = {roleId} não foi encontrada";
                return View("NotFound");
            }

            for (int i = 0; i < model.Count; i++)
            {
                var user = await _userManager.FindByIdAsync(model[i].UserId);

                IdentityResult result = null;

                if (model[i].IsSelected && !(await _userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await _userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!model[i].IsSelected && await _userManager.IsInRoleAsync(user, role.Name))
                {
                    result = await _userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }

                if (result.Succeeded)
                {
                    if (i < (model.Count - 1))
                        continue;
                    else
                        return RedirectToAction("EditRole", new { Id = roleId });
                }
            }
            return RedirectToAction("EditRole", new { Id = roleId });
        }

        //Admin/Manager Roles
        [HttpGet]
        public async Task<IActionResult> ManageUserRoles(string userId) 
        {
            ViewBag.userId = userId;

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"Usuário com Id = {userId} não foi encontrado";
                return View("NotFound");
            }

            var model = new List<UserRolesDto>();
            var userRoles = _roleManager.Roles.ToList();

            foreach (var role in userRoles)
            {
                var userRolesViewModel = new UserRolesDto
                {
                    RoleId = role.Id,
                    RoleName = role.Name
                };

                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userRolesViewModel.IsSelected = true;
                }
                else
                {
                    userRolesViewModel.IsSelected = false;
                }
                model.Add(userRolesViewModel);
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> ManageUserRoles(List<UserRolesDto> model, string userId)
        {
            return View(model);
        }


        //Admin/DeleteRole
        [HttpPost]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role com Id = {id} não foi encontrada";
                return View("NotFound");
            }
            else
            {
                var result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View("ListRoles");
            }
        }

        //Admin/DeleteUser
        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"Usuário com Id = {id} não foi encontrado";
                return View("NotFound");
            }
            else
            {
                var result = await _userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListUsers");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();
            }
        }
    }
}