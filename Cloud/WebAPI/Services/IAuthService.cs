using System.Threading.Tasks;
using Domain;
using Domain.DTOs;
using Domain.Model;

namespace Cloud.Services;

public interface IAuthService
{
    Task<User> ValidateUser(string username, string password);
    Task<UserRegisterDto> RegisterUser(UserRegisterDto userRegisterDto);
    Task<UserLoginDto> LoginUser(UserLoginDto userLoginDto);
    string GenerateJwtToken(User user);
    string GenerateRefreshToken();
}