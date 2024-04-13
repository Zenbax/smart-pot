using System;
using System.Net;
using System.Net.Sockets;
using CustomSocket = System.Net.Sockets.Socket;
using System.Text;
using MongoDB.Driver;
using Socket.Models;
using System.Text.Json;

public class ServerListener
{
    private static IMongoCollection<SensorData> dataCollection;

    public static void StartServer()
    {
        // Initialize MongoDB collection
        int port = int.Parse(Environment.GetEnvironmentVariable("SOCKET_PORT") ?? "11000"); 
        var client = new MongoClient(Environment.GetEnvironmentVariable("MONGODB_CONNECTION_STRING"));
        var database = client.GetDatabase(Environment.GetEnvironmentVariable("MONGODB_DATABASE_NAME"));
        dataCollection = database.GetCollection<SensorData>("SensorData");

        IPAddress ipAddress = IPAddress.Any;
        IPEndPoint localEndPoint = new IPEndPoint(ipAddress, port);

        try
        {
            CustomSocket listener = new CustomSocket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            listener.Bind(localEndPoint);
            listener.Listen(10);

            Console.WriteLine("Waiting for a connection...");

            while (true)
            {
                CustomSocket handler = listener.Accept();
                string data = null;
                byte[] bytes;

                while (true)
                {
                    bytes = new byte[1024];
                    int bytesRec = handler.Receive(bytes);
                    data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                    if (data.IndexOf("<EOF>") > -1)
                    {
                        break;
                    }
                }

                data = data.Replace("<EOF>", "");
                Console.WriteLine("Text received: {0}", data);

                // Process the received data
                ProcessData(data);

                byte[] msg = Encoding.ASCII.GetBytes("Data processed");
                handler.Send(msg);
                handler.Shutdown(SocketShutdown.Both);
                handler.Close();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }

    private static void ProcessData(string data)
    {
        // Simulate parsing received data
        if (data.Contains("TEMP"))
        {
            var value = ExtractValue(data);
            SaveData(new SensorData { SensorType = "Temperature", Value = value });
        }
        else if (data.Contains("HUM"))
        {
            var value = ExtractValue(data);
            SaveData(new SensorData { SensorType = "Humidity", Value = value });
        }
        else if (data.Contains("WAT"))
        {
            var value = ExtractValue(data);
            SaveData(new SensorData { SensorType = "WaterLevel", Value = value });
        }
    }
    
    private static double ExtractValue(string data)
    {
        Console.WriteLine("Extracting value from data: " + data);
        try
        {
            using (JsonDocument doc = JsonDocument.Parse(data))
            {
                if (doc.RootElement.TryGetProperty("value", out JsonElement valueElement))
                {
                    // Check if the value is a string and convert it to double
                    if (valueElement.ValueKind == JsonValueKind.String)
                    {
                        string valueStr = valueElement.GetString();
                        if (double.TryParse(valueStr, out double result))
                        {
                            return result;
                        }
                        else
                        {
                            throw new FormatException("The value string is not in a correct format to convert to Double.");
                        }
                    }
                    else
                    {
                        // Directly get the value as double if it's stored as a number
                        return valueElement.GetDouble();
                    }
                }
                else
                {
                    throw new ArgumentException("Value key not found in JSON.");
                }
            }
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"Failed to parse JSON data: {ex.Message}");
            throw;
        }
    }

    
    private static void SaveData(SensorData sensorData)
    {
        try
        {
            dataCollection.InsertOne(sensorData);
            Console.WriteLine($"Saved {sensorData.SensorType} data to MongoDB.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Failed to save data: " + ex.Message);
        }
    }
}
