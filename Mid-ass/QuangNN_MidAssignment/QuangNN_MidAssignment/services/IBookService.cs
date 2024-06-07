using Infrastructure.Models;
using QuangNN_MidAssignment.DTOs.requestDTO;
using QuangNN_MidAssignment.DTOs.responseDTO;

namespace QuangNN_MidAssignment.services
{
    public interface IBookService
    {
        Task<IEnumerable<BookResponseDto>> GetAllBooksAsync();
        Task<BookResponseDto> GetBookByIdAsync(Guid id);
        Task AddBookAsync(BookRequestDto bookRequestDto);
        Task UpdateBookAsync(Guid id,BookRequestDto bookRequestDto);
        Task DeleteBookAsync(Guid id);
    }
}
