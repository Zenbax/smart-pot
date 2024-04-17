using Domain;
using Domain.Model;

namespace Cloud.Services;

public interface IAuthService
{
    Task<User> ValidateUser(string username, string password);
}