using Domain;
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
}