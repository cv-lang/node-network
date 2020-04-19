using System;
using System.Collections.Generic;
using System.Text;
using Cvl.NodeNetwork.Communication.TransportLayer.Model;

namespace Cvl.NodeNetwork.Server.Hub
{
    public interface INodeNetworkHubService
    {
        Request GetRequest(Guid requestId);
        void SetResponse(Response response, Guid requestId);
    }
}
