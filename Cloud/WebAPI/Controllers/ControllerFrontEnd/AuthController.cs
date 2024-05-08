using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application_.LogicInterfaces;
using Cloud.Services;
using Domain.DTOs;
using Domain;
using Domain.Model;
using Microsoft.IdentityModel.Tokens;

namespace WebAPI.Controllers.ControllerFrontEnd
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
    private readonly IAuthService _authService;
    private readonly ILogger<AuthController> _logger;

    public AuthController(IConfiguration configuration, IAuthService authService, ILogger<AuthController> logger)
    {
        _authService = authService;
        _logger = logger;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequestDto loginRequestDto)
    {
        try
        {
            var response = await _authService.LoginUser(loginRequestDto);
            if (response.Success == false)
                return BadRequest("Bad request: " + response.Message);

            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while logging in.");
            return StatusCode(500, "Internal server error occurred.");
        }
    }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequestDto registerRequestDto)
        {
            _logger.LogInformation("Called: Register user endpoint");
            Console.WriteLine("register: here is the user: " + registerRequestDto.Name);
            var createdUser = await _authService.RegisterUser(registerRequestDto);
            _logger.LogInformation("User registered successfully");
            _logger.LogInformation("Response: " + createdUser?.User.Name);
            if (createdUser == null)
            {
                _logger.LogError("Failed to register user");
                return BadRequest();
            }

            return Ok(createdUser);
        }
    }
}
