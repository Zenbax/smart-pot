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


    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var usersCollection = _database.GetCollection<User>("Users");
        var users = await usersCollection.Find(_ => true).ToListAsync();
        return Ok(users);
    }


    [HttpPost]
    public IActionResult PostUser([FromBody] string text)
    {
        if (text == null)
        {
            return BadRequest("User is null.");
        }

        // Here you would typically parse the text to a User object and insert it into the database
        return Ok();
    }
}
