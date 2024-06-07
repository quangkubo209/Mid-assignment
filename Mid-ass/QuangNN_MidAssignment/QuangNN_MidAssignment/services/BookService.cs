using AutoMapper;
using Infrastructure.Repositories;
using Infrastructure.Models;
using QuangNN_MidAssignment.DTOs.requestDTO;
using QuangNN_MidAssignment.DTOs.responseDTO;

namespace QuangNN_MidAssignment.services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public BookService(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BookResponseDto>> GetAllBooksAsync()
        {
            //var books = await _bookRepository.GetAllAsync();
            var books = await _bookRepository.GetAllBookAsync();

            return _mapper.Map<IEnumerable<BookResponseDto>>(books);
        }

        public async Task<BookResponseDto> GetBookByIdAsync(Guid id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            return _mapper.Map<BookResponseDto>(book);
        }

        public async Task AddBookAsync(BookRequestDto bookRequestDto)
        {
            var book = _mapper.Map<Book>(bookRequestDto);
            await _bookRepository.AddAsync(book);
        }

        public async Task UpdateBookAsync(Guid id,BookRequestDto bookRequestDto)
        {
            var book = _mapper.Map<Book>(bookRequestDto);
            book.Id = id;
            await _bookRepository.UpdateAsync(book);
        }

        public async Task DeleteBookAsync(Guid id)
        {
            await _bookRepository.DeleteAsync(id);
        }
    }
}
