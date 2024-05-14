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
    public string? Image { get; set; }
    public double? AmountOfWaterToBeGiven { get; set; }  // Ny tilføjelse til at håndtere vandingsmængde
    
    public string? UserId { get; set; }

    public bool isDefault { get; set; } = false;
    
    public Plant()
    {
    }
    
    public Plant(string id,string nameOfPlant, int soilMinimumMoisture, string image, double amountOfWaterToBeGiven, string userId, bool isDefault = false)
    {
        Id = id;
        NameOfPlant = nameOfPlant;
        SoilMinimumMoisture = soilMinimumMoisture;
        Image = image;
        AmountOfWaterToBeGiven = amountOfWaterToBeGiven;
        UserId = userId;
        this.isDefault = isDefault;
    }
    
    
    
    
  //Det er en to string metode som er lavet for at kunne se hvad der er i plant objektet
  public override string ToString()
  {
      return
          $"Id: {Id}, NameOfPlant: {NameOfPlant}, SoilMinimumMoisture: {SoilMinimumMoisture}, Image: {Image}, AmountOfWaterToBeGiven: {AmountOfWaterToBeGiven}, UserId: {UserId}";
  }
}