using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Domain;
using Domain.Model;

namespace WebAPI.Controllers.ControllerFrontEnd
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly IMongoCollection<User> _usersCollection;
        private readonly ILogger _logger;

        public UserController(IMongoCollection<User> usersCollection, ILogger<UserController> logger)
        {
            _usersCollection = usersCollection;
            _logger = logger;
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

        [HttpPost("create")]
        public async Task<IActionResult> CreateUser(User newUser)
        {
            
            try
            {
                _logger.LogInformation("Called: Create user endpoint");
                await _usersCollection.InsertOneAsync(newUser);
                return CreatedAtAction(nameof(GetUserById), new { id = newUser.Id }, newUser);
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
