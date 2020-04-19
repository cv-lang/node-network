using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Cvl.NodeNetwork.Communication.TransportLayer;
using Cvl.NodeNetwork.Communication.TransportLayer.Model;

namespace Cvl.NodeNetwork.Server
{
    public interface IServiceHostNotification
    {
        void Notification(string serviceContractTypeFullName, string serviceId, Guid requestId);
    }


    public class NodeNetworkControllerBase
    {
        public async static Task<byte[]> Post(byte[] requestBinaryData, IServiceHostNotification notyf)
        {
            //przygotowanie requestu warstwy transportu
            var request = BaseTransportLayer.GetRequest(requestBinaryData);

            if (request is NodeNetworkRequest nodeNetworkRequest)
            {
                nodeNetworkRequest.Notyfi = notyf;
            }

            //wykonanie requestu
            var response = await request.Invoke();

            //przygotowanie responsu warstwy transportu
            var responseBinaryData = BaseTransportLayer.GetResponseBinaryData(response);

            return responseBinaryData;
        }
    }
}
