using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Model;

public class Plant
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string NameOfPlant { get; set; }
    public int SoilMinimumMoisture { get; set; }
    public string ImageUrl { get; set; }
    
    public string Size { get; set; }
    
    public string PlantType { get; set; }
    
    public HumidityLog HumidityLog { get; set; }
    
    public Plant()
    {
    }
    
    public Plant(string id,string nameOfPlant, int soilMinimumMoisture, string imageUrl, string size, string plantType, HumidityLog humidityLog)
    {
        Id = id;
        NameOfPlant = nameOfPlant;
        SoilMinimumMoisture = soilMinimumMoisture;
        ImageUrl = imageUrl;
        Size = size;
        PlantType = plantType;
        HumidityLog = humidityLog;
    }
}