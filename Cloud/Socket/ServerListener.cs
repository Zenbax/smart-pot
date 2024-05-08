using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using MongoDB.Driver;
using System.Text.Json;
using Socket.Models;
using YourApiNamespace.Controllers;

public class ServerListener
{
    private static IMongoCollection<Pot> potCollection;
    private static IMongoCollection<SensorData> sensorDataCollection;  // Declare the collection for SensorData

    public static void StartServer()
    {
        var client = new MongoClient(Environment.GetEnvironmentVariable("MONGODB_CONNECTION_STRING"));
        var database = client.GetDatabase(Environment.GetEnvironmentVariable("MONGODB_DATABASE_NAME"));
        potCollection = database.GetCollection<Pot>("Pots");
        sensorDataCollection = database.GetCollection<SensorData>("SensorData");  // Initialize the collection

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
                Thread clientThread = new Thread(() => HandleClient(handler));
                clientThread.Start();
            }
        }
    }

    private static void HandleClient(System.Net.Sockets.Socket handler)
    {
        StringBuilder data = new StringBuilder();
        byte[] bytes = new byte[1024];
        int bytesRec;

        try {
            while ((bytesRec = handler.Receive(bytes)) > 0) {
                string part = Encoding.ASCII.GetString(bytes, 0, bytesRec);
                data.Append(part);
                // Break the loop if JSON ends (you assume it ends with a newline or close brace)
                if (part.EndsWith("\n") || part.EndsWith("}"))
                    break;
            }

            string fullData = data.ToString();
            Console.WriteLine("Text received: {0}", fullData);
            ProcessData(fullData, handler);  // Pass handler as a second argument to ProcessData
            handler.Shutdown(SocketShutdown.Both);
            handler.Close();
        } catch (Exception e) {
            Console.WriteLine($"An exception occurred while handling client: {e.Message}");
        }
    }



    private static void ProcessData(string data, System.Net.Sockets.Socket handler)
    {
        string filteredData = new string(data.Where(c => !char.IsControl(c) || char.IsWhiteSpace(c)).ToArray());

        if (string.IsNullOrWhiteSpace(filteredData))
        {
            Console.WriteLine("No valid data received to process.");
            return;
        }

        try
        {
            var sensorData = JsonSerializer.Deserialize<SensorData>(filteredData); // Deserialize the JSON into a SensorData object
            sensorDataCollection.InsertOne(sensorData); // Save the sensor data to MongoDB
            Console.WriteLine("Sensor data saved to MongoDB.");

            // After saving, find the corresponding pot and send it back to the client
            var pot = potCollection.Find(p => p.MachineID == sensorData.MachineID).FirstOrDefault();
            if (pot != null)
            {
                var potJson = JsonSerializer.Serialize(pot);
                byte[] msg = Encoding.ASCII.GetBytes(potJson);
                handler.Send(msg);
                Console.WriteLine("Sent pot data back to client.");
            }
            else
            {
                Console.WriteLine("Pot not found.");
                byte[] msg = Encoding.ASCII.GetBytes("Pot not found\n");
                handler.Send(msg);
            }
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"Failed to parse JSON data: {ex.Message}");
        }
    }
}
