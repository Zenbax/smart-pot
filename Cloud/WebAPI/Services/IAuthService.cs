using Domain;
using Domain.DTOs;
using Domain.Model;

namespace Cloud.Services;

public interface IAuthService
{
    Task<User> ValidateUser(string username, string password);
    Task<UserRegisterDto> RegisterUser(RegisterRequestDto registerRequestDto);
    Task<UserLoginDto> LoginUser(LoginRequestDto loginRequestDto);
    string GenerateJwtToken(User user);
    string GenerateRefreshToken();
}