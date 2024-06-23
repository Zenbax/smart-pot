using YourApiNamespace.Controllers;

namespace Socket;

using System;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using MongoDB.Driver;
using Domain.Model;

public class DataProcessing
{
    public static void HandleClient(Socket handler, IMongoCollection<Pot> potCollection, IMongoCollection<SensorData> sensorDataCollection)
    {
        handler.ReceiveTimeout = 15000;  // Set timeout for receiving data to 15 seconds
        try
        {
            StringBuilder data = new StringBuilder();
            byte[] bytes = new byte[1024];
            int bytesRec;
            while ((bytesRec = handler.Receive(bytes)) > 0)
            {
                string part = Encoding.ASCII.GetString(bytes, 0, bytesRec);
                data.Append(part);
                if (part.EndsWith("\n") || part.EndsWith("}"))
                {
                    ProcessData(data.ToString(), handler, potCollection, sensorDataCollection);
                    data.Clear();
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

 public static void ProcessData(string data, Socket handler, IMongoCollection<Pot> potCollection, IMongoCollection<SensorData> sensorDataCollection)
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
            //zz
        }
    }
}
