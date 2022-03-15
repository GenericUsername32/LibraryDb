using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using LibraryProject.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LibraryProject
{
    public static class RoleChanger
    {
        public static async void AddToRole(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            List<IdentityUser> users = new List<IdentityUser>();

            

            users = await userManager.Users.ToListAsync();

            var userid = users.Where(x => x.UserName == "blabla@bla.com").FirstOrDefault();
            var userid2 = users.Where(x => x.UserName == "test@test").FirstOrDefault();
            var userid3 = users.Where(x => x.UserName == "superadmin@localhost").FirstOrDefault();
            var userRoles = await roleManager.Roles.ToListAsync();
            var roles = await userManager.GetRolesAsync(userid);

            userManager.AddToRoleAsync(userid,
            "User").Wait();

            var user = new IdentityUser
            {
                UserName = "test@test",
                Email = "test@test"
            };


            var result = userManager.CreateAsync
            (user, "p4ssWord!").Result;

            if (result.Succeeded)
            {
                userManager.AddToRoleAsync(user,
                            "User").Wait();
            }

            var roles2 = await userManager.GetRolesAsync(userid);
            var roles3 = await userManager.GetRolesAsync(userid2);
            var roles4 = await userManager.GetRolesAsync(userid3);


            int helo = 1;
        }
    }
}
