using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identity.Models;
using Identity.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Identity.Controllers
{
    public class RolesController : Controller
    {
        //RoleManager - used to create Roles and store it in the Persistent Store e.g.Database
        //IdentityRole - represents the Role object
        private RoleManager<ApplicationRole> _roleManager;
        public RolesController(RoleManager<ApplicationRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            var RoleList = _roleManager.Roles;
            return View(RoleList);
      
        }
        
        [HttpGet]
        public IActionResult CreateRole()
        {
            //var Role = new IdentityRole(); if you are not using CreateRoleViewModel
            //return View(Role);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel createRoleViewModel)
        {
            if (ModelState.IsValid)
            {
                var RoleExist = await _roleManager.RoleExistsAsync(createRoleViewModel.RoleName);
                if (!RoleExist)
                {
                    ApplicationRole identityRole = new ApplicationRole()
                    {
                        Name = createRoleViewModel.RoleName,
                        Description=createRoleViewModel.Description,
                        CreatedDate=DateTime.Now.Date
                    };
                    IdentityResult identityResult = await _roleManager.CreateAsync(identityRole);
                    if (identityResult.Succeeded)
                    {
                       return RedirectToAction("Index", "Roles");
                    }
                    foreach(IdentityError Error in identityResult.Errors)
                    {
                        ModelState.AddModelError("", Error.Description);
                    }
                } 
            }
            ModelState.AddModelError("", "This role already exists. Please check your roles and try again");
            return View();
        }

     public async Task<IActionResult> EditRole(string Id)
        {
            var role = await _roleManager.FindByIdAsync(Id);
            if (role != null)
            {
                return View(role);
 
            }
            TempData["ErrorMsg"]  = "An error occurred while processing your request";

            return RedirectToAction("Index","Roles");
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(ApplicationRole identityRole)
        {


            var role = await _roleManager.FindByIdAsync(identityRole.Id);
            if (role != null)
            {
                role.Name = identityRole.Name;
                role.Description = identityRole.Description;
              var IdentityResult = await _roleManager.UpdateAsync(role);
                if (IdentityResult.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach(var errors in IdentityResult.Errors)
                    {
                        ModelState.AddModelError("", errors.Description);
                    }
                }
            }
            ViewBag.ErrorMsg= "An error occurred while processing your request";

            return View(identityRole);
        }
    }
}