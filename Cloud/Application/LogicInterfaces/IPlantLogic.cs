using Domain.DTOs;
using Domain.Model;

namespace Application_.LogicInterfaces;

public interface IPlantLogic
{
    Task<PlantGetAllDto> GetAllPlants(string? userId); 
    Task<PlantGetByNameDto> GetPlantByName(PlantGetByNameDto plantGetByNameDto);
    Task<PlantCreationDto> CreatePlant(PlantCreationDto plantCreationDto);
    Task<PlantUpdateDto> UpdatePlant(PlantUpdateDto updatedPlantDto);
    Task<PlantDeleteDto> DeletePlant(PlantDeleteDto plantDeleteDto);

}