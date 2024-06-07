using Infrastructure.Models;
using Infrastructure.Repositories;
using Microsoft.IdentityModel.Tokens;
using QuangNN_MidAssignment.DTOs.requestDTO;
using QuangNN_MidAssignment.DTOs.responseDTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;
using Microsoft.AspNetCore.Identity;


namespace QuangNN_MidAssignment.services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<AuthResponseDto> AuthenticateAsync(AuthRequestDto authRequest)
        {
            var user = await _userRepository.GetUserByUsernameAsync(authRequest.Username);

            //check password
            if (user == null || !VerifyPassword(user, authRequest.Password))
                return null;

            //Generate token if validate successfully
            var token = GenerateToken(user);

            // Return user details along with token
            return new AuthResponseDto
            {
                Username = user.Username,
                Role = user.Role,
                Token = token,
                UserId = user.Id,
            };
        }

        //check validation of password
        private bool VerifyPassword(User user, string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
        }

        //public string GenerateToken(User user)
        //{
        //    var jwtSettings = _configuration.GetSection("Jwt");
        //    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
        //    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        //    var claims = new[]
        //    {
        //    new Claim(JwtRegisteredClaimNames.Sub, user.Username),
        //    new Claim(ClaimTypes.Role, user.Role.ToString()),
        //    //new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        //    new Claim(JwtRegisteredClaimNames.Jti, user.Id.ToString())

        //};

        //    var token = new JwtSecurityToken(
        //        issuer: jwtSettings["Issuer"],
        //        audience: jwtSettings["Audience"],
        //        claims: claims,
        //        expires: DateTime.Now.AddMinutes(double.Parse(jwtSettings["ExpiryMinutes"])),
        //        signingCredentials: creds
        //    );

        //    return new JwtSecurityTokenHandler().WriteToken(token);
        //}

        private string GenerateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("Jwt:Key").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

    }
}
