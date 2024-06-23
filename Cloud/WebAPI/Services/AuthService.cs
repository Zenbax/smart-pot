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
using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using System.Text.Json;
using MongoDB.Bson;

// Assembly-attribut for at muliggøre, at Lambda-funktionens JSON-indgang kan konverteres til en .NET-klasse.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace Cloud.Services
{
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
            var user = userRegisterDto.User;
            user.Id = ObjectId.GenerateNewId().ToString(); // Generer en ny ObjectId for MongoDB

            users.Add(user);
//zzzzzzzzzzzzzzzzzz
            return new UserRegisterDto
            {
                User = user,
                Success = true,
                Message = "User registered successfully."
            };
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

        public async Task<APIGatewayProxyResponse> RegisterUserLambda(APIGatewayProxyRequest request)
        {
            var userRegisterDto = JsonSerializer.Deserialize<UserRegisterDto>(request.Body);
            var response = await RegisterUser(userRegisterDto);

            return new APIGatewayProxyResponse
            {
                StatusCode = 200,
                Body = JsonSerializer.Serialize(response),
                Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
            };
        }
    }
}
