using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class Book
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Author is required")]
        public string Author { get; set; }
        
        [RegularExpression(@"^([0-9]{10}|[0-9]{13})$", ErrorMessage = "Wrong ISBN")]
        public string ISBN { get; set; }

        [Display(Name = "Publication Year")]
        [Required(ErrorMessage = "Publication year is required")]
        [Range(1455, 2030, ErrorMessage = "Wrong year of publication")]
        public int PublicationYear { get; set; }

        public string Publisher { get; set; }

        [Required(ErrorMessage = "Country is required")]
        [StringLength(100)]
        public string Country { get; set; }
    }

    public class LibraryFilterable
    {
        public List<Book> books;
        public string FilterTitle { get; set; }
        public string FilterAuthor { get; set; }
        public string FilterISBN { get; set; }
        public string FilterYear { get; set; }
        public string FilterPublisher { get; set; }
        public string FilterCountry { get; set; }
        
        public string OrderByTitleArrow { get; set; }
        public string OrderByAuthorArrow { get; set; }
        public string OrderByISBNArrow { get; set; }
        public string OrderByYearArrow { get; set; }
        public string OrderByPublisherArrow { get; set; }
        public string OrderByCountryArrow { get; set; }
    }
}
