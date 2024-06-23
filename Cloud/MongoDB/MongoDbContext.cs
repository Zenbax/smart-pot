using Domain.Model;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using YourApiNamespace.Controllers;

namespace MongoDB;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;
    private readonly ILogger<MongoDbContext> _logger;
    //zz

    public MongoDbContext(MongoDbSettings settings, ILogger<MongoDbContext> logger)
    {
        var client = new MongoClient(settings.ConnectionString);
        _database = client.GetDatabase(settings.DatabaseName);
        _logger = logger;

        _logger.LogInformation($"Connected to MongoDB: {settings.DatabaseName} at {settings.ConnectionString}");
    }

    public IMongoCollection<User> Users => _database.GetCollection<User>("Users");
    public IMongoCollection<Pot> Pots => _database.GetCollection<Pot>("Pots");
    public IMongoCollection<Plant> Plants => _database.GetCollection<Plant>("Plants");
    public IMongoCollection<SensorData> SensorData => _database.GetCollection<SensorData>("SensorData");
}