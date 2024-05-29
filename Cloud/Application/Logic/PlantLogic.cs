using Application_.LogicInterfaces;
using Domain.DTOs;
using Domain.Model;
using MongoDB.Driver;

namespace Application_.Logic;

public class PlantLogic : IPlantLogic
{
    private readonly IMongoCollection<Plant> _plants;

    public PlantLogic(IMongoCollection<Plant> plantsCollection)
    {
        _plants = plantsCollection;
    }

    public async Task<PlantGetAllDto> GetAllPlants(string? userId)
    {
        PlantGetAllDto plantGetAllDto = new PlantGetAllDto();
        try
        {
            if (string.IsNullOrEmpty(userId))
            {
                // Retrieve all plants, including those with isDefault = true
                plantGetAllDto.Plants = await _plants.Find(p => p.isDefault == true).ToListAsync();
            }
            else
            {
                // Retrieve plants with matching userId, plants with null userId, and plants with isDefault = true
                plantGetAllDto.Plants = await _plants.Find(p => p.UserId == userId || p.UserId == null || p.isDefault == true).ToListAsync();
            }

            if (plantGetAllDto.Plants.Count == 0)
            {
                plantGetAllDto.Message = "No plants in database.";
            }
            else
            {
                plantGetAllDto.Message = "All plants found.";
            }
            plantGetAllDto.Success = true;
        }
        catch (Exception ex)
        {
            plantGetAllDto.Message = $"Error in PlantLogic: {ex.Message}";
            plantGetAllDto.Success = false;
        }
        return plantGetAllDto;
    }


    public async Task<PlantGetByNameDto> GetPlantByName(PlantGetByNameDto plantGetByNameDto)
    {
        try
        {
            Plant plant = await _plants.Find(p => p.NameOfPlant == plantGetByNameDto.NameToGet).FirstOrDefaultAsync();
            if (plant == null)
            {
                plantGetByNameDto.Message = "Plant with name " + plantGetByNameDto.NameToGet + " not found.";
                plantGetByNameDto.Success = false;
                plantGetByNameDto.Plant = null;
            }
            else
            {
                plantGetByNameDto.Message = "Plant with name " + plantGetByNameDto.NameToGet + " found.";
                plantGetByNameDto.Success = true;
                plantGetByNameDto.Plant = plant;
            }
        }
        catch (Exception ex)
        {
            plantGetByNameDto.Message = $"Error in PlantLogic: {ex.Message}";
            plantGetByNameDto.Success = false;
            plantGetByNameDto.Plant = null;
        }
        return plantGetByNameDto;
    }

    public async Task<PlantCreationDto> CreatePlant(PlantCreationDto plantCreationDto)
    {
        try
        {
            await _plants.InsertOneAsync(plantCreationDto.Plant);
            plantCreationDto.Message = "Plant created successfully.";
            plantCreationDto.Success = true;
        }
        catch (Exception ex)
        {
            plantCreationDto.Message = $"Error: {ex.Message}";
            plantCreationDto.Success = false;
        }
        return plantCreationDto;
    }

    public async Task<PlantUpdateDto> UpdatePlant(PlantUpdateDto updatePlantDto)
    {
        try
        {
            var plant = await _plants.Find(p => p.NameOfPlant == updatePlantDto.NameToUpdate).FirstOrDefaultAsync();
            if (plant?.Id == null)
            {
                updatePlantDto.Message = "Plant with name " + updatePlantDto.NameToUpdate + " not found.";
                updatePlantDto.Success = false;
                updatePlantDto.Plant = null;
            }
            else
            {
                updatePlantDto.Plant.Id = plant.Id;
                plant.NameOfPlant = updatePlantDto.Plant.NameOfPlant;
                plant.SoilMinimumMoisture = updatePlantDto.Plant.SoilMinimumMoisture;
                plant.AmountOfWaterToBeGiven = updatePlantDto.Plant.AmountOfWaterToBeGiven;
                plant.Image = updatePlantDto.Plant.Image;

                await _plants.ReplaceOneAsync(p => p.NameOfPlant == updatePlantDto.NameToUpdate, plant);
                updatePlantDto.Message = "Plant updated successfully.";
                updatePlantDto.Success = true;
            }
        }
        catch (Exception ex)
        {
            updatePlantDto.Message = $"Error in PlantLogic: {ex.Message}";
            updatePlantDto.Success = false;
        }
        return updatePlantDto;
    }

    public async Task<PlantDeleteDto> DeletePlant(PlantDeleteDto plantDeleteDto)
    {
        try
        {
            Plant plant = await _plants.Find(p => p.NameOfPlant == plantDeleteDto.NameToDelete).FirstOrDefaultAsync();
            if (plant == null)
            {
                plantDeleteDto.Message = "Plant with name " + plantDeleteDto.NameToDelete + " not found.";
                plantDeleteDto.Success = false;
                return plantDeleteDto;
            }
            
            await _plants.DeleteOneAsync(p => p.NameOfPlant == plantDeleteDto.NameToDelete);
            plantDeleteDto.Message = "Plant deleted with name "+plantDeleteDto.NameToDelete+" successfully.";
            plantDeleteDto.Success = true;
        }
        catch (Exception ex)
        {
            plantDeleteDto.Message = $"Error deleting plant with name "+plantDeleteDto.NameToDelete+": {ex.Message}";
            plantDeleteDto.Success = false;
        }
        return plantDeleteDto;
    }
}