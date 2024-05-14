using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Model;

public class SensorData
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public string MachineID { get; set; }
    public int WaterTankLevel { get; set; }
    public int MeasuredSoilMoisture { get; set; }
    public int AmountOfWatering { get; set; }
    public string PotId { get; set; }
    public string PlantId { get; set; }
    
    public SensorData() {}
}