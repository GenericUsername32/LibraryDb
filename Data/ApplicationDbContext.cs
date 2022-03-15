using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using LibraryProject.Models;

namespace LibraryProject.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            
        }

        public DbSet<LibraryProject.Models.Book> Book { get; set; }
        public DbSet<LibraryProject.Models.Shelf> Shelf { get; set; }
        public DbSet<LibraryProject.Models.Section> Section { get; set; }

    }
}
