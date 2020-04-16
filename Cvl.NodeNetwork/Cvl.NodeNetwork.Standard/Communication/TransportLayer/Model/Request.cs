using System;
using System.Collections.Generic;
using System.Text;

namespace Cvl.NodeNetwork.Communication.TransportLayer.Model
{
    public class Request
    {
        public string MethodName { get; set; }
        public string ContractTypeFullName { get; set; }
        public List<object> Arguments { get; set; }
        
    }
}
