
using System.Net;
using System.Net.Sockets;
using System.Text;
using MongoDB.Driver;
using System.Text.Json;
using Domain.Model;
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

        if (sensorData != null)
        {
            var pot = potCollection.Find(p => p.MachineID == sensorData.MachineID).FirstOrDefault();
            if (pot != null) // Check if pot is found
            {
                sensorData.PotId = pot.Id;
                if (pot.Plant != null)
                {
                    sensorData.PlantId = pot.Plant.Id;
                }

                // Store incoming sensor data
                sensorDataCollection.InsertOne(sensorData);
                Console.WriteLine("Sensor data saved to MongoDB with additional details.");

                // Prepare and send response back to IoT device including the Enable status
                var response = new {
                    MachineID = pot.MachineID,
                    SoilMinimumMoisture = pot.Plant?.SoilMinimumMoisture,
                    AmountOfWaterToBeGiven = pot.Plant?.AmountOfWaterToBeGiven,
                    Enable = pot.Enable // Include the Enable status of the pot
                };
                string jsonResponse = JsonSerializer.Serialize(response);
                byte[] msg = Encoding.ASCII.GetBytes(jsonResponse);
                handler.Send(msg);
            }
            else
            {
                Console.WriteLine($"Pot not found for MachineID {sensorData.MachineID}");
                byte[] msg = Encoding.ASCII.GetBytes("Pot not found\n");
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
