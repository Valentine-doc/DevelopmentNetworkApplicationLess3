using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using DevelopmentNetworkApplicationLess3;

namespace DevelopmentNetworkApplicationLess3
{
    internal class Client
    {
        internal static async Task SendMessage(string name)
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 0);
            UdpClient udpClient = new UdpClient();

            while (true)
            {
                Console.WriteLine("Enter message or Exit to exit");

                string message1 = Console.ReadLine();

                if (String.IsNullOrEmpty(message1))
                {
                    Console.WriteLine("Message cannot be empty");
                    continue;
                }

                else if (message1.ToLower() == "exit")
                {
                    User userClientToExit = new User(name, message1);
                    userClientToExit.CancellationTokenSource.Cancel();
                    var jsonToExit = userClientToExit.ToJson();
                    await udpClient.SendAsync(System.Text.Encoding.UTF8.GetBytes(jsonToExit), jsonToExit.Length, ep);
                    udpClient.Close();
                    break;
                }
                else
                {
                    User userClient = new User(name, message1);
                    var jsonToS = userClient.ToJson();
                    await udpClient.SendAsync(System.Text.Encoding.UTF8.GetBytes(jsonToS), jsonToS.Length, ep);
                    var answer = await udpClient.ReceiveAsync();

                    if (answer.Buffer.Length == 0 || answer.Buffer == null)
                    {
                        Console.WriteLine("Server is not responding");
                    }
                    else
                    {
                        string replyMessage = System.Text.Encoding.UTF8.GetString(answer.Buffer);
                        User userServer = User.FromJson(replyMessage);
                        Console.WriteLine(userServer.ToString());
                    }
                }

            }
        }
    }
}