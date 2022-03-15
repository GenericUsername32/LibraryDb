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
using LibraryProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LibraryProject
{
    public static class SeedData
    {
        public static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if(!roleManager.RoleExistsAsync("Superadministrator").Result)
            {
                var role = new IdentityRole
                {
                    Name = "Superadministrator"
                };
                roleManager.CreateAsync(role).Wait();
            }
            if (!roleManager.RoleExistsAsync("Administrator").Result)
            {
                var role = new IdentityRole
                {
                    Name = "Administrator"
                };
                roleManager.CreateAsync(role).Wait();
            }
            if (!roleManager.RoleExistsAsync("User").Result)
            {
                var role = new IdentityRole
                {
                    Name = "User"
                };
                roleManager.CreateAsync(role).Wait();
            }
            if (!roleManager.RoleExistsAsync("Library").Result)
            {
                var role = new IdentityRole
                {
                    Name = "Library"
                };
                roleManager.CreateAsync(role).Wait();
            }
        }

        public static void SeedSuperAdmin(UserManager<ApplicationUser> userManager)
        {
            if (userManager.FindByNameAsync("superadmin").Result == null)
            {
                var user = new ApplicationUser
                {
                    UserName = "superadmin@localhost",
                    Email = "superadmin@localhost"
                };


                var result = userManager.CreateAsync
                (user, "p4ssWord!").Result;

                if(result.Succeeded)
                {
                    userManager.AddToRoleAsync(user,
                                "Superadministrator").Wait();
                }

            }

            if (userManager.FindByNameAsync("library").Result == null)
            {
                var user = new ApplicationUser
                {
                    UserName = "library@localhost",
                    Email = "library@localhost"
                };


                var result = userManager.CreateAsync
                (user, "p4ssWord!").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Library").Wait();
                }

            }


        }
    }
}
