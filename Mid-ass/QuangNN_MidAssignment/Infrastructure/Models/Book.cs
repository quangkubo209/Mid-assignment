using System;
using Infrastructure.GenericModel;

namespace Infrastructure.Models
{
    public class Book : BaseEntity
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public int PublicationYear { get; set; }

        public string Description { get; set; }
        public double AverageRating { get; set; }

        public int InstockAmount { get; set; }

        public ICollection<BookBorrowingRequestDetails> BookBorrowRequestDetails { get; set; }
    }
}
