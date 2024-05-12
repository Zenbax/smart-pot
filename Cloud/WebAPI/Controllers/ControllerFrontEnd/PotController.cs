using Application_.LogicInterfaces;
using Domain.DTOs;
using Microsoft.AspNetCore.Mvc;
using YourApiNamespace.Controllers;

namespace WebAPI.Controllers.ControllerFrontEnd;

[ApiController]
[Route("[controller]")]
public class PotController : ControllerBase
{
    private readonly IPotLogic _potLogic;

    public PotController(IPotLogic potLogic)
    {
        _potLogic = potLogic;
    }

    [HttpGet("get/all")]
    public async Task<IActionResult> GetAllPots()
    {
        try
        {
            var pots = await _potLogic.GetAllPots();
            if (pots != null)
                return Ok(pots);
            else
                return NotFound("No pots found.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
    
    
    
    
    [HttpGet("get-by-machineid/{machineId}")]
    public async Task<IActionResult> GetPotByMachineId(string machineId)
    {
        try
        {
            var pot = await _potLogic.GetPotByMachineId(machineId);
            if (pot != null)
                return Ok(pot);
            else
                return NotFound($"Pot with Machine ID {machineId} not found.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
    
    

    [HttpGet("get/{id}")]
    public async Task<IActionResult> GetPotById(string id)
    {
        try
        {
            var pot = await _potLogic.GetPotById(id);
            if (pot != null)
                return Ok(pot);
            else
                return NotFound($"Pot with ID {id} not found.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreatePot([FromBody] PotCreationDto potDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        try
        {
            var result = await _potLogic.CreatePot(potDto);
            if (result == "Success")
                return Ok("Pot created successfully.");
            else
                return BadRequest(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdatePot(string id, [FromBody] PotUpdatedDto potUpdatedDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var result = await _potLogic.UpdatePot(id, potUpdatedDto);
            if (result == "Success")
                return Ok("Pot updated successfully.");
            else if (result == "Pot not found")
                return NotFound("Pot not found.");
            else
                return BadRequest(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeletePot(string id)
    {
        try
        {
            var result = await _potLogic.DeletePot(id);
            if (result == "Success")
                return Ok("Pot deleted successfully.");
            else if (result == "Pot not found")
                return NotFound("Pot not found.");
            else
                return BadRequest(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
    
    [HttpPut("update-plant/{potId}/{plantId}")]
    public async Task<IActionResult> UpdatePotPlant(string potId, string plantId)
    {
        if (string.IsNullOrWhiteSpace(potId) || string.IsNullOrWhiteSpace(plantId))
        {
            return BadRequest("Both pot ID and plant ID must be provided.");
        }

        try
        {
            var result = await _potLogic.UpdatePotPlant(potId, plantId);
            if (result == "Plant updated successfully")
                return Ok(result);
            else if (result == "Pot not found" || result == "Plant not found")
                return NotFound(result);
            else
                return BadRequest(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}