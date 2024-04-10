using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class ServerListener
{
    private const int Port = 11000; // Define the port at a class level

    public static int Main(string[] args)
    {
        StartServer();
        return 0;
    }

    public static void StartServer()
    {
        IPAddress ipAddress = IPAddress.Any; // Listen on all network interfaces
        IPEndPoint localEndPoint = new IPEndPoint(ipAddress, Port);

        try
        {
            // Create a Socket that will use Tcp protocol
            Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            listener.Bind(localEndPoint);
            listener.Listen(10); // Backlog set to 10 connections

            Console.WriteLine("Waiting for a connection...");

            while (true) // Continuously accept clients
            {
                Socket handler = listener.Accept(); // Accept a connection. This is a blocking call.
                
                // Incoming data from the client.
                string data = null;
                byte[] bytes;

                while (true)
                {
                    bytes = new byte[1024];
                    int bytesRec = handler.Receive(bytes);
                    data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                    if (data.IndexOf("<EOF>") > -1) // Checks if the end-of-file tag is in the string
                    {
                        break;
                    }
                }

                data = data.Replace("<EOF>", ""); // Removes the termination sequence before processing
                Console.WriteLine("Text received: {0}", data);

                byte[] msg = Encoding.ASCII.GetBytes(data);
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
}
