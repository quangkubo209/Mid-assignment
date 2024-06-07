using Infrastructure.Models;

namespace QuangNN_MidAssignment.DTOs.responseDTO
{
    public class AuthResponseDto
    {
        public string Token { get; set; }
        public string Username { get; set; }

        public Guid UserId { get; set; }

        public RoleEnum Role { get; set; }
    }
}
