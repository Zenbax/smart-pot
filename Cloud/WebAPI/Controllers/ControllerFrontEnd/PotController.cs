using Application_.LogicInterfaces;
using Domain.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YourApiNamespace.Controllers;

namespace WebAPI.Controllers.ControllerFrontEnd;

    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class PotController : ControllerBase
    {
        private readonly IPotLogic _potLogic;

        public PotController(IPotLogic potLogic)
        {
            _potLogic = potLogic;
        }

        [HttpGet]
        public async Task<IEnumerable<Pot>> Get()
        {
            try
            {
                return await _potLogic.GetAllPots();
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return null;
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pot>> Get(string id)
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

        [HttpPost]
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
}