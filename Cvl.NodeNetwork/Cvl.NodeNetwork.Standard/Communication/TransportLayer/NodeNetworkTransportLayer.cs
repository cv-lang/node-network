using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Cvl.NodeNetwork.Client;
using Cvl.NodeNetwork.Communication.TransportLayer.Model;
using Cvl.NodeNetwork.Server.Hub;
using Cvl.NodeNetwork.Test;

namespace Cvl.NodeNetwork.Communication.TransportLayer
{
    /// <summary>
    /// Transport po sieci wezłowej, używa hub'a do komunikacji
    /// pozwala na wystawianie serwisów za NATem
    /// wewnętrznie komunikuje się przez inne warstwy transportu
    /// </summary>
    public class NodeNetworkTransportLayer : RestTransportLayer
    {
        public NodeNetworkTransportLayer(string serviceEndpointUrl):
            base(serviceEndpointUrl)
        {  
        }

        public override Response SendRequest(Request request)
        {
            var nodeNetworkRequest = new NodeNetworkRequest();
            nodeNetworkRequest.OriginalRequest = request;
            return base.SendRequest(nodeNetworkRequest);
        }

        //internal override byte[] SendRequestTransportLayer(byte[] binaryRequestData)
        //{
        //    using (var channelFactory = new ChannelFactory<IHubService>(hubEndpointUrl))
        //    {
        //        var hubServiceProxy = channelFactory.CreateChannel();
        //        var ret = hubServiceProxy.ExecuteCommand(binaryRequestData);
        //        return ret;
        //    }
        //}
    }
}
