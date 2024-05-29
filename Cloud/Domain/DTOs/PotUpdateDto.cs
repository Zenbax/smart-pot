using System.ComponentModel.DataAnnotations;
using Domain.Model;
using YourApiNamespace.Controllers;

namespace Domain.DTOs;

public class PotUpdateDto
{
    public PotUpdateDto(string id, Pot pot)
    {
        IdToUpdate = id;
        Pot = pot;
    }
    
    public PotUpdateDto()
    {
    }
    public string IdToUpdate { get; set; }
    public Pot Pot { get; set; }
    public string Message { get; set; }
    public bool Success { get; set; }
}