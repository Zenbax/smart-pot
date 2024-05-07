using Application_.LogicInterfaces;
using Domain;
using Domain.DTOs;
using Domain.Model;

namespace Cloud.Services;

public class AuthService : IAuthService
{
    private readonly IList<User> users = new List<User> { };
    private readonly IUserLogic _userLogic;
    
    public AuthService(IUserLogic userLogic)
    {
        _userLogic = userLogic;
    }
    
    public async Task<User> ValidateUser(string username, string password)
    {
        return users.FirstOrDefault(u => u.Email == username && u.Password == password);
    }

    public async Task<User> RegisterUser(User user)
    {
        var userCreationDto = new UserCreationDto
        {
            Email = user.Email,
            Password = user.Password,
            Name = user.Name,
            LastName = user.LastName,
            PhoneNumber = user.PhoneNumber
        };

        var userId = await _userLogic.Register(userCreationDto);
        user.Id = userId;

        return user;
    }

    public Task<User> LoginUser(string email, string password)
    {
        return Task.FromResult(users.FirstOrDefault(u => u.Email == email && u.Password == password));
    }
}