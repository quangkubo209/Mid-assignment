using Infrastructure.Models;
using System.ComponentModel.DataAnnotations;

namespace QuangNN_MidAssignment.DTOs.requestDTO
{
    public class UserRequestDto
    {
        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; } 

        [Required]
        public RoleEnum Role { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }
    }
}
