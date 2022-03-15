using LibraryProject.Data;
using LibraryProject.Models;
using System.Linq;

namespace LibraryProject
{
    public static class BookSeed
    {
        
        public static void SeedSections(ApplicationDbContext _context)
        {
            if (!_context.Section.Any(x => x.Name == "Cookbooks"))
            {
                Section sectionA = new Section();
                Section sectionB = new Section();

                sectionA.Name = "Cookbooks";
                sectionB.Name = "Novels";

                _context.Add(sectionA);
                _context.Add(sectionB);
                _context.SaveChanges();
            }
            
        }

        public static void SeedShelves(ApplicationDbContext _context)
        {

            if (!_context.Shelf.Any(x => x.ShelfName == "Cookbooks A-M"))
            {
                Shelf shelfCookBooksAM = new Shelf();
                Shelf shelfCookBooksNZ = new Shelf();
                Shelf shelfNovelsAM = new Shelf();
                Shelf shelfNovelsNZ = new Shelf();

                shelfCookBooksAM.ShelfName = "Cookbooks A-M";
                shelfCookBooksAM.Section = _context.Section.Where(x => x.Name == "Cookbooks").FirstOrDefault();

                shelfCookBooksNZ.ShelfName = "Cookbooks N-Z";
                shelfCookBooksNZ.Section = _context.Section.Where(x => x.Name == "Cookbooks").FirstOrDefault();

                shelfNovelsAM.ShelfName = "Novels A-M";
                shelfNovelsAM.Section = _context.Section.Where(x => x.Name == "Novels").FirstOrDefault();

                shelfNovelsNZ.ShelfName = "Novels N-Z";
                shelfNovelsNZ.Section = _context.Section.Where(x => x.Name == "Novels").FirstOrDefault();

                _context.Add(shelfCookBooksAM);
                _context.Add(shelfCookBooksNZ);
                _context.Add(shelfNovelsAM);
                _context.Add(shelfNovelsNZ);
                _context.SaveChanges();
            }
        }

        public static void SeedBooks(ApplicationDbContext _context)
        {
            if (!_context.Book.Any(x => x.Title == "Flavours from Fjällbacka"))
            {
                Book Book1 = new Book();
                Book Book2 = new Book();
                Book Book3 = new Book();
                Book Book4 = new Book();
                Book Book5 = new Book();
                Book Book6 = new Book();
                Book Book7 = new Book();
                Book Book8 = new Book();

                Book1.Title = "Flavours from Fjällbacka";
                Book1.Shelf = _context.Shelf.Where(x => x.ShelfName == "Cookbooks A-M").FirstOrDefault();
                Book1.Author = "Camilla Läckberg, Christian Hellberg";
                Book1.IsAvailable = true;
                Book1.IsReserved = false;

                Book2.Title = "Köksalmanack";
                Book2.Shelf = _context.Shelf.Where(x => x.ShelfName == "Cookbooks A-M").FirstOrDefault();
                Book2.Author = "Monica Sundberg";
                Book2.IsAvailable = true;
                Book2.IsReserved = false;


                Book3.Title = "The English Huswife";
                Book3.Shelf = _context.Shelf.Where(x => x.ShelfName == "Cookbooks N-Z").FirstOrDefault();
                Book3.Author = "Gervase Markham";
                Book3.IsAvailable = true;
                Book3.IsReserved = false;

                Book4.Title = "This Is the Boke of Cokery";
                Book4.Shelf = _context.Shelf.Where(x => x.ShelfName == "Cookbooks N-Z").FirstOrDefault();
                Book4.Author = "Unknown";
                Book4.IsAvailable = true;
                Book4.IsReserved = false;

                Book5.Title = "Brave New World";
                Book5.Shelf = _context.Shelf.Where(x => x.ShelfName == "Novels A-M").FirstOrDefault();
                Book5.Author = "Aldous Huxley";
                Book5.IsAvailable = true;
                Book5.IsReserved = false;

                Book6.Title = "Animal Farm";
                Book6.Shelf = _context.Shelf.Where(x => x.ShelfName == "Novels A-M").FirstOrDefault();
                Book6.Author = "George Orwell";
                Book6.IsAvailable = true;
                Book6.IsReserved = false;

                Book7.Title = "Slaughterhouse-Five";
                Book7.Shelf = _context.Shelf.Where(x => x.ShelfName == "Novels N-Z").FirstOrDefault();
                Book7.Author = "Kurt Vonnegut";
                Book7.IsAvailable = true;
                Book7.IsReserved = false;

                Book8.Title = "Under The Volcano";
                Book8.Shelf = _context.Shelf.Where(x => x.ShelfName == "Novels N-Z").FirstOrDefault();
                Book8.Author = "Malcolm Lowry";
                Book8.IsAvailable = true;
                Book8.IsReserved = false;

                _context.Add(Book1);
                _context.Add(Book2);
                _context.Add(Book3);
                _context.Add(Book4);
                _context.Add(Book5);
                _context.Add(Book6);
                _context.Add(Book7);
                _context.Add(Book8);
                _context.SaveChanges();

            }


        }

    }
}
