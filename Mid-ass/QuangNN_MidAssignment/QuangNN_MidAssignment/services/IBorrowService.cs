using Common;
using QuangNN_MidAssignment.DTOs;
using QuangNN_MidAssignment.DTOs.requestDTO;
using QuangNN_MidAssignment.DTOs.responseDTO;

namespace QuangNN_MidAssignment.services
{
    public interface IBorrowingService
    {
        Task<IEnumerable<BookBorrowingRequestResponseDto>> GetAllBorrowingRequestAsync();
        Task<GeneralResponse> BorrowBooksAsync( BookBorrowRequestDto bookRequest);

    }
}
