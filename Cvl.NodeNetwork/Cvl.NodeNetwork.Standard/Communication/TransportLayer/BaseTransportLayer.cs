using Cvl.NodeNetwork.Communication.TransportLayer.Model;
using Cvl.NodeNetwork.Tools;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Cvl.NodeNetwork.Communication.TransportLayer
{
    public class BaseTransportLayer
    {
        public BaseTransportLayer(string serviceEndpointUrl)
        {
            this.serviceEndpointUrl = serviceEndpointUrl;
        }

        protected string serviceEndpointUrl;

        public virtual Response SendRequest(Request request)
        {
            //Przygotowanie danych do wysyłki po "kablu"
            var binaryRequestData = GetRequestBinaryData(request);

           //Wysyłka danych po "kablu"
            var binaryResponseData = SendRequestTransportLayer(binaryRequestData);

            //odtworzenie danych do obiektu
            var response = GetResponse(binaryResponseData);
            return response;
        }

        

        internal virtual byte[] SendRequestTransportLayer(byte[] binaryRequestData)
        {
            throw new NotImplementedException();
        }

        #region Request, Response data convertion

        public static Request GetRequest(byte[] requestBinaryData)
        {
            var xmlRequestData = Encoding.UTF8.GetString(requestBinaryData);
            var reqest = Serializer.DeserializeObject<Request>(xmlRequestData);
            return reqest;
        }

        public static byte[] GetRequestBinaryData(Request request)
        {
            var xml = Serializer.SerializeObject(request);
            var binaryData = Encoding.UTF8.GetBytes(xml);
            return binaryData;
        }


        public static Response GetResponse(byte[] responseBinaryData)
        {
            var xmlData = Encoding.UTF8.GetString(responseBinaryData);
            var response = Serializer.DeserializeObject<Response>(xmlData);
            return response;
        }

        public static byte[] GetResponseBinaryData(Response response)
        {
            var xml = Serializer.SerializeObject(response);
            var binaryData = Encoding.UTF8.GetBytes(xml);
            return binaryData;
        }

        #endregion
    }
}
