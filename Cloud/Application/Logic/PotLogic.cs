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
    private readonly IMongoCollection<SensorData> _sensorData;

    public PotLogic(IMongoCollection<Pot> potsCollection, IMongoCollection<SensorData> sensorDataCollection)
    {
        _pots = potsCollection;
        _sensorData = sensorDataCollection;
    }

    public async Task<PotGetAllDto> GetAllPots()
    {
        PotGetAllDto potGetAllDto = new PotGetAllDto();
        try
        {
            var pots = await _pots.Find(p => true).ToListAsync();
            foreach (var pot in pots)
            {
                pot.SensorData = await _sensorData.Find(s => s.PotId == pot.Id).ToListAsync();
            }
            potGetAllDto.Pots = pots;
            if (potGetAllDto.Pots.Count == 0)
            {
                potGetAllDto.Message = "No pots in database.";
            }
            else
            {
                potGetAllDto.Message = "All pots found.";
            }
            potGetAllDto.Success = true;
        }
        catch (Exception ex)
        {
            potGetAllDto.Message = $"Error in PotLogic: {ex.Message}";
            potGetAllDto.Success = false;
        }
        return potGetAllDto;
    }

    public async Task<PotGetAllDto> GetAllPots(string userEmail)
    {
        var pots = await _pots.Find(p => p.Email == userEmail).ToListAsync();
        PotGetAllDto potGetAllDto = new PotGetAllDto { Pots = pots };
        return potGetAllDto;
    }

    public async Task<PotGetByIdDto> GetPotById(PotGetByIdDto potGetByIdDto)
    {
        try
        {
            var pot = await _pots.Find(p => p.Id == potGetByIdDto.IdToGet).FirstOrDefaultAsync();
            if (pot == null)
            {
                potGetByIdDto.Message = "Pot not found";
                potGetByIdDto.Success = false;
            }
            else
            {
                pot.SensorData = await _sensorData.Find(s => s.PotId == pot.Id).ToListAsync();
                potGetByIdDto.Pot = pot;
                potGetByIdDto.Message = "Pot found";
                potGetByIdDto.Success = true;
            }
        }
        catch (Exception ex)
        {
            potGetByIdDto.Message = $"Error: {ex.Message}";
            potGetByIdDto.Success = false;
        }

        return potGetByIdDto;
    }

    public async Task<PotCreationDto> CreatePot(PotCreationDto potCreationDto)
    {
        try
        {
            // Check if MachineID already exists
            var existingPot = await _pots.Find(p => p.MachineID == potCreationDto.Pot.MachineID).FirstOrDefaultAsync();
            if (existingPot != null)
            {
                return new PotCreationDto
                {
                    Success = false,
                    Message = "MachineID already exists. Cannot create pot."
                };
            }

            await _pots.InsertOneAsync(potCreationDto.Pot);
            return new PotCreationDto
            {
                Pot = potCreationDto.Pot,
                Success = true,
                Message = "Pot created successfully."
            };
        }
        catch (Exception ex)
        {
            return new PotCreationDto
            {
                Success = false,
                Message = $"An error occurred while creating the pot: {ex.Message}"
            };
        }
    }

    public async Task<PotUpdateDto> UpdatePot(PotUpdateDto potUpdateDto)
    {
        try
        {
            // Check if the MachineID is already in use by another pot
            var potWithSameMachineId = await _pots.Find(p => p.MachineID == potUpdateDto.Pot.MachineID).FirstOrDefaultAsync();
            if (potWithSameMachineId != null && potWithSameMachineId.Id != potUpdateDto.IdToUpdate)
            {
                potUpdateDto.Message = "MachineID is already in use by the pot with Name: " +
                                       potWithSameMachineId.NameOfPot + " and the ID: " + potWithSameMachineId.Id;
                potUpdateDto.Success = false;
                potUpdateDto.Pot = null;
                return potUpdateDto;
            }

            var existingPot = await _pots.Find(p => p.Id == potUpdateDto.IdToUpdate).FirstOrDefaultAsync();

            if (existingPot == null)
            {
                potUpdateDto.Message = "Pot with ID" + potUpdateDto.IdToUpdate + " not found";
                potUpdateDto.Success = false;
                potUpdateDto.Pot = null;
            }
            else
            {
                existingPot.NameOfPot = potUpdateDto.Pot.NameOfPot;
                existingPot.Email = potUpdateDto.Pot.Email;
                existingPot.Enable = potUpdateDto.Pot.Enable;
                existingPot.Plant = potUpdateDto.Pot.Plant;
                existingPot.MachineID = potUpdateDto.Pot.MachineID; // Update the MachineID
                await _pots.ReplaceOneAsync(p => p.Id == potUpdateDto.IdToUpdate, existingPot);
                potUpdateDto.Pot.Id = existingPot.Id;
                potUpdateDto.Message = "Pot updated successfully";
                potUpdateDto.Success = true;
            }
        }
        catch (Exception ex)
        {
            potUpdateDto.Message = $"Error: {ex.Message}";
            potUpdateDto.Success = false;
        }

        return potUpdateDto;
    }

    public async Task<PotDeleteDto> DeletePot(PotDeleteDto potDeleteDto)
    {
        try
        {
            var result = await _pots.DeleteOneAsync(p => p.Id == potDeleteDto.IdToDelete);
            if (result.DeletedCount == 0)
            {
                potDeleteDto.Message = "Pot not found";
                potDeleteDto.Success = false;
            }
            else
            {
                potDeleteDto.Message = "Pot deleted successfully";
                potDeleteDto.Success = true;
            }
        }
        catch (Exception ex)
        {
            potDeleteDto.Message = $"Error: {ex.Message}";
            potDeleteDto.Success = false;
        }

        return potDeleteDto;
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