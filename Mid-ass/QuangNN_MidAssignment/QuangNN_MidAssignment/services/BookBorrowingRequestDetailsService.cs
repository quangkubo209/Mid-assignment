//using AutoMapper;
//using Infrastructure.Repositories;
//using Infrastructure.Models;
//using QuangNN_MidAssignment.DTOs.requestDTO;
//using QuangNN_MidAssignment.DTOs.responseDTO;

//namespace QuangNN_MidAssignment.services
//{
//    public class BookBorrowingRequestDetailsService : IBookBorrowingRequestDetailsService
//    {
//        private readonly IBookBorrowingRequestDetailsRepository _detailsRepository;
//        private readonly IMapper _mapper;

//        public BookBorrowingRequestDetailsService(IBookBorrowingRequestDetailsRepository detailsRepository, IMapper mapper)
//        {
//            _detailsRepository = detailsRepository;
//            _mapper = mapper;
//        }

//        public async Task<IEnumerable<BookBorrowingRequestDetailsResponseDto>> GetAllRequestDetailsAsync()
//        {
//            var requestDetails = await _detailsRepository.GetAllAsync();
//            return _mapper.Map<IEnumerable<BookBorrowingRequestDetailsResponseDto>>(requestDetails);
//        }

//        public async Task<BookBorrowingRequestDetailsResponseDto> GetRequestDetailsByIdAsync(Guid id)
//        {
//            var requestDetail = await _detailsRepository.GetByIdAsync(id);
//            return _mapper.Map<BookBorrowingRequestDetailsResponseDto>(requestDetail);
//        }

//        public async Task AddRequestDetailsAsync(BookBorrowingRequestDetailsRequestDto requestDetailsDto)
//        {
//            var requestDetail = _mapper.Map<BookBorrowingRequestDetails>(requestDetailsDto);
//            await _detailsRepository.AddAsync(requestDetail);
//        }

//        public async Task UpdateRequestDetailsAsync(BookBorrowingRequestDetailsRequestDto requestDetailsDto)
//        {
//            var requestDetail = _mapper.Map<BookBorrowingRequestDetails>(requestDetailsDto);
//            await _detailsRepository.UpdateAsync(requestDetail);
//        }

//        public async Task DeleteRequestDetailsAsync(Guid id)
//        {
//            await _detailsRepository.DeleteAsync(id);
//        }
//    }
//}
