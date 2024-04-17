using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs;

public class PotUpdatedDto
{
    [Required(ErrorMessage = "Pot name is required")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Plant name is required")]
    public string NameOfPlant { get; set; }

    [Required(ErrorMessage = "Soil minimum moisture is required")]
    [Range(0, 100, ErrorMessage = "Soil minimum moisture must be between 0 and 100")]
    public int SoilMinimumMoisture { get; set; }

    [Required(ErrorMessage = "Image URL is required")]
    public string ImageUrl { get; set; }

    [Required(ErrorMessage = "Humidity percent is required")]
    [Range(0, 100, ErrorMessage = "Humidity percent must be between 0 and 100")]
    public int HumidityPercent { get; set; }
}