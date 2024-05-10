using Application_.LogicInterfaces;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Domain;
using Domain.DTOs;
using Domain.Model;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Controllers.ControllerFrontEnd
{
    [Authorize]
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IUserLogic _userLogic;  // Dependency on the IUserLogic interface


        public UserController(ILogger<UserController> logger, IUserLogic userLogic)
        {
            _logger = logger;
            _userLogic = userLogic;
        }

        [HttpGet("get/all")]
        public async Task<ActionResult<UserGetAllDto>> GetUsers()
        {
            try
            {
                var result = await _userLogic.GetUsers();
                if (result.Success == false)
                {
                    return BadRequest(result);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                UserGetAllDto userGetAllDto = new UserGetAllDto();
                userGetAllDto.Message = $"Error: {ex.Message}";
                userGetAllDto.Success = false;
                return StatusCode(500, userGetAllDto);
            }
        }
        
        [HttpGet("get/{id}")]
        public async Task<ActionResult<UserGetByIdDto>> GetUserById(string id)
        {
            UserGetByIdDto userGetByIdDto = new UserGetByIdDto(id);
            try
            {
                var result = await _userLogic.GetUserById(userGetByIdDto);
                if (result.Success == false)
                {
                    return BadRequest(result);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                userGetByIdDto.Message = $"Error: {ex.Message}";
                userGetByIdDto.Success = false;
                return StatusCode(500, userGetByIdDto);
            }
        }

		[HttpPut("update/{id}")]
public async Task<ActionResult<UserUpdateDto>> UpdateUser(string id, [FromBody] UpdateUserRequestDto updateUserRequestDto)
{
    User user = new User()
    {
        Id = id,
        Name = updateUserRequestDto?.Name,
        LastName = updateUserRequestDto?.LastName,
        Email = updateUserRequestDto?.Email,
        Password = updateUserRequestDto?.Password,
        PhoneNumber = updateUserRequestDto?.PhoneNumber
    };
    var userUpdateDto = new UserUpdateDto(id, user);
    try
    {
        var result = await _userLogic.UpdateUser(userUpdateDto);
        if (result.Success == false)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }
    catch (Exception ex)
    {
        userUpdateDto.Message = $"Error: {ex.Message}";
        userUpdateDto.Success = false;
        return StatusCode(500, userUpdateDto);
    }
}
    }
}
