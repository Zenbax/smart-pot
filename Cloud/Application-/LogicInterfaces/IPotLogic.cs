using Domain.DTOs;
using YourApiNamespace.Controllers;

namespace Application_.LogicInterfaces;

public interface IPotLogic
{
    IEnumerable<Pot> GetAllPots();
    Pot GetPotById(string id);
    void CreatePot(PotCreationDto potDto);
    void UpdatePot(string id, PotCreationDto updatedPotDto);
    void DeletePot(string id);
}