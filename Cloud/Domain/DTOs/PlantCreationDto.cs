using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs;

public class PlantCreationDto
{
    [Required(ErrorMessage = "Plant name is required")]
    public string NameOfPlant { get; set; }

    [Range(0, 100, ErrorMessage = "Soil minimum moisture must be between 0 and 100")]
    public int SoilMinimumMoisture { get; set; }


    [Required(ErrorMessage = "Image URL is required")]
    public string? ImageURL { get; set; }
    
    [Range(0, int.MaxValue, ErrorMessage = "Water ML must be a non-negative number")]
    public int WaterTankLevel { get; set; }
}