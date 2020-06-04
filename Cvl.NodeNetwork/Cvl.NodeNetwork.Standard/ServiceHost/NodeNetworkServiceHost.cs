using Cvl.NodeNetwork.Communication.TransportLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cvl.NodeNetwork.Tools;
using Cvl.NodeNetwork.Server;

namespace Cvl.NodeNetwork.ServiceHost
{
    public class NodeNetworkServiceHost
    {
        internal static List<ServiceReference> services = new List<ServiceReference>();

        public static void RegisterService<TService, TIContract>(string serviceId = null)
        {
            var serviceRef = new ServiceReference();
            serviceRef.ServiceContractFullName = typeof(TIContract).FullName;
            serviceRef.ServiceId = serviceId;
            serviceRef.ServiceType = typeof(TService);
            services.Add(serviceRef);
        }

        public static void RegisterService<TIContract>(TIContract serviceInstance, string serviceId = null)
        {
            var serviceRef = new ServiceReference();
            serviceRef.ServiceContractFullName = typeof(TIContract).FullName;
            serviceRef.ServiceId = serviceId;
            serviceRef.ServiceInstance = serviceInstance;
            services.Add(serviceRef);
        }

        public static string GetXmlOfRegisteredServiceContract()
        {
            var list = new List<ServiceReference>();

            foreach (var serviceReference in services)
            {
                var sr = new ServiceReference();
                sr.ServiceContractFullName = serviceReference.ServiceContractFullName;
                sr.ServiceId = serviceReference.ServiceId;
                list.Add(sr);
            }

            return Serializer.SerializeObject(list);
        }

        public static object GetServiceByContractFullName(string serviceContractFullName, string serviceId)
        {
            var serviceReference = services
                .Single(x => x.ServiceContractFullName == serviceContractFullName &&
                             x.ServiceId == serviceId);

            var serviceInstance = serviceReference.ServiceInstance;

            if (serviceInstance == null)
            {
                serviceInstance = Activator.CreateInstance(serviceReference.ServiceType);
            }

            return serviceInstance;
        }
    }
}
