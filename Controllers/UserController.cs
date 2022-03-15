using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryProject.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryProject.Controllers
{
    [Authorize(Roles = "Superadministrator")]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            


        }
        
        public async Task<IActionResult> IndexAsync()
        {
            Users user = new Users();
            var users = await _userManager.Users.ToListAsync();
            user.userNames = users.Select(x => x.UserName).ToList();


            return View(user.userNames);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            //fix this shit
            Users user = new Users();
            //var users = await _userManager.Users.ToListAsync();
            //user.userNames = users.Select(x => x.UserName).ToList();
            user.userNames = await _userManager.Users.Select(x => x.UserName).ToListAsync();

            var userName = user.userNames.ElementAt((int)id);

            if (userName == null)
            {
                return NotFound();
            }

            user.userName = userName;

            //var userId = users.Where(x => x.UserName == userName).FirstOrDefault();
            var userId = await _userManager.Users.Where(x => x.UserName == userName).FirstOrDefaultAsync();
            var roles = await _roleManager.Roles.ToListAsync();
            var userRoles = await _userManager.GetRolesAsync(userId);

            user.userRoles = userRoles;
            user.roles = roles;
            user.hasRole = new List<bool>();

            foreach(var role in roles)
            {
                if(userRoles.Contains(role.ToString()))
                {
                    user.hasRole.Add(true);
                }
                else
                {
                    user.hasRole.Add(false);
                }
            }
            
            if (userId == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("hasRole,userName")] Users user)
        {
            List<bool> test = user.hasRole;

            var users = await _userManager.Users.ToListAsync();
            var userToUpdate = users.Where(x => x.UserName == user.userName).FirstOrDefault();
            var roles = await _roleManager.Roles.ToListAsync();




            for (int i = 0; i < user.hasRole.Count(); i++)
            {
                if(user.hasRole[i])
                {                    
                    _userManager.AddToRoleAsync(userToUpdate,
                            roles[i].ToString()).Wait();
                }
                else
                {
                    _userManager.RemoveFromRoleAsync(userToUpdate, roles[i].ToString()).Wait();
                }

            }

            var userRoles = await _userManager.GetRolesAsync(userToUpdate);

            user.userRoles = userRoles;
            user.roles = roles;

            foreach (var role in roles)
            {
                if (userRoles.Contains(role.ToString()))
                {
                    user.hasRole.Add(true);
                }
                else
                {
                    user.hasRole.Add(false);
                }
            }


            return View(user);
        }
    }
}

