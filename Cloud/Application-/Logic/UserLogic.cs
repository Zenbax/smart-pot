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

    // Validate Name
    if (string.IsNullOrWhiteSpace(user.Name))
        errors.Add("Name must not be empty.");
    else if (user.Name.Any(char.IsDigit) || user.Name.Any(ch => !char.IsLetter(ch)))
        errors.Add("Name must only contain letters and must not contain numbers or special characters.");
    else if (user.Name.Length < 2 || user.Name.Length > 20)
        errors.Add("Name must be between 2 and 20 characters long.");

    // Validate LastName
    if (string.IsNullOrWhiteSpace(user.LastName))
        errors.Add("Last name must not be empty.");
    else if (user.LastName.Any(char.IsDigit) || user.LastName.Any(ch => !char.IsLetter(ch)))
        errors.Add("Last name must only contain letters and must not contain numbers or special characters.");
    else if (user.LastName.Length < 2 || user.LastName.Length > 20)
        errors.Add("Last name must be between 2 and 20 characters long.");

    // Validate Email
    if (string.IsNullOrWhiteSpace(user.Email))
        errors.Add("Email must not be empty.");
    else if (user.Email.Length < 8 || user.Email.Length > 40)
        errors.Add("Email must be between 8 and 40 characters long.");
    else if (!user.Email.Contains("@") || !user.Email.Contains("."))
        errors.Add("Email must contain '@' and '.'.");
    else
    {
        int atIndex = user.Email.IndexOf('@');
        int dotIndex = user.Email.IndexOf('.', atIndex);
        if (dotIndex == -1 || dotIndex == atIndex + 1)  // Check if '.' comes after '@' and not immediately after
            errors.Add("Email format is incorrect; '.' must come after '@' with characters in between.");
    }

    // Check if email already exists in the database
    if (await _usersCollection.Find(u => u.Email == user.Email).AnyAsync())
        errors.Add("Email already exists.");

    // Validate Password
    if (string.IsNullOrWhiteSpace(user.Password))
        errors.Add("Password must not be empty.");
    else if (user.Password.Length < 8 || user.Password.Length > 12)
        errors.Add("Password must be between 8 and 12 characters long.");

    // Validate PhoneNumber
    if (string.IsNullOrWhiteSpace(user.PhoneNumber))
        errors.Add("Phone number must not be empty.");
    else if (!user.PhoneNumber.All(char.IsDigit) || user.PhoneNumber.Length != 8)
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