using System.ComponentModel.DataAnnotations;
using Domain.Model;

namespace Domain.DTOs;

public class UserGetByIdDto
{
    public UserGetByIdDto(string idToGet)
    {
        IdToGet = idToGet;
    }

    public UserGetByIdDto()
    {
    }


    public User User { get; set; }
    public string IdToGet { get; set; }
    public string Message { get; set; }
    public bool Success { get; set; }
    
}