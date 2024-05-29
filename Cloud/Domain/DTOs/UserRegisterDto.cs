using System.ComponentModel.DataAnnotations;
using Domain.Model;

namespace Domain.DTOs;

public class UserRegisterDto
{
    public UserRegisterDto(User user)
    {
        User = user;
    }
    public UserRegisterDto()
    {
    }

    [Required] public User User { get; set; }
    public string Message { get; set; }
    public bool Success { get; set; }
}