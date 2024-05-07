using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.DTOs;
using Cloud.Services;
using Domain;
using Domain.Model;
using Microsoft.IdentityModel.Tokens;

namespace WebAPI.Controllers.ControllerFrontEnd
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthService _authService;

        public AuthController(IConfiguration configuration, IAuthService authService)
        {
            _configuration = configuration;
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var user = await _authService.LoginUser(loginDto.Email, loginDto.Password);
            if (user == null)
                return Unauthorized();

            var token = GenerateJwtToken(user);
            return Ok(new { token });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserCreationDto userDto)
        {
            var user = new User
            {
                Email = userDto.Email,
                Password = userDto.Password,
                Name = userDto.Name,
                LastName = userDto.LastName,
                PhoneNumber = userDto.PhoneNumber
            };

            var createdUser = await _authService.RegisterUser(user);
            if (createdUser == null)
                return BadRequest();

            return Ok();
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Secret"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Email),
                    // You can add more claims here if needed
                }),
                Expires = DateTime.UtcNow.AddHours(Convert.ToDouble(_configuration["Jwt:ExpireHours"])),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
