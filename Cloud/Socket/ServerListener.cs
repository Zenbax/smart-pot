using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
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
                Console.WriteLine("Server is waiting for a connection...");

                while (true)
                {
                    // Accept a new connection
                    var handler = listener.Accept();
                    // Create a new thread for handling the client
                    Thread clientThread = new Thread(() => HandleClient(handler));
                    clientThread.Start();
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Server start error: {e.Message}");
        }
    }

    private static void HandleClient(System.Net.Sockets.Socket handler)
    {
        Console.WriteLine("Client connected.");
        try
        {
            // Set the receive timeout to 30000 milliseconds (30 seconds)
            handler.ReceiveTimeout = 30000;

            StringBuilder data = new StringBuilder();
            while (true)
            {
                byte[] bytes = new byte[1024];
                int bytesRec = handler.Receive(bytes); // This will throw a SocketException if the timeout is exceeded
                if (bytesRec == 0) break;  // Client has closed the connection

                string part = Encoding.ASCII.GetString(bytes, 0, bytesRec);
                data.Append(part);

                if (part.Contains("\n"))
                {
                    var fullData = data.ToString();
                    var messages = fullData.Split('\n');
                    foreach (var message in messages)
                    {
                        if (!string.IsNullOrWhiteSpace(message))
                        {
                            if (message.Trim().Equals("quit", StringComparison.OrdinalIgnoreCase))
                            {
                                Console.WriteLine("Quit command received, closing connection.");
                                break;
                            }
                            Console.WriteLine("Text received: {0}", message);
                            ProcessData(message.Trim());
                        }
                    }
                    data.Clear();  // Clear the data for the next message
                    if (part.Trim().Equals("quit", StringComparison.OrdinalIgnoreCase)) break;
                }
            }
            byte[] msg = Encoding.ASCII.GetBytes("Data processed\n");
            handler.Send(msg);
            handler.Shutdown(SocketShutdown.Both);
            handler.Close();
            Console.WriteLine("Connection closed.");
        }
        catch (SocketException se)
        {
            if (se.SocketErrorCode == SocketError.TimedOut)
            {
                Console.WriteLine("No data received within 30 seconds, connection closed.");
            }
            else
            {
                Console.WriteLine($"Socket exception: {se.Message}");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"An exception occurred while handling client: {e.Message}");
        }
        finally
        {
            if (handler.Connected)
            {
                handler.Shutdown(SocketShutdown.Both);
                handler.Close();
            }
        }
    }

    private static void ProcessData(string data)
    {
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
