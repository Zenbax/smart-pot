using Application_.LogicInterfaces;
using Domain.DTOs;
using Domain.Model;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.ControllerFrontEnd;

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
    public async Task<IEnumerable<Plant>> GetAllPlant()
    {
        try
        {
            return await _plantLogic.GetAllPlants();
        }
        catch (Exception ex)
        {
            Response.StatusCode = 500;
            return null;
        }
    }

    [HttpGet("getByName/{name}")]
    public async Task<ActionResult<Plant>> GetPlantByName(string name)
    {
        try
        {
            var plant = await _plantLogic.GetPlantByName(name);
            if (plant == null)
            {
                return NotFound();
            }
            return plant;
        }
        catch (Exception ex)
        {
            Response.StatusCode = 500;
            return null;
        }
    }
    
    
    [HttpGet("getById/{id}")]
    public async Task<ActionResult<Plant>> GetPlantById(string id)
    {
        try
        {
            var plant = await _plantLogic.GetPlantById(id);
            if (plant == null)
            {
                return NotFound();
            }
            return plant;
        }
        catch (Exception ex)
        {
            Response.StatusCode = 500;
            return null;
        }
    }
    

    [HttpPost("create")]
    public async Task<ActionResult<string>> Post(PlantCreationDto plantCreationDto)
    {
        try
        {
            var result = await _plantLogic.CreatePlant(plantCreationDto);
            return Ok(result);
        }
        catch (Exception ex)
        {
            Response.StatusCode = 500;
            return null;
        }
    }

    [HttpDelete("delete/{name}")]
    public async Task<ActionResult<string>> Delete(string name)
    {
        try
        {
            var result = await _plantLogic.DeletePlant(name);
            return Ok(result);
        }
        catch (Exception ex)
        {
            Response.StatusCode = 500;
            return null;
        }
    }
    //Not implemented yet
}