using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs;

public class PlantCreationDto
{
    [Required(ErrorMessage = "Plant name is required")]
    public string NameOfPlant  { get; set; }

    [Range(0, 100, ErrorMessage = "Soil minimum moisture must be between 0 and 100")]
    public int SoilMinimumMoisture { get; set; }

    [Required(ErrorMessage = "Image URL is required")]
    public string ImageURL { get; set; }
    
    // public string Size { get; set; }
    //     
    // public string PlantType { get; set; }
    //
    // public HumidityLogDto  HumidityLogs { get; set; }
}