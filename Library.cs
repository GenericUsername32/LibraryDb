using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using LibraryProject.Models;
using System.Linq;

namespace LibraryProject
{
    public static class Library
    {
            public static void MakeLibraryRole(RoleManager<IdentityRole> roleManager)
            {

                if (!roleManager.RoleExistsAsync("Library").Result)
                {
                    var role = new IdentityRole
                    {
                        Name = "Library"
                    };
                    roleManager.CreateAsync(role).Wait();
                }
            }

            public static void MakeLibraryUser(UserManager<ApplicationUser> userManager)
            {


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
            
        public static ApplicationUser GetLibrary(UserManager<ApplicationUser> userManager)
        {
            var users = userManager.Users.ToList();
            var library = users.Where(x => x.UserName == "library@localhost").FirstOrDefault();

            return library;
        }

    }
}
