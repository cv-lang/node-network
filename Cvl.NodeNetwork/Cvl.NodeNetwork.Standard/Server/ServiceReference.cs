using System;
using System.Collections.Generic;
using System.Text;

namespace Cvl.NodeNetwork.Server
{
    public class ServiceReference
    {
        public string ServiceContractFullName { get; set; }

        public string ServiceId { get; set; }

        public Type ServiceType { get; set; }
        public object ServiceInstance { get; set; }

        public string NodeNetworkServieHostAddress { get; set; }
    }
}
