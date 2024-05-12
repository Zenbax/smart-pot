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
        private readonly IMongoCollection<User> _usersCollection;
        private readonly ILogger _logger;
        private readonly IUserLogic _userLogic;  // Dependency on the IUserLogic interface


        public UserController(IMongoCollection<User> usersCollection, ILogger<UserController> logger, IUserLogic userLogic)
        {
            _usersCollection = usersCollection;
            _logger = logger;
            _userLogic = userLogic;
        }

        [HttpGet("get/all")]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                _logger.LogInformation("Called: Getting all users endpoint");
                var users = await _usersCollection.Find(_ => true).ToListAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        
        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            try
            {
                _logger.LogInformation("Called: Get user by ID endpoint");
                var user = await _usersCollection.Find(user => user.Id == id).FirstOrDefaultAsync();
                if (user == null)
                {
                    return NotFound($"User with ID {id} not found.");
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}