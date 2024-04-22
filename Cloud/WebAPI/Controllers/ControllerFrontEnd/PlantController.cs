using Application_.LogicInterfaces;
using Domain.DTOs;
using Domain.Model;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.ControllerFrontEnd;

[ApiController]
[Route("[controller]")]
public class PlantController : ControllerBase
{
    private readonly IPlantLogic _plantLogic;

    public PlantController(IPlantLogic plantLogic)
    {
        _plantLogic = plantLogic;
    }

    [HttpGet]
    public async Task<IEnumerable<Plant>> Get()
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

    [HttpGet("{name}")]
    public async Task<ActionResult<Plant>> Get(string name)
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

    [HttpPost]
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

    [HttpDelete("{name}")]
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
    
}