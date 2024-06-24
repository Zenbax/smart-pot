using System;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Application_.LogicInterfaces;
using Cloud.Services;
using Domain.DTOs;
using Domain;
using Domain.Model;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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
        public async Task<ActionResult<UserLoginDto>> Login(LoginRequestDto loginRequestDto)
        {
            User user = new User()
            {
                Email = loginRequestDto.Email,
                Password = loginRequestDto.Password
            };
            UserLoginDto userLoginDto = new UserLoginDto(user);
            try
            {
                var response = await _authService.LoginUser(userLoginDto);
                if (response.Success == false)
                    return BadRequest(response);

                return Ok(response);
            }
            catch (Exception ex)
            {
                userLoginDto.Message = "Error: " + ex.Message;
                userLoginDto.Success = false;
                return StatusCode(500, userLoginDto);
            }
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserRegisterDto>> Register(RegisterRequestDto registerRequestDto)
        {
            UserRegisterDto userRegisterDto = new UserRegisterDto(new User()
            {
                Name = registerRequestDto.Name,
                LastName = registerRequestDto.LastName,
                Email = registerRequestDto.Email,
                Password = registerRequestDto.Password,
                PhoneNumber = registerRequestDto.PhoneNumber
            });
            try
            {
                var response = await _authService.RegisterUser(userRegisterDto);
                if (response.Success == false)
                    return BadRequest(response);

                return Ok(response);
            }
            catch (Exception ex)
            {
                userRegisterDto.Message = "Error: " + ex.Message;
                userRegisterDto.Success = false;
                return StatusCode(500, userRegisterDto);
            }
        }
    }
}
