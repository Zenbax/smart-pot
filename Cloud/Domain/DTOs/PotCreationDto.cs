using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs;

public class PotCreationDto
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
    // [Required(ErrorMessage = "Pot name is required")]
    // public string NameOfpot { get; set; }
    //
    // [Required(ErrorMessage = "Plant information is required")]
    // public PlantDto Plante { get; set; }
    //
    // [Range(0, 100, ErrorMessage = "Soil moisture percentage must be between 0 and 100")]
    // public int JordFugtighedProcent { get; set; }
    //
    // [Range(0, 100, ErrorMessage = "Water reservoir percentage must be between 0 and 100")]
    // public int VandBeholderProcent { get; set; }
    //
    // public List<WateringLogDto> VandingsLog { get; set; }
    // public List<HumidityLogDto> Fugtighedslog { get; set; }
