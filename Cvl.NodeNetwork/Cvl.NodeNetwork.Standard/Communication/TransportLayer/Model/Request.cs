using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Cvl.NodeNetwork.Server;
using Cvl.NodeNetwork.ServiceHost;

namespace Cvl.NodeNetwork.Communication.TransportLayer.Model
{
    public class Request
    {
        public string ServiceId { get; set; }
        public string ServiceContractTypeFullName { get; set; }
        public string MethodName { get; set; }
        public List<object> Arguments { get; set; }

        public virtual Task<Response> Invoke()
        {
            object serviceInstance = null;
            System.Reflection.MethodInfo methodInfo = null;

            var service = NodeNetworkServiceHost.GetServiceByContractFullName(ServiceContractTypeFullName, ServiceId);
            if (service is Type serviceType)
            {
                serviceInstance = Activator.CreateInstance(serviceType);
                methodInfo = serviceType.GetMethod(MethodName);
            }
            else
            {
                serviceInstance = service;
                methodInfo = serviceInstance.GetType().GetMethod(MethodName);
            }


            var resultData = methodInfo.Invoke(serviceInstance, Arguments.ToArray());
            var response = new Response();
            response.ReturnValue = resultData;

            return Task.FromResult(response);
        }
    }
}
