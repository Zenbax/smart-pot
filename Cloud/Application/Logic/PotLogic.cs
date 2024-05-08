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
            throw new Exception(ex.Message);
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
            var newPot = new Pot
            {
                NameOfPot = potDto.PotName,
                Email = potDto.Email,
                Enable = potDto.Enable,
                MachineID = potDto.MachineID,
                Plant = potDto.Plant
                
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

    public async Task<string> UpdatePot(string id, PotUpdatedDto potUpdatedDto)
    {
        try
        {
            var existingPot = await _pots.Find(p => p.Id == id).FirstOrDefaultAsync();

            if (existingPot == null)
            {
                return "Pot not found";
            }

            existingPot.NameOfPot = potUpdatedDto.PotName;
            existingPot.Email = potUpdatedDto.Email;
            existingPot.Enable = potUpdatedDto.Enable;
            existingPot.Plant = potUpdatedDto.Plant;
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
    
    
    
    


    public async Task<Pot> GetPotByMachineId(string machineId)
    {
        try
        {
            return await _pots.Find(p => p.MachineID == machineId).FirstOrDefaultAsync();
        }
        catch (Exception ex)
        {
            // Log the exception or handle it as necessary
            throw new Exception("Failed to fetch pot by MachineID: " + ex.Message);
        }
    }
    
    
    
    
}