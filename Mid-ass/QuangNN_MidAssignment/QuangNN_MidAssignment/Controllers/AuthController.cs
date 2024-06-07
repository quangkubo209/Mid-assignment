using Common;
using Microsoft.AspNetCore.Mvc;
using QuangNN_MidAssignment.DTOs.requestDTO;
using QuangNN_MidAssignment.DTOs.responseDTO;
using QuangNN_MidAssignment.services;

namespace QuangNN_MidAssignment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<GeneralResponse>> Login([FromBody] AuthRequestDto authRequest)
        {
            var authResponse = await _authService.AuthenticateAsync(authRequest);
            if (authResponse == null)
                return Unauthorized();

            return new SuccessGeneralResponse<AuthResponseDto>
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Message = "Authentication successful",
                Content = authResponse 
            };
        }

    }
}
