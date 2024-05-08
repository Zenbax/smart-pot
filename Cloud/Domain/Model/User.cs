using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Model;

public class User
{
    
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }

    [Required(ErrorMessage = "LastName is required")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }

    [Required(ErrorMessage = "PhoneNumber is required")]
    public string PhoneNumber { get; set; }
    
    public User()
    {
    }
    
    public User(string id, string name, string lastName, string email, string password, string phoneNumber, bool ignoreRequired = false)
    {
        if (ignoreRequired)
        {
            Id = "";
            Name = "";
            LastName = "";
            Email = "";
            Password = "";
        }
        Id = id;
        Name = name;
        LastName = lastName;
        Email = email;
        Password = password;
        PhoneNumber = phoneNumber;
    }
    
    public string ToString()
    {
        return "Id: " + Id + ", Name: " + Name + ", LastName: " + LastName + ", Email: " + Email + ", Password: " + Password + ", PhoneNumber: " + PhoneNumber;
    }
}