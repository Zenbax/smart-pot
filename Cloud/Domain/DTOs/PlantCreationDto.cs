using System.ComponentModel.DataAnnotations;
using Domain.Model;

namespace Domain.DTOs;

public class PlantCreationDto
{
    public PlantCreationDto(Plant plant)
    {
        Plant = plant;
    }

    [Required] public Plant Plant { get; set; }
    public string Message { get; set; }
    public bool Success { get; set; }
}