using System.Collections.Generic;

namespace LibraryProject.Models
{
    public class Shelf
    {
        public int Id { get; set; }
        public string ShelfName { get; set; }
        public List<Book>? Books { get; set; }
        public Section? Section { get; set; }
    }
}
