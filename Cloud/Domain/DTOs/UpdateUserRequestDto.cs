using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.DTOs;

public class UpdateUserRequestDto
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public string Name { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public string PhoneNumber { get; set; }
    
    public UpdateUserRequestDto()
    {
    }
    
    public UpdateUserRequestDto(string id, string name, string lastName, string email, string password, string phoneNumber)
    {
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