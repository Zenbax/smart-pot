using Microsoft.AspNetCore.Mvc;

namespace Cloud.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    [HttpPost]
    public IActionResult PostUser([FromBody] User user)
    {
        if (user == null)
        {
            return BadRequest("User is null.");
        }
        return CreatedAtAction(nameof(PostUser), new { id = user.Id }, user);
    }
}