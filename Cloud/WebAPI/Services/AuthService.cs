using Domain;
using Domain.DTOs;
using Domain.Model;

namespace Cloud.Services;

public class AuthService : IAuthService
{
    private readonly IList<User> users = new List<User>
        { };
    
    public async Task<User> ValidateUser(string username, string password)
    {
        return users.FirstOrDefault(u => u.Email == username && u.Password == password);
    }

    public Task<User> RegisterUser(User user)
    {
        users.Add(user);
        return Task.FromResult(user);
    }

    public Task<User> LoginUser(string email, string password)
    {
        return Task.FromResult(users.FirstOrDefault(u => u.Email == email && u.Password == password));
    }
}