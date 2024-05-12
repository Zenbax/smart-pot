using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using MongoDB.Driver;
using System.Text.Json;
using Socket.Models;
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
                Thread clientThread = new Thread(() => HandleClient(handler));
                clientThread.Start();
            }
        }
    }

  
    
    
    
    private static void HandleClient(System.Net.Sockets.Socket handler)
    {
        handler.ReceiveTimeout = 15000;  // Set timeout for receiving data to 15 seconds
        try
        {
            while (true) // Continue processing until the connection is closed by client
            {
                StringBuilder data = new StringBuilder();
                byte[] bytes = new byte[1024];
                int bytesRec = handler.Receive(bytes);
                if (bytesRec > 0)
                {
                    string part = Encoding.ASCII.GetString(bytes, 0, bytesRec);
                    data.Append(part);
                    if (part.EndsWith("\n") || part.EndsWith("}"))  // Assumed end of one complete message
                    {
                        ProcessData(data.ToString(), handler);
                        data.Clear();  // Clear the data buffer after processing
                        Thread.Sleep(5000);  // Wait for 5 seconds before closing the connection
                        break;
                    }
                }
                else
                {
                    break;  // If no data is received, break the loop and close the socket
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
            Console.WriteLine("No valid data received to process.");
            byte[] msg = Encoding.ASCII.GetBytes("No data received\n");
            handler.Send(msg);
            return;
        }

        try
        {
            var sensorData = JsonSerializer.Deserialize<SensorData>(filteredData);

            // Set the current DateTime as the timestamp
            if (sensorData != null)
            {
                sensorData.Timestamp = DateTime.UtcNow; // Automatically set the timestamp here
                Console.WriteLine("Sensor data saved to MongoDB with current Timestamp.");

                // Find the pot with the given MachineID
                var pot = potCollection.Find(p => p.MachineID == sensorData.MachineID).FirstOrDefault();
                if (pot != null)
                {
                    // Add the PotId and PlantId to the sensor data
                    sensorData.PotId = pot.Id;
                    if (pot.PlantId != null)
                    {
                        sensorData.PlantId = pot.PlantId; 
                    }

                    sensorDataCollection.InsertOne(sensorData);
                    Console.WriteLine("Sensor data saved to MongoDB with current Timestamp, PotId, and PlantId.");

                    var potJson = JsonSerializer.Serialize(pot);
                    byte[] msg = Encoding.ASCII.GetBytes(potJson);
                    handler.Send(msg);
                }
                else
                {
                    Console.WriteLine($"No pot found for MachineID {sensorData.MachineID}");
                    byte[] msg = Encoding.ASCII.GetBytes($"No pot found for MachineID {sensorData.MachineID}\n");
                    handler.Send(msg);
                }
            }
            else
            {
                byte[] msg = Encoding.ASCII.GetBytes("Invalid data format\n");
                handler.Send(msg);
            }
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"Failed to parse JSON data: {ex.Message}");
            byte[] msg = Encoding.ASCII.GetBytes($"JSON parse error: {ex.Message}\n");
            handler.Send(msg);
        }
    }


    
    
    
}
