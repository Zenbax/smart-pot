using Microsoft.AspNetCore.Mvc;

namespace Cloud.Controllers;

[ApiController]
[Route("users")]
public class UserController : ControllerBase
{
    // create the router for the get method, make the route "get-all"
    [HttpGet("get-all")]
    public async Task<IActionResult> GetAll()
    {
        var users = "{ \"users\": [ { \"name\": \"John\" }, { \"name\": \"Doe\" } ] };";
        return Ok(users);
    }
}