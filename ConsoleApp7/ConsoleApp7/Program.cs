using System;
using System.Net;
using System.Text;
using System.Net.Sockets;
class Program
{
    static async Task Main(string[] args) // CLIENT
    {
        using var tcpClient = new Socket(
            AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp
            );
        try
        {
            await tcpClient.ConnectAsync("127.0.0.1", 8888);
            byte[] data = new byte[512];

            while (true)
            {
                Console.WriteLine("You: ");
                string serverMessage = Console.ReadLine() ?? string.Empty;
                byte[] buffer = Encoding.UTF8.GetBytes(serverMessage);
                await tcpClient.SendAsync(buffer);
                if (serverMessage.Equals("exit"))
                {
                    Console.WriteLine("End works the server");
                    break;
                }



                int reciveBytes = await tcpClient.ReceiveAsync(data);
                string clientMessage = Encoding.UTF8.GetString(data, 0, reciveBytes);
                Console.WriteLine($"The client sent message {clientMessage}");
                if (clientMessage.Equals("exit"))
                {
                    Console.WriteLine($"The client turn of");
                    break;
                }

            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exeption {ex.Message}");
        }
    }
}