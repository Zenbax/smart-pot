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

        public async Task<User> GetUserById(string id)
        {
            return await _usersCollection.Find(user => user.Id == id).FirstOrDefaultAsync();
        }
        
        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _usersCollection.Find(user => true).ToListAsync();
        }

        public async Task<UserUpdateDto> UpdateUser(UserUpdateDto userUpdateDto)
        {
            var updatedUser = await _usersCollection.FindOneAndUpdateAsync(
                user => user.Id == userUpdateDto.IdToUpdate,
                Builders<User>.Update
                    .Set(user => user.Name, userUpdateDto.User.Name)
                    .Set(user => user.LastName, userUpdateDto.User.LastName)
                    .Set(user => user.Email, userUpdateDto.User.Email)
                    .Set(user => user.Password, userUpdateDto.User.Password)
                    .Set(user => user.PhoneNumber, userUpdateDto.User.PhoneNumber)
            );

            if (updatedUser == null)
            {
                userUpdateDto.Message = "User with id " + userUpdateDto.IdToUpdate + " not found.";
                userUpdateDto.Success = false;
                userUpdateDto.User = null;
            }
            else
            {
                userUpdateDto.Message = "User updated successfully.";
                userUpdateDto.Success = true;
            }
            return userUpdateDto;
        }
    }