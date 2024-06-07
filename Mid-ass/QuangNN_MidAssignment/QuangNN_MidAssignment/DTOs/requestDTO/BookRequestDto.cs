using Infrastructure.Models;
using System.ComponentModel.DataAnnotations;

namespace QuangNN_MidAssignment.DTOs.requestDTO
{
    public class BookRequestDto
    {
  
        [Required]
        public string Title { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public Guid CategoryId { get; set; }

        [Required]
        public int PublicationYear { get; set; }

        
        public string Description { get; set; }

        public double AverageRating { get; set; }

        [Required]
        public int InstockAmount { get; set; }
    }
}
