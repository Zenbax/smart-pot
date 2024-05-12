using System.ComponentModel.DataAnnotations;
using Domain.Model;

namespace Domain.DTOs;

public class PotUpdatedDto
{
    [Required(ErrorMessage = "Pot name is required")]
    public string PotName { get; set; }

    [Required]
    public bool Enable { get; set; }

}