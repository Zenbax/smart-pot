using Application_.LogicInterfaces;
using Domain.DTOs;
using MongoDB.Bson;
using MongoDB.Driver;
using YourApiNamespace.Controllers;

namespace Application_.Logic;

public class PotLogic : IPotLogic
{
    private readonly IMongoCollection<Pot> _pots;

    public PotLogic(IMongoCollection<Pot> potsCollection)
    {
        _pots = potsCollection;
    }

    public IEnumerable<Pot> GetAllPots()
    {
        return _pots.Find(p => true).ToList();
    }

    public Pot GetPotById(string id)
    {
        return _pots.Find(p => p.Id == id).FirstOrDefault();
    }

    public void CreatePot(PotCreationDto potDto)
    {
        var newPot = new Pot
        {
            Id = ObjectId.GenerateNewId().ToString(),
            Name = potDto.Name,
            PlantType = potDto.PlantType,
            HumidityPercent = potDto.HumidityPercent
        };
        _pots.InsertOne(newPot);
    }

    public void UpdatePot(string id, PotCreationDto updatedPotDto)
    {
        _pots.FindOneAndUpdate(p => p.Id == id, Builders<Pot>.Update
            .Set(p => p.Name, updatedPotDto.Name)
            .Set(p => p.PlantType, updatedPotDto.PlantType)
            .Set(p => p.HumidityPercent, updatedPotDto.HumidityPercent));
    }


    public void DeletePot(string id)
    {
        _pots.FindOneAndDelete(p => p.Id == id);
    }
    
}