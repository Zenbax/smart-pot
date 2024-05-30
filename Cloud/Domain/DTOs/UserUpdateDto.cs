using System.ComponentModel.DataAnnotations;
using Domain.Model;

namespace Domain.DTOs;

public class UserUpdateDto
{
    public UserUpdateDto(string id, User user)
    {
        IdToUpdate = id;
        User = user;
    }

    public UserUpdateDto()
    {
    }

    [Required] public User User { get; set; }
    [Required] public string IdToUpdate { get; set; }
    public string Message { get; set; }
    public bool Success { get; set; }
}