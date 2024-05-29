using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs;

public class LoginRequestDto
{
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }

    public LoginRequestDto(string email, string password)
    {
        Email = email;
        Password = password;
    }
}