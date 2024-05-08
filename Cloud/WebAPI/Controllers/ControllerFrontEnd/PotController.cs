using Application_.LogicInterfaces;
using Domain.DTOs;
using Microsoft.AspNetCore.Mvc;
using YourApiNamespace.Controllers;

namespace WebAPI.Controllers.ControllerFrontEnd;

    [ApiController]
    [Route("pot")]
    public class PotController : ControllerBase
    {
        private readonly IPotLogic _potLogic;

        public PotController(IPotLogic potLogic)
        {
            _potLogic = potLogic;
        }

        [HttpGet("get/all")]
        public async Task<ActionResult<IEnumerable<Pot>>> GetPot()
        {
            try
            {
                var pots = await _potLogic.GetAllPots();
                return Ok(pots);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving pots.");
            }
        }

        [HttpGet("get/{id}")]
        public async Task<ActionResult<Pot>> GetPotById(string id)
        {
            try
            {
                var pot = await _potLogic.GetPotById(id);
                if (pot == null)
                {
                    return NotFound();
                }
                return pot;
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return null;
            }
        }

        [HttpPost("create")]
        public async Task<ActionResult<string>> Post(PotCreationDto potCreationDto)
        {
            try
            {
                var result = await _potLogic.CreatePot(potCreationDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return ex.Message;
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> Delete(string id)
        {
            try
            {
                var result = await _potLogic.DeletePot(id);
                if (result == "Pot not found")
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return ex.Message;
            }
        }
        
        //New
}