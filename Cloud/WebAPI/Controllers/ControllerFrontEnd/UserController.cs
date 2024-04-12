using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Domain;

namespace YourApiNamespace.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMongoCollection<User> _usersCollection;
        private readonly ILogger _logger;

        public UserController(IMongoCollection<User> usersCollection, ILogger<UserController> logger)
        {
            _usersCollection = usersCollection;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            _logger.LogInformation("1 This is a TEST!! GET USERS!");
            _logger.LogWarning("2 This is a TEST!! GET USERS! warn!");
            _logger.LogError("3 This is a TEST!! GET USERS! err!");
            var users = await _usersCollection.Find(_ => true).ToListAsync();
            return Ok(users);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(User newUser)
        {
            
            await _usersCollection.InsertOneAsync(newUser);
            return CreatedAtAction(nameof(GetUserById), new { id = newUser.Id }, newUser);
        }

        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            throw new Exception("This is a TEST! Get user by id");
            
            var user = await _usersCollection.Find(user => user.Id == id).FirstOrDefaultAsync();
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
    }
}
