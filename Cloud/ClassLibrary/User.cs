public class User
{
    // The ObjectId is represented as a string here for simplicity, but you can also use MongoDB.Bson.ObjectId
    public string Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}