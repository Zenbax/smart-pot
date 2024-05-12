﻿using Application_.LogicInterfaces;
using Domain.DTOs;
using Domain.Model;
using MongoDB.Driver;
using YourApiNamespace.Controllers;

namespace Application_.Logic;

public class PlantLogic : IPlantLogic
{
    private readonly IMongoCollection<Plant> _plants;
    private readonly IMongoCollection<Pot> _pots;

    public PlantLogic(IMongoCollection<Plant> plantsCollection, IMongoCollection<Pot> potsCollection)
    {
        _plants = plantsCollection;
        _pots = potsCollection;
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
            var newPlant = new Plant
            {
                NameOfPlant = plantDto.NameOfPlant,
                SoilMinimumMoisture = plantDto.SoilMinimumMoisture,
                WaterTankLevel = plantDto.WaterTankLevel,
                ImageURL = plantDto.ImageURL,
                Active = false,
                PotId = null
            };
            
            // Insert the new plant into the collection
            await _plants.InsertOneAsync(newPlant);
            
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
            plant.WaterTankLevel = updatedPlantDto.WaterTankLevel;
            plant.ImageURL = updatedPlantDto.ImageURL;

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
}