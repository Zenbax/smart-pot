using Application_.LogicInterfaces;
using Domain.DTOs;
using Domain.Model;
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
        try
        {
            return await _pots.Find(p => true).ToListAsync();
        }
        catch (Exception ex)
        {
            // Håndter eventuelle fejl
            return null;
        }
    }

    public async Task<Pot> GetPotById(string id)
    {
        try
        {
            return await _pots.Find(p => p.Id == id).FirstOrDefaultAsync();
        }
        catch (Exception ex)
        {
            // Håndter eventuelle fejl
            return null;
        }
    }

    public async Task<string> CreatePot(PotCreationDto potDto)
    {
        try
        {
            var plant = new Plant
            {
                NameOfPlant = potDto.NameOfPlant,
                SoilMinimumMoisture = potDto.SoilMinimumMoisture,
                ImageUrl = potDto.ImageUrl
            };

            var newPot = new Pot
            {
                NameOfPot = potDto.Name,
                Plant = plant,
                SoilMoisturePercent = potDto.HumidityPercent,
                WaterReservoirPercent = 0, // Initialiser vandbeholderprocenten til 0
                WateringLog = new List<HumidityLog>(), // Opret tomme logfiler
                MoistureLog = new List<HumidityLog>()
            };

            // Indsæt den nye Pot i databasen
            await _pots.InsertOneAsync(newPot);
        
            return "Success";
        }
        catch (Exception ex)
        {
            // Håndter eventuelle fejl
            return $"Error: {ex.Message}";
        }
    }

    public async Task<string> UpdatePot(string id, UpdatedPotDto updatedPotDto)
    {
        try
        {
            var existingPot = await _pots.Find(p => p.Id == id).FirstOrDefaultAsync();

            if (existingPot == null)
            {
                return "Pot not found";
            }

            existingPot.NameOfPot = updatedPotDto.Name;
            existingPot.Plant.NameOfPlant = updatedPotDto.NameOfPlant;
            existingPot.Plant.SoilMinimumMoisture = updatedPotDto.SoilMinimumMoisture;
            existingPot.Plant.ImageUrl = updatedPotDto.ImageUrl;
            existingPot.SoilMoisturePercent = updatedPotDto.HumidityPercent;

            await _pots.ReplaceOneAsync(p => p.Id == id, existingPot);

            return "Success";
        }
        catch (Exception ex)
        {
            return $"Error: {ex.Message}";
        }
    }

    public async Task<string> DeletePot(string id)
    {
        try
        {
            var result = await _pots.DeleteOneAsync(p => p.Id == id);
            return result.IsAcknowledged ? "Success" : "Failure";
        }
        catch (Exception e)
        {
            return $"Error: {e.Message}";

        }
    }
}