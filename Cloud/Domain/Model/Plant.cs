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
    public string? ImageUrl { get; set; }
    public bool Enable { get; set; }
    public double? AmountOfWaterToBeGiven { get; set; }  // Ny tilføjelse til at håndtere vandingsmængde
    


    
    
    public Plant()
    {
    }
    
    public Plant(string id,string nameOfPlant, int soilMinimumMoisture, string imageUrl, double amountOfWaterToBeGiven, bool enabale)
    {
        Id = id;
        NameOfPlant = nameOfPlant;
        SoilMinimumMoisture = soilMinimumMoisture;
        ImageUrl = imageUrl;
        AmountOfWaterToBeGiven = amountOfWaterToBeGiven;
        Enable = enabale;
    }
    
    
  //Det er en to string metode som er lavet for at kunne se hvad der er i plant objektet
    public override string ToString()
    {
        return $"Id: {Id}, NameOfPlant: {NameOfPlant}, SoilMinimumMoisture: {SoilMinimumMoisture}, ImageUrl: {ImageUrl}, AmountOfWaterToBeGiven: {AmountOfWaterToBeGiven}";
    }
}