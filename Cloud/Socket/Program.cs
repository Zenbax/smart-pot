using System;
using MongoDB.Driver;
using dotenv.net;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Starting socket !");
        
        // Load environment variables from .env file
        DotEnv.Load(options: new DotEnvOptions(envFilePaths: new[] { ".env" }, ignoreExceptions: false));
        
        // Initialize MongoDB
        InitializeMongoDB();
        
        ServerListener.StartServer();
    }
    
    private static void InitializeMongoDB()
    {
        try
        {
            string connectionString = Environment.GetEnvironmentVariable("MONGODB_CONNECTION_STRING");
            string databaseName = Environment.GetEnvironmentVariable("MONGODB_DATABASE_NAME");

            
            var client = new MongoClient(connectionString); 
            client.GetDatabase(databaseName);
            
            Console.WriteLine("MongoDB connected.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Failed to connect to MongoDB: " + ex.Message);
        }
    }
}