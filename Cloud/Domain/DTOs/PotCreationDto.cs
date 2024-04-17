using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs;

public class PotCreationDto
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string PlantType { get; set; }
    [Range(0, 100)]
    public int HumidityPercent { get; set; }
}
