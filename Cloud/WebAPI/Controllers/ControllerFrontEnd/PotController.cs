using Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Socket;
using System;
using System.Collections.Generic;
using Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Socket;
using System;
using System.Collections.Generic;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PotController : ControllerBase
    {
        private readonly IPotService _potService;
        private readonly IHumidityLogService _humidityLogService;
        private readonly IHubContext<HumidityHub> _hubContext;

        public PotController(IPotService potService, IHumidityLogService humidityLogService, IHubContext<HumidityHub> hubContext)
        {
            _potService = potService;
            _humidityLogService = humidityLogService;
            _hubContext = hubContext;
        }

        [HttpGet("{id}")]
        public ActionResult<Pot> GetPotById(int id)
        {
            var pot = _potService.GetPotById(id);
            if (pot == null)
            {
                return NotFound();
            }
            return Ok(pot);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Pot>> GetAllPots()
        {
            return Ok(_potService.GetAllPots());
        }

        [HttpPost("humidity")]
        public ActionResult SaveHumidityLog([FromBody] HumidityLog humidityLog)
        {
            try
            {
                _humidityLogService.SaveHumidityLog(humidityLog);
                _hubContext.Clients.All.SendAsync("ReceiveHumidityUpdate", humidityLog.PotId, humidityLog.HumidityPercentage);
                return Ok("Humidity log saved successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error saving humidity log: {ex.Message}");
            }
        }

        [HttpGet("humidity/{potId}")]
        public ActionResult<double> GetLatestHumidityLog(int potId)
        {
            try
            {
                var humidityLog = _humidityLogService.GetLatestHumidityLog(potId);
                if (humidityLog == null)
                {
                    return NotFound();
                }
                return Ok(humidityLog.HumidityPercentage);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving latest humidity log: {ex.Message}");
            }
        }
    }
}
