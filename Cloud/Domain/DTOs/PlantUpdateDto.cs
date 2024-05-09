using System.ComponentModel.DataAnnotations;
using Domain.Model;

namespace Domain.DTOs;

public class PlantUpdateDto
{
    public PlantUpdateDto(string name, Plant plant)
    {
        NameToUpdate = name;
        Plant = plant;
    }
    [Required] public Plant Plant { get; set; }
    [Required] public string NameToUpdate { get; set; }
    public string Message { get; set; }
    public bool Success { get; set; }
    
    public string ToString()
    {
        return $"NameToUpdate: {NameToUpdate}, Plant: {Plant}, Message: {Message}, Success: {Success}";
    }
}