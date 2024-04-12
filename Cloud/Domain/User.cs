namespace Domain;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class User
{
    
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
    
    public User()
    {
    }
    public User(string id, string name, string lastName, string email, string password, string phoneNumber)
    {
        Id = id;
        Name = name;
        LastName = lastName;
        Email = email;
        Password = password;
        PhoneNumber = phoneNumber;
    }
    
    
}