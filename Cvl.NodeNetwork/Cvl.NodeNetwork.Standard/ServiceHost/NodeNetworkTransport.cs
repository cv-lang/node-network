using Cvl.NodeNetwork.Client;
using Cvl.NodeNetwork.Server.Hub;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.NodeNetwork.ServiceHost
{
    public class NodeNetworkTransport
    {
        public async Task ConnectServiceToNetwork(string url)
        {
            var signalRUrl = url + "/NodeNetworkHub";
            var nodenetworkControllerUrl = url;

            HubConnection connection = new HubConnectionBuilder()
                .WithUrl(signalRUrl)
                .Build();

            //obsługa reconnect
            connection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await connection.StartAsync();
            };

            //obsługa otrzymania zgłoszenia od głównego węzła
            connection.On<string>("RequestNotification",
                (requestId) =>
            {
                //mamy powiadomienie od Huba że mamy requesta

                Console.WriteLine(requestId);

                var idRequest = Guid.Parse(requestId);

                var endpoint = nodenetworkControllerUrl;

                using (var mychannelFactory = new ChannelFactory<INodeNetworkHubService>(endpoint))
                {
                    var serviceProxy = mychannelFactory.CreateChannel();
                    var request = serviceProxy.GetRequest(idRequest);
                    var response = request.Invoke().Result;
                    serviceProxy.SetResponse(response, idRequest);
                }
            });

            var t = connection.ConnectionId;
            await connection.StartAsync();

            await connectToNodeNetwork(connection);


            Console.WriteLine("Ok");
        }

        private async Task connectToNodeNetwork(HubConnection connection)
        {
            var registeredServiceContractsXml = NodeNetworkServiceHost.GetXmlOfRegisteredServiceContract();

            await connection.SendAsync("ConnectToNodeNetwork", registeredServiceContractsXml);
        }
    }
}
