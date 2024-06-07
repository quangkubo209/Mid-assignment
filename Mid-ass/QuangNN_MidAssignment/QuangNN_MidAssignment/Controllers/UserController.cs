using Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using QuangNN_MidAssignment.DTOs.requestDTO;
using QuangNN_MidAssignment.DTOs.responseDTO;
using QuangNN_MidAssignment.services;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace QuangNN_MidAssignment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize(Policy = "AdminPolicy")]
    public class UserController : ControllerBase

    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly IAuthService _authService;

        public UserController(IUserService userService, IConfiguration configuration, IAuthService authService)
        {
            _userService = userService;
            _configuration = configuration;
            _authService = authService;
        }

        [HttpGet]
        public async Task<ActionResult<GeneralResponse<IEnumerable<UserResponseDto>>>> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return new SuccessGeneralResponse<IEnumerable<UserResponseDto>>
            {
                Content = users,
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GeneralResponse<UserResponseDto>>> GetUserById(Guid id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return new GeneralResponse<UserResponseDto>
                {
                    Success = false,
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Message = "User not found"
                };
            }
            return new SuccessGeneralResponse<UserResponseDto>
            {
                Content = user,
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }

        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<GeneralResponse>> AddUser([FromBody] UserRequestDto userRequestDto)
        {
            await _userService.AddUserAsync(userRequestDto);
            return new SuccessGeneralResponse
            {
                StatusCode = System.Net.HttpStatusCode.Created,
                Message = "User created successfully"
            };
        }

        [HttpPut("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<GeneralResponse>> UpdateUser(Guid id, [FromBody] UserRequestDto userRequestDto)
        {
            var existingUser = await _userService.GetUserByIdAsync(id);
            if (existingUser == null)
            {
                return new GeneralResponse
                {
                    Success = false,
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Message = "User not found"
                };
            }

            await _userService.UpdateUserAsync(id, userRequestDto);
            return new SuccessGeneralResponse
            {
                StatusCode = System.Net.HttpStatusCode.NoContent,
                Message = "User updated successfully"
            };
        }

        [HttpDelete("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<GeneralResponse>> DeleteUser(Guid id)
        {
            var existingUser = await _userService.GetUserByIdAsync(id);
            if (existingUser == null)
            {
                return new GeneralResponse
                {
                    Success = false,
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Message = "User not found"
                };
            }

            await _userService.DeleteUserAsync(id);
            return new SuccessGeneralResponse
            {
                StatusCode = System.Net.HttpStatusCode.NoContent,
                Message = "User deleted successfully"
            };
        }

        //fetch user by token 
        [HttpPost("user-token")]
        public async Task<ActionResult<GeneralResponse<UserResponseDto>>> GetUserByToken([FromBody] string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest(new GeneralResponse
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "Token is required"
                });
            }

            var userResponse = await _userService.GetUserByTokenAsync(token);
            if (userResponse == null)
            {
                return Unauthorized(new GeneralResponse
                {
                    StatusCode = HttpStatusCode.Unauthorized,
                    Message = "Invalid token"
                });
            }

            return Ok(new SuccessGeneralResponse<UserResponseDto>
            {
                StatusCode = HttpStatusCode.OK,
                Message = "User fetched successfully",
                Content = userResponse
            });
        }
    }
}
