using Application.LogicInterfaces;
using Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

using MongoDB.Driver;
using Domain.Model;

namespace WebAPI.ControllerFrontEnd
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserLogic _userLogic;

        public UserController(IUserLogic userLogic)
        {
            _userLogic = userLogic;
        }
        

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userLogic.GetUsers();
            return Ok(users);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserCreationDto newUser)
        {
            await _userLogic.Register(newUser);
            return CreatedAtAction(nameof(GetUserById), new { id = newUser.Id }, newUser);  
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _userLogic.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
    }
}