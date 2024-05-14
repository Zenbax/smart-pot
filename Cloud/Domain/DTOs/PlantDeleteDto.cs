namespace Domain.DTOs;

public class PlantDeleteDto
{
    public PlantDeleteDto(string nameToDelete)
    {
        NameToDelete = nameToDelete;
    }

    public PlantDeleteDto()// for testing purposes
    {

    }
    
    public string NameToDelete { get; set; }
    public string Message { get; set; }
    public bool Success { get; set; }
}