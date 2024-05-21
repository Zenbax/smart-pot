using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Domain.Model;
using MongoDB.Driver;
using Socket;
using YourApiNamespace.Controllers;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Domain.Model;
using MongoDB.Driver;
using Socket;
using YourApiNamespace.Controllers;

public class ServerListener
{
    private readonly IDatabaseService _databaseService;
    private IMongoCollection<Pot> potCollection;
    private IMongoCollection<SensorData> sensorDataCollection;

    public ServerListener(IDatabaseService databaseService)
    {
        _databaseService = databaseService;
        InitializeDatabase();
    }

    private void InitializeDatabase()
    {
        try
        {
            _databaseService.Connect();
            var client = new MongoClient(Environment.GetEnvironmentVariable("MONGODB_CONNECTION_STRING"));
            var database = client.GetDatabase(Environment.GetEnvironmentVariable("MONGODB_DATABASE_NAME"));
            potCollection = database.GetCollection<Pot>("Pots");
            sensorDataCollection = database.GetCollection<SensorData>("SensorData");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Failed to initialize database connection: " + ex.Message);
            throw;
        }
    }

    public static void StartServer(IDatabaseService databaseService)
    {
        var listener = new ServerListener(databaseService);
        int port = int.Parse(Environment.GetEnvironmentVariable("SOCKET_PORT") ?? "11000");
        IPAddress ipAddress = IPAddress.Any;
        IPEndPoint localEndPoint = new IPEndPoint(ipAddress, port);

        using (var socketListener = new System.Net.Sockets.Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
        {
            socketListener.Bind(localEndPoint);
            socketListener.Listen(10);
            Console.WriteLine("Server is waiting for a connection...");

            while (true)
            {
                var handler = socketListener.Accept();
                Console.WriteLine("Client connected.");
                Thread clientThread = new Thread(() => DataProcessing.HandleClient(handler, listener.potCollection, listener.sensorDataCollection));
                clientThread.Start();
            }
        }
    }
}
