using Application_.LogicInterfaces;
using Domain.DTOs;
using Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.ControllerFrontEnd;

[Authorize]
[ApiController]
[Route("plant")]
public class PlantController : ControllerBase
{
    private readonly IPlantLogic _plantLogic;
    private readonly IPotLogic _potLogic;

    public PlantController(IPlantLogic plantLogic, IPotLogic potLogic)
    {
        _plantLogic = plantLogic;
        _potLogic = potLogic;
    }
  
    [HttpGet("get/all")]
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

    [HttpGet("get/{name}")]
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

    [HttpPost("{potId}/create")]
    public async Task<ActionResult<string>> Post(PlantCreationDto plantCreationDto, string potId)
    {
        try
        {
            // checkk if pot exists
            var pot = await _potLogic.GetPotById(potId);
            if (pot == null)
            {
                return NotFound("Pot not found");
            }
            
            // set plant potid
            plantCreationDto.PotId = potId;
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