using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Model;

public class HumidityLog
{
    [BsonRepresentation(BsonType.ObjectId)]// This is needed for MongoDB to generate an ID
    public string Id { get; set; }

    public DateTime TimeStamp { get; set; }
    public int MoisturePercent { get; set; }
    public int WateringAmountML { get; set; }
}