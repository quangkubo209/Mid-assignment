using Infrastructure.Models;

namespace QuangNN_MidAssignment.DTOs.responseDTO
{
    public class UserResponseDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public RoleEnum Role { get; set; }
    }
}
