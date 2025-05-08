using LibraryManagementSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Application.DTOS.Books
{
    public class CreateBookDto
    {
        public long Id { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? ISBN { get; set; }
        public BookGenre Genre { get; set; }
        public BookFormat Format { get; set; }
        public BookAvailability Availability { get; set; }
        public DateTime PublishedDate { get; set; }
        public string? Description { get; set; }

    }
}
