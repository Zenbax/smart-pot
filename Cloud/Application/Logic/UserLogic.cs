﻿// UserLogic.cs
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Model;
using MongoDB.Driver;
using MongoDB.Bson;


namespace Application.Logic
{
    public class UserLogic : IUserLogic
    {
        private readonly IMongoCollection<User> _usersCollection;

        public UserLogic(IMongoCollection<User> usersCollection)
        {
            _usersCollection = usersCollection;
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
            var newUser = new User
            {
                Id = ObjectId.GenerateNewId().ToString(), // Genererer en ny ObjectId som id
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
    
}