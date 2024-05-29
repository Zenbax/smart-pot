using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.DTOs;

public class UpdatePlantRequestDto
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string? NameOfPlant { get; set; }
    public int? SoilMinimumMoisture { get; set; }
    public double AmountOfWaterToBeGiven { get; set; }
    public string? Image { get; set; }

    
    
    public UpdatePlantRequestDto()
    {
    }
    
    public UpdatePlantRequestDto(string id,string nameOfPlant, int soilMinimumMoisture, string image)
    {
        Id = id;
        NameOfPlant = nameOfPlant;
        SoilMinimumMoisture = soilMinimumMoisture;
        Image = image;
    }
}