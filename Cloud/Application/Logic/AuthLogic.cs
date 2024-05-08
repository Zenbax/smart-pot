using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Application_.LogicInterfaces;
using Domain.DTOs;
using Domain.Model;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;
using MongoDB.Driver;
using LoggerExtensions = DnsClient.Internal.LoggerExtensions;

namespace Application_.Logic;

public class AuthLogic : IAuthLogic
    {
        private readonly IMongoCollection<User> _usersCollection;
        private readonly ILogger<AuthLogic> _logger;

        public AuthLogic(IMongoCollection<User> usersCollection, ILogger<AuthLogic> logger)
        {
            _usersCollection = usersCollection;
            _logger = logger;
        }
        
        public async Task<UserLoginDto> Login(UserLoginDto userLoginDto)
        {   
            var foundUser = await _usersCollection.Find(u => u.Email == userLoginDto.User.Email && u.Password == userLoginDto.User.Password).FirstOrDefaultAsync();
            if (foundUser == null)
            {
                userLoginDto.Message = "Invalid email or password.";
                userLoginDto.Success = false;
            }
            else
            {
                userLoginDto.Message = "User logged in successfully.";
                userLoginDto.Success = true;
                userLoginDto.User = foundUser;
            }

            return userLoginDto;
        }

        public async Task<UserRegisterDto> Register(UserRegisterDto userRegisterDto)
        {
            try
            {
                _logger.LogInformation("Called register in application logic.");
                Console.WriteLine("Called register in application logic.");
                var emailExists = await _usersCollection.Find(u => u.Email == userRegisterDto.User.Email).AnyAsync();
                if (emailExists)
                {
                    throw new ArgumentException("Email already exists.");
                }

                userRegisterDto.User.Id = ObjectId.GenerateNewId().ToString();

                await _usersCollection.InsertOneAsync(userRegisterDto.User);
                Console.WriteLine("User registered successfully with id: " + userRegisterDto.User.Id);
                userRegisterDto.Message = "User registered successfully with id: " + userRegisterDto.User.Id;
                userRegisterDto.Success = false;

                return userRegisterDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in register in application logic.");
                Console.WriteLine("Error in register in application logic.");
                userRegisterDto.Message = ex.Message;
                userRegisterDto.Success = false;
                return userRegisterDto;
            }
        }
}