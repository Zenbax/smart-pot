using Domain.DTOs;
using Domain.Model;

namespace Application_.LogicInterfaces;

public interface IPlantLogic
{
    Task<IEnumerable<Plant>> GetAllPlants();
    Task<Plant> GetPlantByName(string name);
    Task<string> CreatePlant(PlantCreationDto plantDto);
    Task<string> UpdatePlant(string name, PlantUpdateDto updatedPlantDto);
    Task<string> DeletePlant(string name);
    Task<Plant> GetPlantById(string id);

}