using Domain.Model;

namespace Domain.DTOs;

public class PlantGetByNameDto
{
    public PlantGetByNameDto(string nameToGet)
    {
        NameToGet = nameToGet;
    }
    
    public PlantGetByNameDto()// for testing purposes
    {
        
    }

    public Plant Plant { get; set; }
    public string NameToGet { get; set; }
    public string Message { get; set; }
    public bool Success { get; set; }
}