using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identity.Models;
using Identity.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Controllers
{
    [Authorize(Roles ="Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private RoleManager<ApplicationRole> _roleManager;
        public UserController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {
           List<ApplicationUser> applicationUser = _userManager.Users.ToList();
            List<UserViewModel> userViewModels = new List<UserViewModel>();
            foreach(var item in applicationUser)
            {
               
                UserViewModel userViewModel = new UserViewModel()
                {
                    Id= item.Id,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Email = item.Email,
                    ProfilePicture=item.ProfilePicture
                   
                };
                userViewModel.RoleNames = await _userManager.GetRolesAsync(item);
                userViewModels.Add(userViewModel);
            }
            return View(userViewModels);
        }
         [HttpGet]
        public async Task<IActionResult> EditUser(string Id)
        {
            if (!string.IsNullOrEmpty(Id))
            {
                ApplicationUser user = await _userManager.FindByIdAsync(Id);
                UserEditViewModel userViewModel = new UserEditViewModel()
                {
                    
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    ProfilePicture = user.ProfilePicture
                };
                foreach(var role in _roleManager.Roles)
                {
                    UserRoleViewModel userRoleViewModel = new UserRoleViewModel()
                    {
                        RoleId = role.Id,
                        RoleName = role.Name,
                    };
                    if(await _userManager.IsInRoleAsync(user, role.Name))
                    {
                        userRoleViewModel.IsSelected = true;
                    }
                    else
                    {
                        userRoleViewModel.IsSelected = false;
                    }
                    userViewModel.userRoleViewModel.Add(userRoleViewModel);
                }
                return View(userViewModel);
            }
            return RedirectToAction("Index");
           
        }

      

        [HttpPost]
        public async Task<IActionResult> EditUser(UserEditViewModel userViewModel)
        {
            if (userViewModel != null)
            {
                ApplicationUser item = await _userManager.FindByIdAsync(userViewModel.Id);
                if(item==null)
                {
                    ViewBag.ErrorMessage = "User Cannot Be Found";
                    return View("Error");
                }

                item.FirstName = userViewModel.FirstName;
                item.LastName = userViewModel.LastName;
                var roles = await _userManager.GetRolesAsync(item);
                var result = await _userManager.RemoveFromRolesAsync(item, roles);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "Cannot  remove User's existing roles");
                    return View(userViewModel);
                }
                result = await _userManager.AddToRolesAsync(item, userViewModel.userRoleViewModel.Where(x => x.IsSelected).Select(x => x.RoleName));
                var Result = await _userManager.UpdateAsync(item);
                if (Result.Succeeded && result.Succeeded)
                {
                    TempData["Update"] = "User has been updated successfully";
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var errors in Result.Errors)
                    {
                        ModelState.AddModelError("", errors.Description);
                    }
                }
            }
            return View(userViewModel);





        }

    }
}