using System;
using MongoDB.Driver;
using dotenv.net;
using Socket;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Starting socket !");
        
        // Use the generic database service
        IDatabaseService databaseService = new MongoDBService();

        // Initialize database connection
        InitializeDatabase(databaseService);
        
        ServerListener.StartServer(databaseService);
    }
    
    private static void InitializeDatabase(IDatabaseService databaseService)
    {
        try
        {
            databaseService.Connect();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Failed to initialize database: " + ex.Message);
        }
    }
}