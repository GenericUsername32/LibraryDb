using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LibraryProject.Models;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Linq;
using LibraryProject.Data;
using System;
using Microsoft.AspNetCore.Authorization;

namespace LibraryProject.Controllers
{
    [Authorize(Roles = "Superadministrator,Administrator,User")]
    public class ApplicationUserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public ApplicationUserController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        
        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var currentUserWithBooks = await _userManager.Users.Where(x => x.Id == currentUser.Id).Include("Books").Include("ReservedBooks").FirstOrDefaultAsync();
            
           
            

            return View(currentUserWithBooks);
        }

        [HttpPost, ActionName("Return")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Return(int id)
        {

            var book = await _context.Book.FindAsync(id);
            book.User = Library.GetLibrary(_userManager);
            book.IsAvailable = true;            
            _context.Update(book);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        
        [HttpPost, ActionName("Borrow")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BorrowBook(int id)
        {


            var book = await _context.Book.FindAsync(id);
            if (book.IsAvailable && book.IsReserved)
            {
                var currentUser = await _userManager.GetUserAsync(HttpContext.User);


                book.User = currentUser;
                book.IsAvailable = false;
                book.DueDate = DateTime.Now.AddDays(30);
                book.IsReserved = false;
                book.reserveUser = Library.GetLibrary(_userManager);
                _context.Update(book);
                await _context.SaveChangesAsync();

            }

            return RedirectToAction(nameof(Index));
        }

        
        [HttpPost, ActionName("UnReserve")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UnReserveBook(int id)
        {


            var book = await _context.Book.FindAsync(id);
            if (book.IsReserved)
            {
                


                book.reserveUser = Library.GetLibrary(_userManager);
                book.IsReserved = false;
                _context.Update(book);
                await _context.SaveChangesAsync();

            }

            return RedirectToAction(nameof(Index));
        }


    }
}
