using System.ComponentModel.DataAnnotations;
using Domain.Model;

namespace Domain.DTOs;

public class PotCreationDto
{
    [Required(ErrorMessage = "Pot name is required")]
    public string PotName { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    public string Email { get; set; }
    
    public string MachineID { get; set; }
}