using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Cvl.NodeNetwork.Communication.TransportLayer
{
    public class RestTransportLayer : BaseTransportLayer
    {
        public RestTransportLayer(string serviceEndpointUrl)
        {
            this.serviceEndpointUrl = serviceEndpointUrl;
        }

        private string serviceEndpointUrl;

        protected override byte[] SendRequestTransportLayer(byte[] binaryRequestData)
        {
            var httpClient = new HttpClient();

            HttpResponseMessage responseHttp = httpClient
                .PostAsync(new Uri(serviceEndpointUrl),
                    new ByteArrayContent(binaryRequestData)).Result;
            Byte[] binaryResponseData = responseHttp.Content.ReadAsByteArrayAsync().Result;

            return binaryResponseData;
        }
    }
}
