using Domain.DTOs;
using Domain.Model;

namespace Application.LogicInterfaces;

public interface IUserLogic
{
	
    Task<string> Login(LoginDto userLoginDto);
    Task<string> Register(UserCreationDto userCreationDto);
    Task<User> GetUserById(string id);
	Task<IEnumerable<User>> GetUsers();
}