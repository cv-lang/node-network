using Castle.DynamicProxy;
using Cvl.NodeNetwork.Communication.TransportLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cvl.NodeNetwork.Client
{
    public class ChannelFactory : IDisposable
    {
        public void Dispose()
        {
            
        }
    }

    public class ChannelFactory<T> : ChannelFactory
        where T:class
    {
        private string serviceEndpointUrl;
        public ChannelFactory(string ServiceEndpointUrl)
        {
            serviceEndpointUrl = ServiceEndpointUrl;
        }

        public T CreateChannel()
        {
            var proxyConnection = new InterceptorProxy<T>(this, new RestTransportLayer(serviceEndpointUrl));

            var generator = new ProxyGenerator();
            var proxy = generator.CreateInterfaceProxyWithoutTarget<T>(proxyConnection);
            return proxy;
        }
    }
}
