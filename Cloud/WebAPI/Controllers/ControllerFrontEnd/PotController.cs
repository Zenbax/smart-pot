using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Application_.LogicInterfaces;
using Domain.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using YourApiNamespace.Controllers;

namespace WebAPI.Controllers.ControllerFrontEnd;

    [Authorize]
    [ApiController]
    [Route("pot")]
    public class PotController : ControllerBase
    {
        private readonly IPotLogic _potLogic;

        public PotController(IPotLogic potLogic)
        {
            _potLogic = potLogic;
        }
        
        //måske skal denne metode slettes
/*
        [HttpGet("get/all")]
        public async Task<ActionResult<PotGetAllDto>> GetPot()
        {
            PotGetAllDto potGetAllDto = new PotGetAllDto();
            try
            {
                var result = await _potLogic.GetAllPots();
                if (result.Success == false)
                {
                    return BadRequest(result);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                potGetAllDto.Message = $"Error: {ex.Message}";
                potGetAllDto.Success = false;
                return StatusCode(500, potGetAllDto);
            }
        }
        */
        
        
        
        [HttpGet("get/all")]
        public async Task<IActionResult> GetAllPots()
        {
            // Assumption: The email is stored as a claim, adjust based on your actual implementation
            string userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            var result = await _potLogic.GetAllPots(userEmail);
            return Ok(result);
        }
        

        [HttpGet("get/{id}")]
        public async Task<ActionResult<PotGetByIdDto>> GetPotById(string id)
        {
            PotGetByIdDto potGetByIdDto = new PotGetByIdDto(id);
            try
            {
                var result = await _potLogic.GetPotById(potGetByIdDto);
                if (result.Success == false)
                {
                    return BadRequest(result);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                potGetByIdDto.Message = $"Error: {ex.Message}";
                potGetByIdDto.Success = false;
                return StatusCode(500, potGetByIdDto);
            }
        }

        [HttpPost("create")]
        public async Task<ActionResult<PotCreationDto>> CreatePot(CreatePotRequestDto createPotRequestDto)
        {
            var pot = new Pot
            {
                NameOfPot = createPotRequestDto.PotName,
                Email = createPotRequestDto.Email,
                Enable = createPotRequestDto.Enable,
                MachineID = createPotRequestDto.MachineID,
                Plant = createPotRequestDto.Plant
            };

            var result = await _potLogic.CreatePot(new PotCreationDto(pot));
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }



        [HttpPut("update/{id}")] 
        public async Task<IActionResult> UpdatePot(string id, [FromBody] UpdatePotRequestDto updatePotRequestDto)
        {
            Console.WriteLine("UpdatePot called.");
            Pot pot = new Pot()
            {
                NameOfPot = updatePotRequestDto.PotName,
                Email = updatePotRequestDto.Email,
                Enable = updatePotRequestDto.Enable,
                MachineID = updatePotRequestDto.MachineID,
                Plant = updatePotRequestDto.Plant
            };
            PotUpdateDto potUpdatedDto = new PotUpdateDto(id, pot);
            try
            {
                var result = await _potLogic.UpdatePot(potUpdatedDto);
                if (result.Success == false)
                {
                    return BadRequest(result);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                potUpdatedDto.Message = $"Error: {ex.Message}";
                potUpdatedDto.Success = false;
                return StatusCode(500, potUpdatedDto);
            }
        }
        

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<PotDeleteDto>> Delete(string id)
        {
            PotDeleteDto potDeleteDto = new PotDeleteDto(id);
            try
            {
                var result = await _potLogic.DeletePot(potDeleteDto);
                if (result.Success == false)
                {
                    return BadRequest(result);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                potDeleteDto.Message = $"Error: {ex.Message}";
                potDeleteDto.Success = false;
                return StatusCode(500, potDeleteDto);
            }
        }
}