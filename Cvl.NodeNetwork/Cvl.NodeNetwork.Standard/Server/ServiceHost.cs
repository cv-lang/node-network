using Cvl.NodeNetwork.Communication.TransportLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cvl.NodeNetwork.Server
{
    public class ServiceHost
    {
        private static Dictionary<string, object> services = new Dictionary<string, object>();

        public static Response Invoke(Request request)
        {
            object serviceInstance = null;
            System.Reflection.MethodInfo methodInfo = null;

            var service = services[request.ContractTypeFullName];
            if(service is Type serviceType)
            {
                serviceInstance = Activator.CreateInstance(serviceType);
                methodInfo = serviceType.GetMethod(request.MethodName);
            }
            else
            {
                serviceInstance = service;
                methodInfo = serviceInstance.GetType().GetMethod(request.MethodName);
            }

            
            var resultData = methodInfo.Invoke(serviceInstance, request.Arguments.ToArray());
            var response = new Response();
            response.ReturnValue = resultData;

            return response;
        }

        public static void RegisterService<TContract>(Type serviceType)
        {
            var contractFullName = typeof(TContract).FullName;
            services[contractFullName] = serviceType;
        }
    }
}
