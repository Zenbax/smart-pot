using Domain.DTOs;
using Domain.Model;
using YourApiNamespace.Controllers;

namespace Application_.LogicInterfaces;

public interface IPotLogic
{
    Task<PotGetAllDto> GetAllPots();
    Task<PotGetAllDto> GetAllPots(string userEmail);


    Task<PotGetByIdDto> GetPotById(PotGetByIdDto potGetByIdDto);
    Task<Pot> GetPotByMachineId(string machineId);
    Task<PotCreationDto> CreatePot(PotCreationDto potCreationDto);
    Task<PotUpdateDto> UpdatePot(PotUpdateDto potUpdateDto);
    Task<PotDeleteDto> DeletePot(PotDeleteDto potDeleteDto);
}