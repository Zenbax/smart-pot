using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Application_.LogicInterfaces;
using Domain;
using Domain.DTOs;
using Domain.Model;
using Microsoft.Extensions.Configuration;
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

    public async Task<UserRegisterDto> RegisterUser(UserRegisterDto userRegisterDto)
    {
        return await _userAuth.Register(userRegisterDto);
    }

    public async Task<UserLoginDto> LoginUser(UserLoginDto userLoginDto)
    {
        if (userLoginDto.User.Email == null || userLoginDto.User.Password == null)
        {
            userLoginDto.Message = "Email or password is missing.";
            userLoginDto.Success = false;
            return userLoginDto;
        }
        userLoginDto.Token = GenerateJwtToken(userLoginDto.User);
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