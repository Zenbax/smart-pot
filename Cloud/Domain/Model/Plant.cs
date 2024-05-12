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
    public int WaterTankLevel { get; set; }
    public string ImageURL { get; set; } = string.Empty;
    public bool Active { get; set; }
    public string PotId { get; set; }

    
    
    public Plant()
    {
    }
    
    public Plant(string id,string nameOfPlant, int soilMinimumMoisture, string ImageURL, string potId)
    {
        Id = id;
        NameOfPlant = nameOfPlant;
        SoilMinimumMoisture = soilMinimumMoisture;
        ImageURL = ImageURL;
        PotId = potId;
    }
}