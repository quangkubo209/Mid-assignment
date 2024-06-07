using System;
using Infrastructure.GenericModel;

namespace Infrastructure.Models
{
    public class BookBorrowingRequestDetails : BaseEntity
    {
        public Guid BookBorrowingRequestId { get; set; }
        public BookBorrowingRequest BookBorrowingRequest { get; set; }
        public Guid BookId { get; set; }
        public Book Book { get; set; }
        public int BorrowingPeriod { get; set; }
        public DateTime? ReturnDate { get; set; }

        public int Amount { get; set; }
    }
}
