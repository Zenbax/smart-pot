using Domain;
using Domain.DTOs;
using Domain.Model;

namespace Cloud.Services;

public interface IAuthService
{
    Task<User> ValidateUser(string username, string password);
    Task<UserRegisterDto> RegisterUser(RegisterRequestDto userRegisterDto);
    Task<UserLoginDto> LoginUser(LoginRequestDto userLoginDto);
    string GenerateJwtToken(User user);
    string GenerateRefreshToken();
}