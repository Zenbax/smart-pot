using Domain.Model;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace YourApiNamespace.Controllers;

public class Pot
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public string NameOfPot { get; set; }
    public Plant Plant { get; set; }
    public int SoilMoisturePercent { get; set; }
    public int WaterReservoirPercent { get; set; }
    public List<HumidityLog> WateringLog { get; set; }
    public List<HumidityLog> MoistureLog { get; set; }
    
    public Pot()
    {
    }
    public Pot(string nameOfPot, Plant plant, int soilMoisturePercent, int waterReservoirPercent, List<HumidityLog> wateringLog, List<HumidityLog> moistureLog)
    {
        NameOfPot = nameOfPot;
        Plant = plant;
        SoilMoisturePercent = soilMoisturePercent;
        WaterReservoirPercent = waterReservoirPercent;
        WateringLog = wateringLog;
        MoistureLog = moistureLog;
    }
}