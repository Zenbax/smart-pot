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
        
        
        private async Task<string> ValidateUser(UserCreationDto user)
        {
            List<string> errors = new List<string>();

            // Validate Name: not empty and no digits
            if (string.IsNullOrWhiteSpace(user.Name))
                errors.Add("Name must not be empty.");
            if (user.Name.Any(char.IsDigit))
                errors.Add("Name must not contain numbers.");

            // Validate LastName: not empty and no digits
            if (string.IsNullOrWhiteSpace(user.LastName))
                errors.Add("Last name must not be empty.");
            if (user.LastName.Any(char.IsDigit))
                errors.Add("Last name must not contain numbers.");

            // Validate Email: contains "@" and ".", and not empty
            if (string.IsNullOrWhiteSpace(user.Email))
                errors.Add("Email must not be empty.");
            if (!user.Email.Contains("@") || !user.Email.Contains("."))
                errors.Add("Email must contain '@' and '.'.");

            // Check if email already exists in the database
            if (await _usersCollection.Find(u => u.Email == user.Email).AnyAsync())
                errors.Add("Email already exists.");

            // Validate Password: not empty, and length between 8 and 12
            if (string.IsNullOrWhiteSpace(user.Password))
                errors.Add("Password must not be empty.");
            if (user.Password.Length < 8 || user.Password.Length > 12)
                errors.Add("Password must be between 8 and 12 characters long.");

            // Validate PhoneNumber: exactly 8 digits, not empty, and only digits
            if (string.IsNullOrWhiteSpace(user.PhoneNumber))
                errors.Add("Phone number must not be empty.");
            if (!user.PhoneNumber.All(char.IsDigit) || user.PhoneNumber.Length != 8)
                errors.Add("Phone number must be exactly 8 digits and contain only numbers.");

            // Return all error messages concatenated into a single string, separated by semicolons
            return errors.Any() ? string.Join("; ", errors) : "Validation successful.";
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
            string validationResult = await ValidateUser(userCreationDto);
            if (validationResult != "Validation successful.")
                throw new ArgumentException(validationResult);

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