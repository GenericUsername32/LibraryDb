using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryProject.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public Shelf? Shelf { get; set; }
        public ApplicationUser? User { get; set; }   
        public bool IsAvailable { get; set; }
        public bool IsReserved { get; set; }
        [InverseProperty("ReservedBooks")]
        public ApplicationUser? reserveUser { get; set; }
        public DateTime DueDate { get; set; }
    }
}
