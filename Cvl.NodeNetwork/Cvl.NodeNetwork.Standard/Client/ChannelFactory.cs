using Castle.DynamicProxy;
using Cvl.NodeNetwork.Communication.TransportLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cvl.NodeNetwork.Client
{
    public class ChannelFactory : IDisposable
    {
        internal string ServiceId { get; set; }
        public void Dispose()
        {
            
        }
    }

    public class ChannelFactory<T> : ChannelFactory
        where T:class
    {
        public ChannelFactory(string serviceEndpointUrl, string serviceId=null)
        {
            this.serviceEndpointUrl = serviceEndpointUrl;
            ServiceId = serviceId;
        }

        private string serviceEndpointUrl;

        public T CreateChannel()
        {
            var transport = createTransportLayer(serviceEndpointUrl);
            var proxyConnection = new InterceptorProxy<T>(this, transport);

            var generator = new ProxyGenerator();
            var proxy = generator.CreateInterfaceProxyWithoutTarget<T>(proxyConnection);
            return proxy;
        }

        private BaseTransportLayer createTransportLayer(string url)
        {
            if (url.StartsWith("http"))
            {
                return new RestTransportLayer(url);
            } else if (url.StartsWith("nodenetwork://"))
            {
                url = url.Replace("nodenetwork://", "");
                return new NodeNetworkTransportLayer(url);
            }

            throw new NotImplementedException($"Not implemented transport layer: {url}");
        }
    }
}
