using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace YourApiNamespace.Controllers;

public class Pot
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string Name { get; set; }
    public string PlantType { get; set; }
    public int HumidityPercent { get; set; }
    
    public Pot()
    {
    }
    public Pot(string id, string name, string plantType, int humidityPercent)
    {
        Id = id;
        Name = name;
        PlantType = plantType;
        HumidityPercent = humidityPercent;
    }
}