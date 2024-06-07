using AutoMapper;
using Common;
using Infrastructure.Models;
using Infrastructure.Repositories;
using QuangNN_MidAssignment.DTOs;
using QuangNN_MidAssignment.DTOs.requestDTO;
using QuangNN_MidAssignment.DTOs.responseDTO;
using System.Net;

namespace QuangNN_MidAssignment.services
{
    public class BorrowingService : IBorrowingService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IUserRepository _userRepository;
        private readonly IBorrowRepositoy _borrowRepository;
        private readonly IBookBorrowingRequestRepository _bookBorrowingRequestRepository;
        private readonly IMapper _mapper;

        public BorrowingService(IBookRepository bookRepository, IUserRepository userRepository, IBorrowRepositoy borrowRepository, IBookBorrowingRequestRepository bookBorrowingRequestRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _userRepository = userRepository;
            _borrowRepository = borrowRepository;
            _bookBorrowingRequestRepository = bookBorrowingRequestRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BookBorrowingRequestResponseDto>> GetAllBorrowingRequestAsync()
        {
            var requests = await _bookBorrowingRequestRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<BookBorrowingRequestResponseDto>>(requests);
        }

        //logic handle borrow book
        public async Task<GeneralResponse> BorrowBooksAsync(BookBorrowRequestDto bookRequest)
        {
            //check count < 5
            int count = 0;
            foreach (var x in bookRequest.BookBorrowingRequestDetails)
            {
                count += x.Amount;
            }
            if (count > 5)
            {
                return new GeneralResponse
                {
                    Success = false,
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "Cannot borrow more than 5 books in one request"
                };
            }

            var user = await _userRepository.GetByIdAsync(bookRequest.RequestorId);
            if (user == null)
            {
                return new GeneralResponse
                {
                    Success = false,
                    StatusCode = HttpStatusCode.NotFound,
                    Message = "User not found"
                };
            }

            //check < 3 rq/month
            var currentMonth = DateTime.Now.Month;
            var currentYear = DateTime.Now.Year;
            var borrowRequestsThisMonth = await _borrowRepository.GetBorrowingRequestsForMonthAsync(bookRequest.RequestorId, currentMonth, currentYear);

            if (borrowRequestsThisMonth.Count() >= 3)
            {
                return new GeneralResponse
                {
                    Success = false,
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "User has exceeded the borrowing limit for this month"
                };
            }

            //var Bookrq = _mapper.Map<BookBorrowingRequest>(bookRequest);

            //add
            var request = new BookBorrowingRequest
            {
                RequestorId = bookRequest.RequestorId,
                DateRequested = DateTime.Now,
                Status = "Waiting",
                BookBorrowingRequestDetails = bookRequest.BookBorrowingRequestDetails.Select(book => new BookBorrowingRequestDetails
                {
                    BookId = book.BookId,
                    BorrowingPeriod = book.BorrowingPeriod,
                    ReturnDate = DateTime.Now.AddDays(book.BorrowingPeriod),
                    Amount = book.Amount
                }).ToList()
            };

            await _bookBorrowingRequestRepository.AddAsync(request);

            return new SuccessGeneralResponse
            {
                StatusCode = HttpStatusCode.Created,
                Message = "Books borrowed successfully",
            };
        }

    

    }
}
