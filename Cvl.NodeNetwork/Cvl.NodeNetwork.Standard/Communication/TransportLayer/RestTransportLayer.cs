using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Cvl.NodeNetwork.Communication.TransportLayer
{
    public class RestTransportLayer : BaseTransportLayer
    {
        public RestTransportLayer(string serviceEndpointUrl)
        :base(serviceEndpointUrl)
        {
        }

        internal override byte[] SendRequestTransportLayer(byte[] binaryRequestData)
        {
            var httpClient = new HttpClient();

            var serviceUrl = serviceEndpointUrl + "/NodeNetwork";

            HttpResponseMessage responseHttp = httpClient
                .PostAsync(new Uri(serviceUrl),
                    new ByteArrayContent(binaryRequestData)).Result;
            Byte[] binaryResponseData = responseHttp.Content.ReadAsByteArrayAsync().Result;

            return binaryResponseData;
        }
    }
}
