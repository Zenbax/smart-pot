using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class ServerListener
{
    private const int Port = 11000;
    public static void StartServer()
    {
        IPAddress ipAddress = IPAddress.Any;
        IPEndPoint localEndPoint = new IPEndPoint(ipAddress, Port);

        try
        {
            Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            listener.Bind(localEndPoint);
            listener.Listen(10);

            Console.WriteLine("Waiting for a connection...");

            while (true)
            {
                Socket handler = listener.Accept();
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
                
                // Send data to MongoDB 
                

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
        // Simulate JSON parsing (this is very basic and for demonstration only)
        if (data.Contains("TEMP"))
        {
            var value = ExtractValue(data);
            HandleTemperature(value);
        }
        else if (data.Contains("VAND"))
        {
            var value = ExtractValue(data);
            HandleWaterLevel(value);
        }
        else if (data.Contains("FUGT"))
        {
            var value = ExtractValue(data);
            HandleHumidity(value);
        }
    }

   
    
    private static int ExtractValue(string data)
    {
        try
        {
            string valueKeyword = "\"value\":";
            int startIndex = data.IndexOf(valueKeyword) + valueKeyword.Length;
            if (startIndex != -1)
            {
                int endIndex = data.IndexOf("}", startIndex);
                if (endIndex != -1)
                {
                    // Trim starter for at fjerne eventuelle førende og afsluttende mellemrum samt anførselstegn
                    string valueStr = data.Substring(startIndex, endIndex - startIndex).Trim().Trim('"');
                    return int.Parse(valueStr);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error parsing value: " + ex.Message);
        }
        throw new FormatException("Could not parse the value from data.");
    }



    private static void HandleTemperature(int value)
    {
        Console.WriteLine($"Temperature: {value} degrees");
        // Implement your logic here
    }

    private static void HandleWaterLevel(int value)
    {
        Console.WriteLine($"Water level: {value}%");
        // Implement your logic here
    }

    private static void HandleHumidity(int value)
    {
        Console.WriteLine($"Humidity: {value}%");
        // Implement your logic here
    }
}
