using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.DTOs
{
    public class CreatePlantRequestDto
    {
        [Required(ErrorMessage = "Plant name is required")]
        public string NameOfPlant { get; set; }

        [Range(0, 100, ErrorMessage = "Soil minimum moisture must be between 0 and 100")]
        public int SoilMinimumMoisture { get; set; }

        [Required(ErrorMessage = "Image URL is required")]
        public string ImageUrl { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Water ML must be a non-negative number")]
        public int AmountOfWaterToBeGiven { get; set; }
        
        public bool Enable { get; set; }
    }
}