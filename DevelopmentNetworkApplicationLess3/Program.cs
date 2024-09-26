using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevelopmentNetworkApplicationLess3
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello, enter your name: ");

            string nickName = Console.ReadLine();

            while (String.IsNullOrEmpty(nickName))
            {
                Console.WriteLine("Enter your name: ");
                nickName = Console.ReadLine();
            }

            await Client.SendMessage(nickName);
        }
    }
}