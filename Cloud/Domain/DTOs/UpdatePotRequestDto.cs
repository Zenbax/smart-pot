using System.ComponentModel.DataAnnotations;
using Cloud.Services;
using Domain.Model;

namespace Domain.DTOs;

public class UpdatePotRequestDto
{
    public string PotName { get; set; }
    
    public string Email { get; set; }

    [ZeroOrOne(ErrorMessage = "The Enable field must be either 0 or 1.")]
    public int Enable { get; set; }
    
    public string MachineID { get; set; }

    public Plant? Plant { get; set; }
}