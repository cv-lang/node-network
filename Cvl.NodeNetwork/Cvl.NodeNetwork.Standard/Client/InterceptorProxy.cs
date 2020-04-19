using Castle.DynamicProxy;
using Cvl.NodeNetwork.Communication.TransportLayer;
using Cvl.NodeNetwork.Communication.TransportLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cvl.NodeNetwork.Client
{
    public class InterceptorProxy<T> : IInterceptor
        where T : class
    {
        private ChannelFactory channelFactory;
        private BaseTransportLayer transportLayer;

        public InterceptorProxy(ChannelFactory channelFactory, BaseTransportLayer transportLayer)
        {
            this.channelFactory = channelFactory;
            this.transportLayer = transportLayer;
        }

        public void Intercept(IInvocation invocation)
        {
            var request = new Request();
            request.ServiceId = channelFactory.ServiceId;
            request.MethodName = invocation.Method.Name;
            request.ServiceContractTypeFullName = typeof(T).FullName;
            request.Arguments = invocation.Arguments.ToList();

            var respond = transportLayer.SendRequest(request);
            invocation.ReturnValue = respond.ReturnValue;
        }
    }
}
