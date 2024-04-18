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
        
        
        private bool ValidateUser(UserCreationDto user)
        {
            // Validate Name and LastName: not empty and no digits
            bool isValidName = !string.IsNullOrWhiteSpace(user.Name) && !user.Name.Any(char.IsDigit);
            bool isValidLastName = !string.IsNullOrWhiteSpace(user.LastName) && !user.LastName.Any(char.IsDigit);

            // Validate Email: contains "@" and ".", and not empty
            bool isValidEmail = !string.IsNullOrWhiteSpace(user.Email) && user.Email.Contains("@") && user.Email.Contains(".");

            // Validate Password: not empty, and length between 8 and 12
            bool isValidPassword = !string.IsNullOrWhiteSpace(user.Password) && user.Password.Length >= 8 && user.Password.Length <= 12;

            // Validate PhoneNumber: exactly 8 digits, not empty, and only digits
            bool isValidPhone = !string.IsNullOrWhiteSpace(user.PhoneNumber) && user.PhoneNumber.All(char.IsDigit) && user.PhoneNumber.Length == 8;

            return isValidName && isValidLastName && isValidEmail && isValidPassword && isValidPhone;
        }

        public async Task<string> Login(LoginDto userLoginDto)
        {
            var user = await _usersCollection.Find(u => u.Email == userLoginDto.Username && u.Password == userLoginDto.Password).FirstOrDefaultAsync();
            if (user != null)
            {
                // Implement your token generation logic here
                // For now, we are just returning a simple GUID as token
                return Guid.NewGuid().ToString();
            }
            return null;
        }

        public async Task<string> Register(UserCreationDto userCreationDto)
        {
            if (!ValidateUser(userCreationDto))
            {
                throw new ArgumentException("User data is invalid.");
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