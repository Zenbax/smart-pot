using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs;

public class UserCreationDto
{
    [Required(ErrorMessage = "Name is required.")]
    [StringLength(20, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 20 characters long.")]
    [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Name must contain only letters.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Last name is required.")]
    [StringLength(20, MinimumLength = 2, ErrorMessage = "Last name must be between 2 and 20 characters long.")]
    [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Last name must contain only letters.")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Email is required.")]
    [StringLength(40, MinimumLength = 8, ErrorMessage = "Email must be between 8 and 40 characters long.")]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    [StringLength(12, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 12 characters long.")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Phone number is required.")]
    [RegularExpression("^[0-9]{8}$", ErrorMessage = "Phone number must be exactly 8 digits and contain only numbers.")]
    public string PhoneNumber { get; set; }
}