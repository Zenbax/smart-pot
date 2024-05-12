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

<<<<<<< HEAD
    [Required] public Pot Pot { get; set; }
    public string Message { get; set; }
    public bool Success { get; set; }
=======
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    public string Email { get; set; }
    
    public string MachineID { get; set; }
>>>>>>> Artesh_Branchh
}