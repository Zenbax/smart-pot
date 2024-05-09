using Domain.DTOs;
using Domain.Model;

namespace Application_.LogicInterfaces;

public interface IUserLogic
{
    Task<User> GetUserById(string id);
    Task<IEnumerable<User>> GetUsers();
    Task<UserUpdateDto> UpdateUser(UserUpdateDto userUpdateDto);
}