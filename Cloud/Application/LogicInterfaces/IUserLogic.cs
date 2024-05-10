using Domain.DTOs;
using Domain.Model;

namespace Application_.LogicInterfaces;

public interface IUserLogic
{
    Task<UserGetByIdDto> GetUserById(UserGetByIdDto userGetByIdDto);
    Task<UserGetAllDto> GetUsers();
    Task<UserUpdateDto> UpdateUser(UserUpdateDto userUpdateDto);
}