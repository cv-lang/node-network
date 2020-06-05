using Cvl.NodeNetwork.Test;
using System;
using Cvl.NodeNetwork.ServiceHost;
using System.Threading.Tasks;

namespace TestApp1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //waiting until hub is started
            await Task.Delay(1000);

            Console.WriteLine("ServiceHost - register service");
            // część Hosta serwisu
            //rejestruje serwis testowy w sieci (w HUBie)
            NodeNetworkServiceHost.RegisterService<TestService, ITestService>();
            await NodeNetworkServiceHost.ConnectToNodeNetworkHub("https://localhost:44331");

            Console.ReadKey();
        }        
    }
}
