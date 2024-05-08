using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs;

public class RegisterRequestDto
{
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }

    [Required(ErrorMessage = "LastName is required")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }
    
    public string PhoneNumber { get; set; }
    
    public RegisterRequestDto(string name, string lastName, string email, string password, string phoneNumber)
    {
        Name = name;
        LastName = lastName;
        Email = email;
        Password = password;
        PhoneNumber = phoneNumber;
    }
}