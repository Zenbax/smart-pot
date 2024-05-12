﻿using Application_.LogicInterfaces;
using Domain.DTOs;
using Domain.Model;
using MongoDB.Bson;
using MongoDB.Driver;
using YourApiNamespace.Controllers;

namespace Application_.Logic;
    public class PotLogic : IPotLogic
{
    private readonly IMongoCollection<Pot> _pots;
    private readonly IMongoCollection<Plant> _plants;

    public PotLogic(IMongoCollection<Pot> potsCollection, IMongoCollection<Plant> plantsCollection)
    {
        _pots = potsCollection;
        _plants = plantsCollection;
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
            var potFound = await GetPotByMachineId(potDto.MachineID);
            if (potFound != null)
            {
                return "Pot with Machine ID already exists";
            }
            var newPot = new Pot
            {
                NameOfPot = potDto.PotName,
                Email = potDto.Email,
                Enable = false,
                MachineID = potDto.MachineID,
                PlantId = null
                
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
            existingPot.Enable = potUpdatedDto.Enable;
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
    
    public async Task<string> UpdatePotPlant(string potId, string plantId)
    {
        try
        {
            // Check if the pot exists
            var pot = await _pots.Find(p => p.Id == potId).FirstOrDefaultAsync();
            if (pot == null)
            {
                return "Pot not found";
            }

            // Check if the new plant exists
            var plantExists = await _plants.Find(p => p.Id == plantId).AnyAsync();
            if (!plantExists)
            {
                return "Plant not found";
            }

            // Set all other plants in this pot to inactive
            var deactivateOtherPlants = Builders<Plant>.Update.Set(p => p.Active, false);
            await _plants.UpdateManyAsync(
                p => p.PotId == potId && p.Id != plantId,
                deactivateOtherPlants
            );

            // Update the pot with the new active plant
            var updatePot = Builders<Pot>.Update.Set(p => p.PlantId, plantId).Set(p => p.Enable, true);
            var result = await _pots.UpdateOneAsync(p => p.Id == potId, updatePot);

            // Set the new plant as active
            var activateNewPlant = Builders<Plant>.Update.Set(p => p.Active, true).Set(p => p.PotId, potId);
            await _plants.UpdateOneAsync(p => p.Id == plantId, activateNewPlant);

            return result.IsAcknowledged && result.ModifiedCount > 0 ? "Plant updated successfully" : "Update failed";
        }
        catch (Exception ex)
        {
            return $"Error: {ex.Message}";
        }
    }
    
}