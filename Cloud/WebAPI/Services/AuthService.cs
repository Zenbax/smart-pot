using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Application_.LogicInterfaces;
using Domain;
using Domain.DTOs;
using Domain.Model;
using Microsoft.IdentityModel.Tokens;

namespace Cloud.Services;

public class AuthService : IAuthService
{
    private readonly IList<User> users = new List<User> { };
    private readonly IAuthLogic _userAuth;
    private readonly IConfiguration _configuration;
    
    public AuthService(IAuthLogic authLogic, IConfiguration configuration)
    {
        _userAuth = authLogic;
        _configuration = configuration;
    }
    
    public async Task<User> ValidateUser(string username, string password)
    {
        return users.FirstOrDefault(u => u.Email == username && u.Password == password);
    }

    public async Task<UserRegisterDto> RegisterUser(RegisterRequestDto registerRequestDto)
    {
        var user = new User
        {
            Name = registerRequestDto.Name,
            LastName = registerRequestDto.LastName,
            Email = registerRequestDto.Email,
            Password = registerRequestDto.Password,
            PhoneNumber = registerRequestDto.PhoneNumber
        };
        var userRegisterDto = new UserRegisterDto(user);

        return await _userAuth.Register(userRegisterDto);
    }

    public Task<UserLoginDto> LoginUser(UserLoginDto userLoginDto)
    {
        throw new NotImplementedException();
    }

    public async Task<UserLoginDto> LoginUser(LoginRequestDto loginRequestDto)
    {
        User user = new User { Email = loginRequestDto.Email, Password = loginRequestDto.Password };
        var userLoginDto = new UserLoginDto(user);
        if (loginRequestDto.Email == null || loginRequestDto.Password == null)
        {
            userLoginDto.Message = "Email or password is missing.";
            userLoginDto.Success = false;
            return userLoginDto;
        }
        Console.WriteLine(user.ToString());
        userLoginDto.Token = GenerateJwtToken(user);
        userLoginDto.RefreshToken = GenerateRefreshToken();
        var response = await _userAuth.Login(userLoginDto);

        return response;
    }
    
    public string GenerateJwtToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Secret"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Email, user.Email)
            }),
            Expires = DateTime.UtcNow.AddHours(Convert.ToDouble(_configuration["Jwt:ExpireHours"])).AddMinutes(1), // Add an extra minute
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
    
    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}