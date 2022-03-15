using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryProject.Data;
using LibraryProject.Models;

namespace LibraryProject.Controllers
{
    
    public class BooksController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public BooksController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Books
        [Authorize(Roles = "Superadministrator,Administrator,User")]
        public async Task<IActionResult> Index(string searchString, string SearchType, string SearchCategory)
        {
            string test = SearchCategory;

            if (SearchCategory == null)
            {
                SearchCategory = "";
            }

            

                if (string.IsNullOrEmpty(searchString))

                {
                    return View(await _context.Book.Where(x => x.Shelf.Section.Name.Contains(SearchCategory)).Where(x => x.Shelf.Section.Name.Contains(SearchCategory)).ToListAsync());
                }

                if (SearchType == "Author")
                {
                    return View(await _context.Book.Where(x => x.Shelf.Section.Name.Contains(SearchCategory)).Where(x => x.Author.Contains(searchString)).ToListAsync());
                }
                else
                {
                    return View(await _context.Book.Where(x => x.Shelf.Section.Name.Contains(SearchCategory)).Where(x => x.Title.Contains(searchString)).ToListAsync());
                }
            

        }

        // GET: Books/Details/5
        [Authorize(Roles = "Superadministrator,Administrator,User")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book.Include(x => x.Shelf).ThenInclude(y => y.Section)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        [Authorize(Roles = "Superadministrator,Administrator")]
        public IActionResult Create()
        {
            TempData["shelvesList"] = _context.Shelf.Select(x => x.ShelfName).ToList();
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Superadministrator,Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Author")] Book book, [Bind("Shelf")] string shelf)
        {
            book.Shelf = _context.Shelf.Where(x => x.ShelfName == shelf).ToList().FirstOrDefault();
            book.User = Library.GetLibrary(_userManager);
            book.IsAvailable = true;

            if (ModelState.IsValid)
            {

                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Edit/5
        [Authorize(Roles = "Superadministrator,Administrator")]
        public async Task<IActionResult> Edit(int id)
        {
                        
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Superadministrator,Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Author,IsAvailable,IsReserved")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Delete/5
        [Authorize(Roles = "Superadministrator,Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [Authorize(Roles = "Superadministrator,Administrator")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Book.FindAsync(id);
            _context.Book.Remove(book);
            await _context.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Book.Any(e => e.Id == id);
        }

        [Authorize(Roles = "Superadministrator,Administrator,User")]
        [HttpPost, ActionName("Borrow")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BorrowBook(int id)
        {


            var book = await _context.Book.FindAsync(id);
            if (book.IsAvailable)
            {
                var currentUser = await _userManager.GetUserAsync(HttpContext.User);


                book.User = currentUser;
                book.IsAvailable = false;
                book.DueDate = DateTime.Now.AddDays(30);
                _context.Update(book);
                await _context.SaveChangesAsync();            

            }

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Superadministrator,Administrator,User")]
        [HttpPost, ActionName("Reserve")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReserveBook(int id)
        {


            var book = await _context.Book.FindAsync(id);
            if (!book.IsReserved)
            {
                var currentUser = await _userManager.GetUserAsync(HttpContext.User);


                book.reserveUser = currentUser;
                book.IsReserved = true;                
                _context.Update(book);
                await _context.SaveChangesAsync();

            }

            return RedirectToAction(nameof(Index));
        }


    }
}
