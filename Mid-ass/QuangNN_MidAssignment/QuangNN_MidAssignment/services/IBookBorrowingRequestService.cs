using Infrastructure.Models;
using QuangNN_MidAssignment.DTOs;
using QuangNN_MidAssignment.DTOs.requestDTO;
using QuangNN_MidAssignment.DTOs.responseDTO;

namespace QuangNN_MidAssignment.services
{
    public interface IBookBorrowingRequestService
    {
        Task<IEnumerable<BookBorrowingRequestResponseDto>> GetAllRequestsAsync();

        Task<IEnumerable<BookBorrowingRequestResponseDto>> GetBorrowingRequestsByUserIdAsync(Guid userId);

        Task<BookBorrowingRequest> ApproveOrRejectRequestAsync(ApproveRejectRequestDto dto);
        Task<BookBorrowingRequestResponseDto> GetRequestByIdAsync(Guid id);
        Task AddRequestAsync(BookBorrowRequestDto requestDto);
        Task UpdateRequestStatusAsync(Guid id, string status);
        Task DeleteRequestAsync(Guid id);
    }
}
