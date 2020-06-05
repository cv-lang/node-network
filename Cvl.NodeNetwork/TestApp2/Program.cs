using Cvl.NodeNetwork.Client;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Text;
using System.Threading.Tasks;
using Cvl.NodeNetwork.Server;
using Cvl.NodeNetwork.Server.Hub;
using Cvl.NodeNetwork.Test;
using Cvl.NodeNetwork.ServiceHost;

namespace TestApp2
{ 
    class Program
    {
        static async Task Main(string[] args)
        {
            //waiting until hub and service host is started
            await Task.Delay(2000);


            Console.WriteLine("Client");

            var endpoint = "nodenetwork://" + "https://localhost:44331";
            using (var mychannelFactory = new ChannelFactory<ITestService>(endpoint))
            {
                var serviceProxy = mychannelFactory.CreateChannel();
                var ret = serviceProxy.Ping(12);
            }

            Console.ReadKey();
        }
    }
}
