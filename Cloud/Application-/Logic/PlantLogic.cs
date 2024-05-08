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

    public async Task<IEnumerable<Plant>> GetAllPlants()
    {
        try
        {
            return await _plants.Find(p => true).ToListAsync();
        }
        catch (Exception ex)
        { 
            return null;
        }
    }

    public async Task<Plant> GetPlantByName(string name)
    {
        try
        {
            return await _plants.Find(p => p.NameOfPlant == name).FirstOrDefaultAsync();
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<string> CreatePlant(PlantCreationDto plantDto)
    {
        try
        {
            var plant = new Plant
            {
                NameOfPlant = plantDto.NameOfPlant,
                SoilMinimumMoisture = plantDto.SoilMinimumMoisture,
                WaterML = plantDto.WaterML,
                ImageUrl = plantDto.ImageUrl,
            };

            await _plants.InsertOneAsync(plant);
            return "Success";
        }
        catch (Exception ex)
        {
            return $"Error: {ex.Message}";
        }
    }

    public async Task<string> UpdatePlant(string name, PlantUpdateDto updatedPlantDto)
    {
        try
        {
            var plant = await _plants.Find(p => p.NameOfPlant == name).FirstOrDefaultAsync();
            if (plant == null)
            {
                return "Plant not found";
            }

            plant.NameOfPlant = updatedPlantDto.NameOfPlant;
            plant.SoilMinimumMoisture = updatedPlantDto.SoilMinimumMoisture;
            plant.WaterML = updatedPlantDto.WaterML;
            plant.ImageUrl = updatedPlantDto.ImageURL;

            await _plants.ReplaceOneAsync(p => p.NameOfPlant == name, plant);
            return "Success";
        }
        catch (Exception ex)
        {
            return $"Error: {ex.Message}";
        }
    }

    public async Task<string> DeletePlant(string name)
    {
        try
        {
            await _plants.DeleteOneAsync(p => p.NameOfPlant == name);
            return "Success";
        }
        catch (Exception ex)
        {
            return $"Error: {ex.Message}";
        }
    }

    public Task<Plant> GetPlantById(string id)
    {
        try
        {
            return _plants.Find(p => p.Id == id).FirstOrDefaultAsync();
        }
        catch (Exception ex)
        {
            return null;
        }
    }
}