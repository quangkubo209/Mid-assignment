using Common;
using Infrastructure.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuangNN_MidAssignment.DTOs;
using QuangNN_MidAssignment.DTOs.requestDTO;
using QuangNN_MidAssignment.DTOs.responseDTO;
using QuangNN_MidAssignment.services;

namespace QuangNN_MidAssignment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IBorrowingService _borrowService;

        public BookController(IBookService bookService, IBorrowingService borrowingService)
        {
            _bookService = bookService;
            _borrowService = borrowingService;
        }

        [HttpGet]
        public async Task<ActionResult<GeneralResponse<IEnumerable<BookResponseDto>>>> GetAllBooks()
        {
            var books = await _bookService.GetAllBooksAsync();
            return (new SuccessGeneralResponse<IEnumerable<BookResponseDto>>
            {
                Content = books,
                StatusCode = System.Net.HttpStatusCode.OK
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GeneralResponse<BookResponseDto>>> GetBookById(Guid id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
            {
                return (new GeneralResponse<BookResponseDto>
                {
                    Success = false,
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Message = "Book not found"
                });
            }
            return (new SuccessGeneralResponse<BookResponseDto>
            {
                Content = book,
                StatusCode = System.Net.HttpStatusCode.OK
            });
        }

        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<GeneralResponse>> AddBook([FromBody] BookRequestDto bookRequestDto)
        {
            await _bookService.AddBookAsync(bookRequestDto);
            return new SuccessGeneralResponse
            {
                StatusCode = System.Net.HttpStatusCode.Created,
                Message = "Book created successfully"
            };
        }

        [HttpPut("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<GeneralResponse>> UpdateBook(Guid id, [FromBody] BookRequestDto bookRequestDto)
        {
            var existingBook = await _bookService.GetBookByIdAsync(id);
            if (existingBook == null)
            {
                return (new GeneralResponse
                {
                    Success = false,
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Message = "Book not found"
                });
            }
            await _bookService.UpdateBookAsync( id, bookRequestDto);
            return (new SuccessGeneralResponse
            {
                StatusCode = System.Net.HttpStatusCode.NoContent,
                Message = "Book updated successfully"
            });
        }

        [HttpDelete("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<GeneralResponse>> DeleteBook(Guid id)
        {
            var existingBook = await _bookService.GetBookByIdAsync(id);
            if (existingBook == null)
            {
                return (new GeneralResponse
                {
                    Success = false,
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Message = "Book not found"
                });
            }

            await _bookService.DeleteBookAsync(id);
            return (new SuccessGeneralResponse
            {
                StatusCode = System.Net.HttpStatusCode.NoContent,
                Message = "Book deleted successfully"
            });
        }

        [HttpPost("borrow")]
        //[Authorize(Roles = "User")]
        public async Task<ActionResult<GeneralResponse>> BorrowBooks([FromBody] BookBorrowRequestDto bookRequest)
        {
            var response = await _borrowService.BorrowBooksAsync(bookRequest);
            if (response.Success)
            {
                return Ok(response);
            }
            return StatusCode((int)response.StatusCode, response);
        }

        // get borrow list by user 
        //[HttpPut("user-borrow/{id}")]
        ////[Authorize(Policy = "UserPolicy")]
        //public async Task<ActionResult<GeneralResponse<IEnumerable<BookResponseDto>>>> GetBorrowedBooks(Guid id)
        //{
        //    var response = await _borrowService.GetBorrowedBooksAsync(id);
        //    if (response.Success)
        //    {
        //        return Ok(response);
        //    }
        //    return StatusCode((int)response.StatusCode, response);
        //}
    }
}
