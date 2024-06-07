using AutoMapper;
using Infrastructure.Repositories;
using Infrastructure.Models;
using QuangNN_MidAssignment.DTOs.requestDTO;
using QuangNN_MidAssignment.DTOs.responseDTO;
using QuangNN_MidAssignment.DTOs;

namespace QuangNN_MidAssignment.services
{
    public class BookBorrowingRequestService : IBookBorrowingRequestService
    {
        private readonly IBookBorrowingRequestRepository _requestRepository;
        private readonly IMapper _mapper;
        private readonly IBookBorrowingRequestRepository _bookBorrowingRequestRepository;
        private readonly IBorrowRepositoy _borrowRepository;

        public BookBorrowingRequestService(IBookBorrowingRequestRepository requestRepository, IMapper mapper, IBookBorrowingRequestRepository bookBorrowingRequestRepository, IBorrowRepositoy borrowRepositoty)
        {
            _requestRepository = requestRepository;
            _mapper = mapper;
            _bookBorrowingRequestRepository = bookBorrowingRequestRepository;
            _borrowRepository = borrowRepositoty;
        }

        //public async Task<IEnumerable<BookBorrowingRequestResponseDto>> GetAllRequestsAsync()
        //{
        //    var requests = await _requestRepository.GetAllBorrowingRequestsWithDetailsAsync();
        //    return _mapper.Map<IEnumerable<BookBorrowingRequestResponseDto>>(requests);
        //}
        public async Task<IEnumerable<BookBorrowingRequestResponseDto>> GetAllRequestsAsync()
        {
            var requests = await _requestRepository.GetAllBorrowingRequestsWithDetailsAsync();
            return MapToResponseDto(requests);
        }

        private IEnumerable<BookBorrowingRequestResponseDto> MapToResponseDto(IEnumerable<BookBorrowingRequest> requests)
        {
            var responseDtoList = new List<BookBorrowingRequestResponseDto>();

            foreach (var request in requests)
            {
                var requestDto = new BookBorrowingRequestResponseDto
                {
                    Id = request.Id,
                    RequestorId = request.RequestorId,
                    UsernameRequestor = request.User.Username,
                    DateRequested = request.DateRequested,
                    Status = request.Status,
                    RequestDetails = request.BookBorrowingRequestDetails.Select(detail => new BookBorrowingRequestDetailsResponseDto
                    {
                        BookId = detail.BookId,
                        Amount = detail.Amount,
                        ReturnDate = detail.ReturnDate
                    }).ToList()
                };

                responseDtoList.Add(requestDto);
            }

            return responseDtoList;
        }

        public async Task<IEnumerable<BookBorrowingRequestResponseDto>> GetBorrowingRequestsByUserIdAsync(Guid userId)
        {
            var requests = await _borrowRepository.GetBorrowingRequestsByUserIdAsync(userId);
            return MapToResponseDto(requests);
        }

        
        public async Task<BookBorrowingRequestResponseDto> GetRequestByIdAsync(Guid id)
        {
            var request = await _requestRepository.GetByIdAsync(id);
            return _mapper.Map<BookBorrowingRequestResponseDto>(request);
        }

        public async Task AddRequestAsync(BookBorrowRequestDto requestDto)
        {
            var request = _mapper.Map<BookBorrowingRequest>(requestDto);
            await _requestRepository.AddAsync(request);
        }

        public async Task UpdateRequestStatusAsync(Guid id, string status)
        {
            var request = await _requestRepository.GetByIdAsync(id);
            if (request != null)
            {
                request.Status = status;
                await _requestRepository.UpdateAsync(request);
            }
        }

        public async Task DeleteRequestAsync(Guid id)
        {
            await _requestRepository.DeleteAsync(id);
        }

        public async Task<BookBorrowingRequest> ApproveOrRejectRequestAsync(ApproveRejectRequestDto dto)
        {
            var request = await _bookBorrowingRequestRepository.GetByIdAsync(dto.RequestId);
            if (request == null)
            {
                throw new Exception("Request not found");
            }

            request.ApproverId = dto.ApproverId;
            request.Status = dto.Status;
            request.UpdatedAt = DateTime.UtcNow;

            await _bookBorrowingRequestRepository.UpdateAsync(request);
            return request;
        }
    }
}
