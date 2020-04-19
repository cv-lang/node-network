using Cvl.NodeNetwork.Server;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Cvl.NodeNetwork.Server.Hub;

namespace Cvl.NodeNetwork.Communication.TransportLayer.Model
{
    public class NodeNetworkRequest : Request
    {
        public NodeNetworkRequest()
        {
            RequestId = Guid.NewGuid();
        }

        public Guid RequestId { get; set; }
        public Request OriginalRequest { get; internal set; }
        public IServiceHostNotification Notyfi { get; internal set; }

        
        public async override Task<Response> Invoke()
        {
            //jesteśmy w/na Hubie

            //ustawiam oryginalny request 
            NodeNetworkHubService.SetRequest(OriginalRequest, RequestId);

            //powiadamiam host serwisu
            Notyfi.Notification(OriginalRequest.ServiceContractTypeFullName, 
                OriginalRequest.ServiceId, RequestId);

            //czekam aż NodeNetworkServieHost prześle dane z serwisu
            while (NodeNetworkHubService.IsReadyResponse(RequestId) == false)
            {
                await Task.Delay(100);
            }

            return NodeNetworkHubService.GetResponse(RequestId);
        }
    }
}
