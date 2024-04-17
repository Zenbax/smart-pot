namespace YourApiNamespace.Controllers;

public class Pot
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string Name { get; set; }
    public PotType Type { get; set; }
    public int HumidityPercent { get; set; }
    
    public Pot()
    {
    }
    public Pot(string id, string name, PotType type, int humidityPercent)
    {
        Id = id;
        Name = name;
        Type = type;
        HumidityPercent = humidityPercent;
    }
}