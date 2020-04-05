using Cvl.NodeNetwork.Client;
using Cvl.NodeNetwork.Test;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Text;
using System.Threading.Tasks;

namespace TestApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //Console.ReadKey();
            var endpoint = "https://localhost:44331/NodeNetwork";

            using (var mychannelFactory = new ChannelFactory<ITestService>(endpoint))
            {
                var serviceProxy = mychannelFactory.CreateChannel();
                var ret = serviceProxy.Ping(12);
            }


                Console.ReadKey();
        }

        //public async static string PobierzDaneSerwera(string request)
        //{
        //    var connection = new HubConnectionBuilder()
        //        .WithUrl("https://localhost:44331/hub")
        //        .Build();

        //    connection.Closed += async (error) =>
        //    {
        //        await Task.Delay(new Random().Next(0, 5) * 1000);
        //        await connection.StartAsync();
        //    };

        //    connection.On<string, string>("ReceiveMessage", (user, message) =>
        //    {
        //        var newMessage = $"{user}: {message.Length}";
        //        Console.WriteLine(newMessage);
        //    });

        //    await connection.StartAsync();

        //    var sb = new StringBuilder();

        //    for (int i = 0; i < 1000 * 1; i++)
        //    {
        //        sb.Append($" Tresc {i} ");
        //    }

        //    var wiadomosc = sb.ToString();

        //    var rozmiar = wiadomosc.Length;

        //    Console.WriteLine(rozmiar);

        //    await connection.SendAsync("SendMessage", "michal", wiadomosc);
           
        //}
    }
}
