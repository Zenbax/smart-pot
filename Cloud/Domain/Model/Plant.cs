using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Model;

public class Plant
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string? NameOfPlant { get; set; }
    public int? SoilMinimumMoisture { get; set; }
    public int? WaterTankLevel { get; set; }
    public string? ImageUrl { get; set; }

    
    
    public Plant()
    {
    }
    
    public Plant(string id,string nameOfPlant, int soilMinimumMoisture, int waterTankLevel, string imageUrl)
    {
        Id = id;
        NameOfPlant = nameOfPlant;
        SoilMinimumMoisture = soilMinimumMoisture;
        WaterTankLevel = waterTankLevel;
        ImageUrl = imageUrl;
    }
    
    public string ToString()
    {
        return $"Id: {Id}, NameOfPlant: {NameOfPlant}, SoilMinimumMoisture: {SoilMinimumMoisture}, WaterTankLevel: {WaterTankLevel}, ImageUrl: {ImageUrl}";
    }
}