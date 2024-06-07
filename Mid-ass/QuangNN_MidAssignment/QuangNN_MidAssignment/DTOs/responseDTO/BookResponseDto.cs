namespace QuangNN_MidAssignment.DTOs.responseDTO
{
    public class BookResponseDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public Guid CategoryId { get; set; }

        public int PublicationYear { get; set;  }

        public string CategoryName { get; set; }

        public int InstockAmount { get; set; }

        public DateTime ReturnDate { get; set; }

        public string Status { get; set; }
    }
}
