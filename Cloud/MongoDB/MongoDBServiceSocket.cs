using dotenv.net;
using MongoDB.Driver;

namespace MongoDB;

public class MongoDBServiceSocket : IDatabaseService
{
    private string _connectionString;
    private string _databaseName;

    public MongoDBServiceSocket()
    {
        // Load environment variables from .env file
        DotEnv.Load(options: new DotEnvOptions(envFilePaths: new[] { ".env" }, ignoreExceptions: false));
        _connectionString = Environment.GetEnvironmentVariable("MONGODB_CONNECTION_STRING");
        _databaseName = Environment.GetEnvironmentVariable("MONGODB_DATABASE_NAME");
    }

    public void Connect()
    {
        try
        {
            var client = new MongoClient(_connectionString);
            client.GetDatabase(_databaseName);
            Console.WriteLine("MongoDB connected.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Failed to connect to MongoDB: " + ex.Message);
            throw;
        }
    }
}