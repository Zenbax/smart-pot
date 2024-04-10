using MongoDB.Driver;
using MongoDB.Bson;

public class DatabaseHandler
{
    private IMongoCollection<MyDataType> _collection;

    public DatabaseHandler(string connectionString, string dbName, string collectionName)
    {
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase(dbName);
        _collection = database.GetCollection<MyDataType>(collectionName);
    }

    public void InsertData(MyDataType data)
    {
        _collection.InsertOne(data);
    }

    // Add additional methods to handle data retrieval, updates, and deletes as needed
}

public class MyDataType
{
    public ObjectId Id { get; set; }
    // Other properties to match the structure of your documents
}