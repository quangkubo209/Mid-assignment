using System.ComponentModel.DataAnnotations;

namespace QuangNN_MidAssignment.DTOs.requestDTO
{
    public class ApproveRejectRequestDto
    {
        [Required]
        public Guid RequestId { get; set; }

        [Required]
        public Guid ApproverId { get; set; }

        [Required]
        public string Status { get; set; } 
    }
}
