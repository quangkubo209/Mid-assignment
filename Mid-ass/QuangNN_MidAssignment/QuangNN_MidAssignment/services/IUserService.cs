using Infrastructure.Models;
using QuangNN_MidAssignment.DTOs.requestDTO;
using QuangNN_MidAssignment.DTOs.responseDTO;

namespace QuangNN_MidAssignment.services
{
    public interface IUserService
    {
        Task<IEnumerable<UserResponseDto>> GetAllUsersAsync();
        Task<UserResponseDto> GetUserByIdAsync(Guid id);
        Task AddUserAsync(UserRequestDto userRequestDto);
        Task UpdateUserAsync(Guid id, UserRequestDto userRequestDto);
        Task DeleteUserAsync(Guid id);

        Task<UserResponseDto> GetUserByTokenAsync(string token);
    }
}
