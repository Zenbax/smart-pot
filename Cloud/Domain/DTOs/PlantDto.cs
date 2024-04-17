using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs;

public class PlantDto
{
    [Required(ErrorMessage = "Plant name is required")]
    public string NavnPåPlante { get; set; }

    [Range(0, 100, ErrorMessage = "Soil minimum moisture must be between 0 and 100")]
    public int JordMinimumsFugtighed { get; set; }

    [Required(ErrorMessage = "Image URL is required")]
    public string BilledeURL { get; set; }
}