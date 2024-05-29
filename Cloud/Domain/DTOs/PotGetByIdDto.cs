using Domain.Model;
using YourApiNamespace.Controllers;

namespace Domain.DTOs;

public class PotGetByIdDto
{
    public PotGetByIdDto(string idToGet)
    {
        IdToGet = idToGet;
    }
    
    public PotGetByIdDto()// for 
    {
    }

    public Pot Pot { get; set; }
    public string IdToGet { get; set; }
    public string Message { get; set; }
    public bool Success { get; set; }
}