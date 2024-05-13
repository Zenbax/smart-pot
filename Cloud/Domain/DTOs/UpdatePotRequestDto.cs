using System.ComponentModel.DataAnnotations;
using Domain.Model;

namespace Domain.DTOs;

public class UpdatePotRequestDto
{
    public string PotName { get; set; }
    
    public string Email { get; set; }

    public bool Enable { get; set; }

    public Plant? Plant { get; set; }
}