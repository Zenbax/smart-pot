﻿using System;
using System.Threading.Tasks;
using Application_.LogicInterfaces;
using Domain.DTOs;
using Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.ControllerFrontEnd;

[Authorize]
[ApiController]
[Route("plant")]
public class PlantController : ControllerBase
{
    private readonly IPlantLogic _plantLogic;

    public PlantController(IPlantLogic plantLogic)
    {
        _plantLogic = plantLogic;
    }

    [HttpGet("get/all")]
    public async Task<ActionResult<PlantGetAllDto>> GetAllPlants([FromQuery] string? userId)
    {
        try
        {
            var result = await _plantLogic.GetAllPlants(userId);
            if (result.Success == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        catch (Exception ex)
        {
            PlantGetAllDto plantGetAllDto = new PlantGetAllDto
            {
                Message = $"Error: {ex.Message}",
                Success = false
            };
            return StatusCode(500, plantGetAllDto);
        }
    }


    [HttpGet("get/{name}")]
    public async Task<ActionResult<PlantGetByNameDto>> GetPlantByName(string name)
    {
        PlantGetByNameDto plantGetByNameDto = new PlantGetByNameDto(name);
        try
        {
            var result = await _plantLogic.GetPlantByName(plantGetByNameDto);
            if (result.Success == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        catch (Exception ex)
        {
            plantGetByNameDto.Message = $"Error: {ex.Message}";
            plantGetByNameDto.Success = false;
            return StatusCode(500, plantGetByNameDto);
        }
    }

    [HttpPost("create")]
    public async Task<ActionResult<PlantCreationDto>> PostNewPlant([FromBody] CreatePlantRequestDto createPlantRequestDto)
    {
        if (createPlantRequestDto == null)
        {
            return BadRequest("Request data is null");
        }

        Plant plant = new Plant()
        {
            NameOfPlant = createPlantRequestDto.NameOfPlant,
            SoilMinimumMoisture = createPlantRequestDto.SoilMinimumMoisture,
            AmountOfWaterToBeGiven = createPlantRequestDto.AmountOfWaterToBeGiven,
            Image = createPlantRequestDto.Image,
            UserId = createPlantRequestDto.UserId,
            isDefault = false
        };

        PlantCreationDto plantCreationDto = new PlantCreationDto(plant);

        try
        {
            var result = await _plantLogic.CreatePlant(plantCreationDto);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        catch (Exception ex)
        {
            plantCreationDto.Message = $"Error: {ex.Message}";
            plantCreationDto.Success = false;
            return StatusCode(500, plantCreationDto);
        }
    }

    
    //den her nye metode sletter en plante
  
    
    // Create a new endpoint for updating a plant
    [HttpPut("update/{name}")]
    public async Task<ActionResult<PlantUpdateDto>> Put(string name, UpdatePlantRequestDto updatePlantRequestDto)
    {
        Plant plant = new Plant()
        {
            Id = updatePlantRequestDto?.Id,
            NameOfPlant = updatePlantRequestDto?.NameOfPlant,
            SoilMinimumMoisture = updatePlantRequestDto?.SoilMinimumMoisture,
            AmountOfWaterToBeGiven = updatePlantRequestDto?.AmountOfWaterToBeGiven,
            Image = updatePlantRequestDto?.Image
        };
        PlantUpdateDto plantUpdateDto = new PlantUpdateDto(name, plant);
        plantUpdateDto.NameToUpdate = name;
        try
        {
            var result = await _plantLogic.UpdatePlant(plantUpdateDto);
            if (result.Success == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        catch (Exception ex)
        {
            plantUpdateDto.Message = $"Error: {ex.Message}";
            plantUpdateDto.Success = false;
            return StatusCode(500, plantUpdateDto);
        }
    }
    
    [HttpDelete("delete/{name}")]
    public async Task<ActionResult<PlantDeleteDto>> Delete(string name)
    {
        PlantDeleteDto plantDeleteDto = new PlantDeleteDto(name);
        try
        {
            var result = await _plantLogic.DeletePlant(plantDeleteDto);
            if (result.Success == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        catch (Exception ex)
        {
            plantDeleteDto.Message = $"Error deleting plant with name "+plantDeleteDto.NameToDelete+": {ex.Message}";
            plantDeleteDto.Success = false;
            return StatusCode(500, plantDeleteDto);
        }
    }
}


//Update