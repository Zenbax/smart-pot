using Application_.LogicInterfaces;
using Domain.DTOs;
using Domain.Model;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Application_.Logic;

public class UserLogic : IUserLogic
    {
        private readonly IMongoCollection<User> _usersCollection;
        

        public UserLogic(IMongoCollection<User> usersCollection)
        {
            _usersCollection = usersCollection;
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