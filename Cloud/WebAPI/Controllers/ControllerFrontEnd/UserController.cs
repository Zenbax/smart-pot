using Microsoft.AspNetCore.Mvc;

namespace Cloud.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    [HttpPost]
    public IActionResult PostUser([FromBody] String text)
    {
        if (text == null)
        {
            return BadRequest("User is null.");
        }

        return Ok();
    }
}