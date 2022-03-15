using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace LibraryProject.Models
{
    public class Users
    {
        public string userName { get; set; }
        public IEnumerable<string> userRoles { get; set; }
        public List<string> userNames { get; set; }
        public List<IdentityRole> roles { get; set; }
        public List<bool> hasRole { get; set; }


    }
}
