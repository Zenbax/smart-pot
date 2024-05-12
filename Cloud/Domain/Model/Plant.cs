using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Model;

public class Plant
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
<<<<<<< HEAD
    public string? Id { get; set; }
    public string? NameOfPlant { get; set; }
    public int? SoilMinimumMoisture { get; set; }
    public int? WaterTankLevel { get; set; }
    public string? ImageUrl { get; set; }
=======
    public string Id { get; set; }
    public string NameOfPlant { get; set; }
    public int SoilMinimumMoisture { get; set; }
    public int WaterTankLevel { get; set; }
    public string ImageURL { get; set; } = string.Empty;
    public bool Active { get; set; }
    public string PotId { get; set; }
>>>>>>> Artesh_Branchh

    
    
    public Plant()
    {
    }
    
<<<<<<< HEAD
    public Plant(string id,string nameOfPlant, int soilMinimumMoisture, int waterTankLevel, string imageUrl)
=======
    public Plant(string id,string nameOfPlant, int soilMinimumMoisture, string ImageURL, string potId)
>>>>>>> Artesh_Branchh
    {
        Id = id;
        NameOfPlant = nameOfPlant;
        SoilMinimumMoisture = soilMinimumMoisture;
<<<<<<< HEAD
        WaterTankLevel = waterTankLevel;
        ImageUrl = imageUrl;
=======
        ImageURL = ImageURL;
        PotId = potId;
>>>>>>> Artesh_Branchh
    }
    
    public string ToString()
    {
        return $"Id: {Id}, NameOfPlant: {NameOfPlant}, SoilMinimumMoisture: {SoilMinimumMoisture}, WaterTankLevel: {WaterTankLevel}, ImageUrl: {ImageUrl}";
    }
}