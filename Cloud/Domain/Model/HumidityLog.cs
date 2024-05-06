using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace Domain.Model
{
    public class HumidityLog
    {
        public int Id { get; set; }
        public int PotId { get; set; }
        public double HumidityPercentage { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
