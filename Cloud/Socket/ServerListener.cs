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
    private static IMongoCollection<Pot> potCollection;
    private static IMongoCollection<SensorData> sensorDataCollection;

    public static void StartServer()
    {
        var client = new MongoClient(Environment.GetEnvironmentVariable("MONGODB_CONNECTION_STRING"));
        var database = client.GetDatabase(Environment.GetEnvironmentVariable("MONGODB_DATABASE_NAME"));
        potCollection = database.GetCollection<Pot>("Pots");
        sensorDataCollection = database.GetCollection<SensorData>("SensorData");

        int port = int.Parse(Environment.GetEnvironmentVariable("SOCKET_PORT") ?? "11000");
        IPAddress ipAddress = IPAddress.Any;
        IPEndPoint localEndPoint = new IPEndPoint(ipAddress, port);

        using (var listener = new System.Net.Sockets.Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
        {
            listener.Bind(localEndPoint);
            listener.Listen(10);
            Console.WriteLine("Server is waiting for a connection...");

            while (true)
            {
                var handler = listener.Accept();
                Console.WriteLine("Client connected.");
                Thread clientThread = new Thread(() => DataProcessing.HandleClient(handler, potCollection, sensorDataCollection));
                clientThread.Start();
            }
        }
    }
}