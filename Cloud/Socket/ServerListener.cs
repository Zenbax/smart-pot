using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using MongoDB.Driver;
using System.Text.Json;
using Socket.Models;

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
            using (var listener = new System.Net.Sockets.Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                listener.Bind(localEndPoint);
                listener.Listen(10);

                Console.WriteLine("Waiting for a connection...");

                while (true)
                {
                    using (var handler = listener.Accept())
                    {
                        StringBuilder data = new StringBuilder();
                        byte[] bytes = new byte[1024];
                        int bytesRec = 0;

                        // Continuously read data from the client
                        while (handler.Connected)
                        {
                            if (handler.Available > 0)
                            {
                                bytesRec = handler.Receive(bytes);
                                string part = Encoding.ASCII.GetString(bytes, 0, bytesRec);
                                data.Append(part);

                                if (part.Contains("\n"))  // Check if the end marker (newline) is in the string
                                {
                                    var fullData = data.ToString();
                                    var messages = fullData.Split('\n');
                                    foreach (var message in messages)
                                    {
                                        if (!string.IsNullOrWhiteSpace(message))
                                        {
                                            Console.WriteLine("Text received: {0}", message);
                                            ProcessData(message.Trim());
                                        }
                                    }
                                    data.Clear();  // Clear the data for the next message
                                }
                            }
                        }

                        byte[] msg = Encoding.ASCII.GetBytes("Data processed");
                        handler.Send(msg);
                        handler.Shutdown(SocketShutdown.Both);
                        handler.Close();
                    }
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }

    private static void ProcessData(string data)
    {
        // Filter out non-printable characters except for typical whitespace used in JSON
        string filteredData = new string(data.Where(c => !char.IsControl(c) || char.IsWhiteSpace(c)).ToArray());

        if (string.IsNullOrWhiteSpace(filteredData))
        {
            Console.WriteLine("No valid data received to process.");
            return;
        }

        try
        {
            using (JsonDocument doc = JsonDocument.Parse(filteredData))
            {
                var sensorType = doc.RootElement.GetProperty("type").GetString();
                var value = doc.RootElement.GetProperty("value").GetString();
                if (double.TryParse(value, out double parsedValue))
                {
                    SaveData(new SensorData { SensorType = sensorType, Value = parsedValue });
                    Console.WriteLine($"Processed and saved data: Type = {sensorType}, Value = {parsedValue}");
                }
                else
                {
                    Console.WriteLine($"Unable to parse the value '{value}' as double.");
                }
            }
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"Failed to parse JSON data: {ex.Message}");
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
