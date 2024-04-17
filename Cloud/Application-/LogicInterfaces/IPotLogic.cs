using Domain.DTOs;
using YourApiNamespace.Controllers;

namespace Application_.LogicInterfaces;

public interface IPotLogic
{
    Task<IEnumerable<Pot>> GetAllPots();
    Task<Pot> GetPotById(string id);
    Task<string> CreatePot(PotCreationDto potDto);
    Task<string> UpdatePot(string id, UpdatedPotDto updatedPotDto);
    Task<string> DeletePot(string id);
}