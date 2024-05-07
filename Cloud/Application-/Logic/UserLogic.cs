using Application_.LogicInterfaces;
using Domain.DTOs;
using Domain.Model;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using LoggerExtensions = DnsClient.Internal.LoggerExtensions;

namespace Application_.Logic;

public class UserLogic : IUserLogic
    {
        private readonly IMongoCollection<User> _usersCollection;
        private readonly ILogger<UserLogic> _logger;

        public UserLogic(IMongoCollection<User> usersCollection, ILogger<UserLogic> logger)
        {
            _usersCollection = usersCollection;
            _logger = logger;
        }
        
        public async Task<string> Login(LoginDto userLoginDto)
        {
            var user = await _usersCollection.Find(u => u.Email == userLoginDto.Email && u.Password == userLoginDto.Password).FirstOrDefaultAsync();
            if (user == null)
            {
                throw new ArgumentException("Invalid email or password.");
            }

            return user.Id; 
        }

        public async Task<string> Register(UserCreationDto userCreationDto)
        {
            _logger.LogInformation("Called register in application logic.");
            Console.WriteLine("Called register in application logic.");
            var emailExists = await _usersCollection.Find(u => u.Email == userCreationDto.Email).AnyAsync();
            if (emailExists)
            {
                throw new ArgumentException("Email already exists.");
            }
            
           var newUser = new User
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Name = userCreationDto.Name,
                LastName = userCreationDto.LastName,
                Email = userCreationDto.Email,
                Password = userCreationDto.Password,
                PhoneNumber = userCreationDto.PhoneNumber
            };

            await _usersCollection.InsertOneAsync(newUser);
            Console.WriteLine("User registered successfully with id: " + newUser.Id);
            return newUser.Id;
        }

        public async Task<User> GetUserById(string id)
        {
            return await _usersCollection.Find(user => user.Id == id).FirstOrDefaultAsync();
        }
        
        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _usersCollection.Find(user => true).ToListAsync();
        }
}