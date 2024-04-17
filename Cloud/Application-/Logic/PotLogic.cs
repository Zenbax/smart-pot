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

    public async Task<IEnumerable<Pot>> GetAllPots()
    {
        return await _pots.Find(p => true).ToListAsync();
    }

    public async Task<Pot> GetPotById(string id)
    {
        return await _pots.Find(p => p.Id == id).FirstOrDefaultAsync();
    }

    public async Task<string> CreatePot(PotCreationDto potDto)
    {
        var newPot = new Pot
        {
            Id = ObjectId.GenerateNewId().ToString(),
            Name = potDto.Name,
            PlantType = potDto.PlantType,
            HumidityPercent = potDto.HumidityPercent
        };
        await _pots.InsertOneAsync(newPot);
        return newPot.Id;
    }

    public async Task<string> UpdatePot(string id, PotCreationDto updatedPotDto)
    {
        var result = await _pots.UpdateOneAsync(
            p => p.Id == id,
            Builders<Pot>.Update
                .Set(p => p.Name, updatedPotDto.Name)
                .Set(p => p.PlantType, updatedPotDto.PlantType)
                .Set(p => p.HumidityPercent, updatedPotDto.HumidityPercent)
        );

        return result.IsAcknowledged ? "Success" : "Failure";
    }

    public async Task<string> DeletePot(string id)
    {
        var result = await _pots.DeleteOneAsync(p => p.Id == id);
        return result.IsAcknowledged ? "Success" : "Failure";
    }
}