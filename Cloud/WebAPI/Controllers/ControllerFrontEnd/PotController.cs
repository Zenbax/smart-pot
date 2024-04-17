using Application_.LogicInterfaces;
using Domain.DTOs;
using Microsoft.AspNetCore.Mvc;
using YourApiNamespace.Controllers;

namespace DefaultNamespace;
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
        return await _potLogic.GetAllPots();
    }
    [HttpGet("{id}")]
    public async Task<Pot> Get(string id)
    {
        return await _potLogic.GetPotById(id);
    }
    [HttpPost]
    public async Task<string> Post(PotCreationDto potCreationDto)
    {
        return await _potLogic.CreatePot(potCreationDto);
    }
    [HttpPut("{id}")]
    public async Task<string> Put(string id, PotCreationDto potCreationDto)
    {
        return await _potLogic.UpdatePot(id, potCreationDto);
    }
    [HttpDelete("{id}")]
    public async Task<string> Delete(string id)
    {
        return await _potLogic.DeletePot(id);
    }
    
}