using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using MongoDB.Driver;
using Domain.Model;
using Socket.Models;
using YourApiNamespace.Controllers;

public class ServerListener
{
    private static IMongoCollection<Pot> potCollection;
    private static IMongoCollection<Plant> plantCollection;
    private static IMongoCollection<SensorData> sensorDataCollection;

    public static void StartServer()
    {
        var client = new MongoClient(Environment.GetEnvironmentVariable("MONGODB_CONNECTION_STRING"));
        var database = client.GetDatabase(Environment.GetEnvironmentVariable("MONGODB_DATABASE_NAME"));
        potCollection = database.GetCollection<Pot>("Pots");
        plantCollection = database.GetCollection<Plant>("Plants");
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
                Thread clientThread = new Thread(() => HandleClient(handler));
                clientThread.Start();
            }
        }
    }

    private static void HandleClient(System.Net.Sockets.Socket handler)
    {
        handler.ReceiveTimeout = 15000;
        try
        {
            StringBuilder data = new StringBuilder();
            while (true)
            {
                byte[] bytes = new byte[1024];
                int bytesRec = handler.Receive(bytes);
                if (bytesRec > 0)
                {
                    string part = Encoding.ASCII.GetString(bytes, 0, bytesRec);
                    data.Append(part);
                    if (part.EndsWith("\n") || part.EndsWith("}"))
                    {
                        ProcessData(data.ToString(), handler);
                        data.Clear();
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
        }
        catch (SocketException e)
        {
            Console.WriteLine($"Socket exception: {e.Message}");
        }
        finally
        {
            handler.Shutdown(SocketShutdown.Both);
            handler.Close();
            Console.WriteLine("Connection closed.");
        }
    }

    private static void ProcessData(string data, System.Net.Sockets.Socket handler)
    {
        string filteredData = new string(data.Where(c => !char.IsControl(c) || char.IsWhiteSpace(c)).ToArray());
        if (string.IsNullOrWhiteSpace(filteredData))
        {
            byte[] msg = Encoding.ASCII.GetBytes("No data received\n");
            handler.Send(msg);
            return;
        }

        try
        {
            var sensorData = JsonSerializer.Deserialize<SensorData>(filteredData);
            if (sensorData == null)
            {
                byte[] msg = Encoding.ASCII.GetBytes("Invalid data format\n");
                handler.Send(msg);
                return;
            }

            sensorData.Timestamp = DateTime.UtcNow;
            var pot = potCollection.Find(p => p.MachineID == sensorData.MachineID).FirstOrDefault();

            if (pot == null)
            {
                byte[] msg = Encoding.ASCII.GetBytes($"No pot found for MachineID {sensorData.MachineID}\n");
                handler.Send(msg);
                return;
            }

            sensorData.PotId = pot.Id;
            sensorData.PlantId = pot.PlantId; // Ensure PlantId is stored in sensorData

            var plant = plantCollection.Find(p => p.Id == pot.PlantId).FirstOrDefault();
            if (plant == null)
            {
                byte[] msg = Encoding.ASCII.GetBytes("No plant data available\n");
                handler.Send(msg);
                return;
            }

            bool watering = pot.Enable && sensorData.MeasuredSoilMoisture > plant.SoilMinimumMoisture;
            sensorData.AmountOfWateringGiven = watering ? plant.AmountOfWateringToBeGiven : 0; // Update amount based on watering requirement

            // Save sensor data with updated AmountOfWateringGiven value
            sensorDataCollection.InsertOne(sensorData);

            var iotDataResponse = new
            {
                MachineId = pot.MachineID,
                Watering = watering
            };

            byte[] response = Encoding.ASCII.GetBytes(JsonSerializer.Serialize(iotDataResponse) + "\n");
            handler.Send(response);
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"Failed to parse JSON data: {ex.Message}");
            byte[] msg = Encoding.ASCII.GetBytes($"JSON parse error: {ex.Message}\n");
            handler.Send(msg);
        }
    }
}
