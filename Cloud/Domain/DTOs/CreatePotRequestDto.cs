using System.ComponentModel.DataAnnotations;
using Domain.Model;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.DTOs;

public class CreatePotRequestDto
{
    [Required(ErrorMessage = "Pot name is required")]
    public string PotName { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    public string Email { get; set; }
    
    public string MachineID { get; set; }

    [Required]
    public bool Enable { get; set; }
    public Plant? Plant { get; set; }
    
    
    public CreatePotRequestDto()
    {
    }
}