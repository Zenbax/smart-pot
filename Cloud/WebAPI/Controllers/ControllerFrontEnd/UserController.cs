using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace Cloud.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IMongoDatabase _database;

    public UserController(IMongoDatabase database)
    {
        _database = database;
    }


    
    
    


    /*Testing
    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var usersCollection = _database.GetCollection<User>("Users");
        var users = await usersCollection.Find(_ => true).ToListAsync();
        return Ok(users);
    }

    
    [HttpGet("{name}")]
    public async Task<IActionResult> GetUserByName(string name)
    {
        var usersCollection = _database.GetCollection<User>("users"); 
        var filter = Builders<User>.Filter.Eq("name", name);
        var user = await usersCollection.Find(filter).FirstOrDefaultAsync();

        if (user == null)
        {
            return NotFound($"User with name: {name} not found.");
        }

        return Ok(user);
    }



    [HttpPost]
    public async Task<IActionResult> PostUser([FromBody] User newUser)
    {
        if (newUser == null)
        {
            return BadRequest("User is null.");
        }
        
        var usersCollection = _database.GetCollection<User>("users"); // Make sure to use the correct collection name
        await usersCollection.InsertOneAsync(newUser);
        return CreatedAtAction(nameof(GetUserByName), new { name = newUser.Name }, new { newUser.Id, newUser.Name, newUser.Email });
    }
	*/
}
