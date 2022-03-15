using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;


namespace LibraryProject.Models
{
    public class ApplicationUser : IdentityUser
    {
        public List<Book> Books { get; set; }
        public List<Book> ReservedBooks { get; set; }
    }
}
