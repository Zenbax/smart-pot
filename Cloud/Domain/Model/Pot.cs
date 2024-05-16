using Cloud.Services;
using Domain.Model;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace YourApiNamespace.Controllers;

public class Pot
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string UserId { get; set; }


    public string NameOfPot { get; set; }
    public string Email { get; set; }
    
    [ZeroOrOne(ErrorMessage = "The Enable field must be either 0 or 1.")]
    public int Enable { get; set; }
    
    public string MachineID { get; set; }
    public Plant Plant { get; set; }
    public List<SensorData> SensorData { get; set; }

    public Pot() {}

    public Pot(string nameOfPot, string email, int enable, string machineId, Plant plant)
    {
        NameOfPot = nameOfPot;
        Email = email;
        Enable = enable;
        MachineID = machineId;
        Plant = plant;
    }
}