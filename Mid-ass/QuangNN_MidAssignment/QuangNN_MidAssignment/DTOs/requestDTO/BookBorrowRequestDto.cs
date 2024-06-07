using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuangNN_MidAssignment.DTOs
{
    public class BookBorrowRequestDto
    {
        public Guid Id { get; set; }
        [Required]
        public Guid RequestorId { get; set; }

        [Required]
        public DateTime DateRequested { get; set; }

        [Required]
        public string Status { get; set; }


        [Required]
        public ICollection<BookBorrowItemDto> BookBorrowingRequestDetails { get; set; }
    }

    public class BookBorrowItemDto
    {
        [Required]
        public Guid BookId { get; set; }

        [Required]
        public int BorrowingPeriod { get; set; }

        //public DateTime? ReturnDate { get; set; }

        [Required]
        public int Amount { get; set; }
    }
}
