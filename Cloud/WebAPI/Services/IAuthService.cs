using Domain;
using Domain.Model;

namespace Cloud.Services;

public interface IAuthService
{
    Task<User> ValidateUser(string username, string password);
    Task<User> RegisterUser(User user);
    Task<User> LoginUser(string email, string password);
}