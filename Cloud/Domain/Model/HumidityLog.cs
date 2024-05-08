using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace Domain.Model
{
    public class HumidityLog
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string PotId { get; set; }

        public double HumidityPercentage { get; set; } = 0;
        public DateTime Timestamp { get; set; }
    }
}
