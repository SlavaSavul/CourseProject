using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CourseProject.Controllers
{

    [Authorize(Roles = "Admin")]
    public class RolesController : Controller
    {
        RoleManager<IdentityRole> _roleManager;
        UserManager<ApplicationUser> _userManager;
        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task AssignRole(string id, string role)
        {

            ApplicationUser user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                if (!userRoles.Contains(role))
                {
                    await _userManager.AddToRoleAsync(user, role);
                }
            }
        }
        
        [HttpPost]
        public async Task DeleteRole(string id, string role)
        {
            ApplicationUser currentUser =await _userManager.FindByEmailAsync(User.Identity.Name);

            if (id == currentUser.Id)
            {
                return;
            }
            ApplicationUser user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                if (userRoles.Contains(role))
                {
                    await _userManager.RemoveFromRoleAsync(user,role);
                }
            }
        }

    }
}