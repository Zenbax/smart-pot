using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using ClassLibrary;
// change 2
namespace YourApiNamespace.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMongoCollection<User> _usersCollection;

        public UserController(IMongoCollection<User> usersCollection)
        {
            _usersCollection = usersCollection;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
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
            var user = await _usersCollection.Find(user => user.Id == id).FirstOrDefaultAsync();
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
    }
}
