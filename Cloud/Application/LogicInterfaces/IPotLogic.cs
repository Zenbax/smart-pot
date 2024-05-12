using Domain.DTOs;
using Domain.Model;
using YourApiNamespace.Controllers;

namespace Application_.LogicInterfaces;

public interface IPotLogic
{
    Task<IEnumerable<Pot>> GetAllPots();
    Task<Pot> GetPotById(string id);
    Task<Pot> GetPotByMachineId(string machineId);  // Newly added method
    Task<string> CreatePot(PotCreationDto potDto);
    Task<string> UpdatePot(string id, PotUpdatedDto potUpdatedDto);
    Task<string> DeletePot(string id);
    Task<string> UpdatePotPlant(string potId, string plantId);
}