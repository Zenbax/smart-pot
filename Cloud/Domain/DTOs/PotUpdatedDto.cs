using System.ComponentModel.DataAnnotations;
using Domain.Model;

namespace Domain.DTOs;

public class PotUpdatedDto
{
    [Required(ErrorMessage = "Pot name is required")]
    public string PotName { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    public string Email { get; set; }

    [Required]
    public bool Enable { get; set; }

    [Required(ErrorMessage = "Plant details are required")]
    public string PlantId { get; set; }
}