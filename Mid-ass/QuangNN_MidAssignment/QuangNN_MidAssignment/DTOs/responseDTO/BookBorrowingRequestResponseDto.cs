namespace QuangNN_MidAssignment.DTOs.responseDTO
{
    public class BookBorrowingRequestResponseDto
    {
        public Guid Id { get; set; }
        public Guid RequestorId { get; set; }

        public string UsernameRequestor { get; set; }
        public DateTime DateRequested { get; set; }
        public string Status { get; set; }
        public Guid? ApproverId { get; set; }
        public List<BookBorrowingRequestDetailsResponseDto> RequestDetails { get; set; }
    }

    public class BookBorrowingRequestDetailsResponseDto
    {
        public Guid BookId { get; set; }
        public int Amount { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}
