using System;
using System.Collections.Generic;
using Infrastructure.GenericModel;

namespace Infrastructure.Models
{
    public class BookBorrowingRequest : BaseEntity
    {
        public Guid RequestorId { get; set; }
        public User User { get; set; }
        public DateTime DateRequested { get; set; }
        public string Status { get; set; }
        public Guid? ApproverId { get; set; }
        public User Approver { get; set; }
        public ICollection<BookBorrowingRequestDetails> BookBorrowingRequestDetails { get; set; }
    }
}
