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

        public async Task<UserGetByIdDto> GetUserById(UserGetByIdDto userGetByIdDto)
        {
            try
            {
                var user = await _usersCollection.Find(user => user.Id == userGetByIdDto.IdToGet).FirstOrDefaultAsync();
                if (user == null)
                {
                    userGetByIdDto.Message = "User with id " + userGetByIdDto.IdToGet + " not found.";
                    userGetByIdDto.Success = false;
                    userGetByIdDto.User = null;
                }
                else
                {
                    userGetByIdDto.Message = "User retrieved successfully.";
                    userGetByIdDto.Success = true;
                    userGetByIdDto.User = user;
                }
            }
            catch (Exception ex)
            {
                userGetByIdDto.Message = "Error: " + ex.Message;
                userGetByIdDto.Success = false;
                userGetByIdDto.User = null;
            }
            return userGetByIdDto;
        }
        
        public async Task<UserGetAllDto> GetUsers()
        {
            try 
            {
                var users = await _usersCollection.Find(user => true).ToListAsync();
                if (users.Count == 0)
                {
                    return new UserGetAllDto
                    {
                        Users = null,
                        Message = "No users in database.",
                        Success = true
                    };
                }
                return new UserGetAllDto
                {
                    Users = users,
                    Message = "Users retrieved successfully.",
                    Success = true
                };
            }
            catch (Exception ex)
            {
                return new UserGetAllDto
                {
                    Users = null,
                    Message = "Error: " + ex.Message,
                    Success = false
                };
            }
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