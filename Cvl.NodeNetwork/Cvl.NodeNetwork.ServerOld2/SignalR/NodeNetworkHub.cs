using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cvl.NodeNetwork.Server;
using Cvl.NodeNetwork.Server.Hub;
using Cvl.NodeNetwork.Tools;

namespace Cvl.NodeNetwork.Server.SignalR
{
    public class NodeNetworkHub : Microsoft.AspNetCore.SignalR.Hub
    {
        public Task SendMessage(string user, string message)
        {

            return Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }


        private static List<ServiceReference> registeredServiceContracts
            = new List<ServiceReference>();

        public Task ConnectToNodeNetwork(string registeredServiceContractsXml)
        {
            var list = Serializer.DeserializeObject<List<ServiceReference>>(registeredServiceContractsXml);
            foreach (var serviceReference in list)
            {
                serviceReference.NodeNetworkServieHostAddress = this.Context.ConnectionId;
            }
            registeredServiceContracts.AddRange(list);

            return Clients.Caller.SendAsync("Ok");
        }


        public static void SendNotificationToServiceHost(
            string serviceContractTypeFullName,
            string serviceId,
            Guid requestId,
            IHubContext<NodeNetworkHub> _strongChatHubContext)
        {
            var serviceReference = registeredServiceContracts
                .Single(x => x.ServiceId == serviceId &&
                             x.ServiceContractFullName == serviceContractTypeFullName);


            var client = _strongChatHubContext.Clients.Client(serviceReference.NodeNetworkServieHostAddress);
            client.SendAsync("RequestNotification", requestId.ToString());
        }
    }
}
