using System.ComponentModel.DataAnnotations;
using Domain.Model;
using YourApiNamespace.Controllers;

namespace Domain.DTOs;

public class PotCreationDto
{
    public PotCreationDto(Pot pot)
    {
        Pot = pot;
    }

    [Required] public Pot Pot { get; set; }
    public string Message { get; set; }
    public bool Success { get; set; }
}