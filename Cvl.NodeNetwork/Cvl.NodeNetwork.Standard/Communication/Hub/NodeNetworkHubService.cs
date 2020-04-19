using Cvl.NodeNetwork.Communication.TransportLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cvl.NodeNetwork.Server.Hub
{
    /// <summary>
    /// Serwisy wykonywan na hubie
    /// </summary>
    public class NodeNetworkHubService : INodeNetworkHubService
    {
        private class ReqestContainerElement
        {
            public Request Request { get; set; }
            public Response Response { get; set; }
            public bool IsReadyResponse { get; set; }
        }

        private static Dictionary<Guid, ReqestContainerElement> reqests 
            = new Dictionary<Guid, ReqestContainerElement>();

        private ReqestContainerElement getReqestElement(Guid requestId)
        {
            return reqests[requestId];
        }

        public static void SetRequest(Request request, Guid requestId)
        {
            var element = new ReqestContainerElement();
            element.Request = request;
            reqests[requestId] = element;
        }

        internal static bool IsReadyResponse(Guid requestId)
        {
            return reqests[requestId].IsReadyResponse;
        }

        
        public Request GetRequest(Guid requestId)
        {
            var element = getReqestElement(requestId);
            return element.Request;
        }



        public void SetResponse(Response response, Guid requestId)
        {
            var element = getReqestElement(requestId);
            element.Response = response;
            element.IsReadyResponse = true;
        }

        internal static Response GetResponse(Guid requestId)
        {
            return reqests[requestId].Response;
        }

    }
}
