using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cvl.NodeNetwork.WebHub.SignalR
{
    public class NodeNetworkHub : Hub
    {
        public Task SendMessage(string user, string message)
        {

            return Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
