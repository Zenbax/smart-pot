using Domain.DTOs;
using Domain.Model;

namespace Application_.LogicInterfaces;

public interface IAuthLogic
{
    Task<UserLoginDto> Login(UserLoginDto userLoginDto);
    Task<UserRegisterDto> Register(UserRegisterDto userRegisterDto);
}