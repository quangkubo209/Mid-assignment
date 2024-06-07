using Common;
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
    public class BorrowController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IBorrowingService _borrowService;
        private readonly IBookBorrowingRequestService _bookBorrowingRequestService;

        public BorrowController(IBookService bookService, IBorrowingService borrowingService , IBookBorrowingRequestService bookBorrowingRequestService)
        {
            _bookService = bookService;
            _borrowService = borrowingService;
            _bookBorrowingRequestService = bookBorrowingRequestService;
        }

        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<GeneralResponse<IEnumerable<BookBorrowingRequestResponseDto>>>> GetAllBorrowingRequests()
        {
            var books = await _bookBorrowingRequestService.GetAllRequestsAsync();
            return (new SuccessGeneralResponse<IEnumerable<BookBorrowingRequestResponseDto>>
            {
                Content = books,
                StatusCode = System.Net.HttpStatusCode.OK
            });
        }

        //get request by user id
        [HttpGet("user/{userId}/borrowing-requests")]
        public async Task<ActionResult<IEnumerable<BookBorrowingRequestResponseDto>>> GetBorrowingRequestsByUserId(Guid userId)
        {
            var requests = await _bookBorrowingRequestService.GetBorrowingRequestsByUserIdAsync(userId);
            if (requests == null)
            {
                return NotFound();
            }
            return Ok(requests);
        }

        //admin approve or reject status
        [HttpPost("change-status")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> ApproveOrRejectRequest([FromBody] ApproveRejectRequestDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Request data is null");
            }

            if (dto.Status != "approved" && dto.Status != "rejected")
            {
                return BadRequest("Invalid status value. It must be either 'approved' or 'rejected'.");
            }

            try
            {
                var result = await _bookBorrowingRequestService.ApproveOrRejectRequestAsync(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }



    }   
}
