using System.ComponentModel.DataAnnotations;
using Domain.Model;

namespace Domain.DTOs;

public class UserLoginDto
{
    public UserLoginDto(User user)
    {
        User = user;
    }

    public UserLoginDto()
    {
    }

    [Required]
    public User User { get; set; }
    
    public string Token { get; set; }
    
    public string RefreshToken { get; set; }
    public string Message { get; set; }
    public bool Success { get; set; }
    
}