using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Cvl.NodeNetwork.Communication.TransportLayer;
using Cvl.NodeNetwork.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Cvl.NodeNetwork.WebHub.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NodeNetworkController : ControllerBase
    {
        
        [HttpGet]
        public string Get(string message)
        {
            return "ping " + message;
        }


        [RequestSizeLimit(400000000)]
        public async void Post()
        {
            //Pobieranie danych binarnych
            byte[] requestBinaryData = null;
            using (var ms = new MemoryStream())
            {
                await Request.Body.CopyToAsync(ms);
                requestBinaryData = ms.ToArray();
            }

            //przygotowanie requestu warstwy transportu
            var request = BaseTransportLayer.GetRequest(requestBinaryData);

            //wykonanie serwisu
            var response = ServiceHost.Invoke(request);

            //przygotowanie responsu warstwy transportu
            var responseBinaryData = BaseTransportLayer.GetResponseBinaryData(response);            

            //wysłanie danych binarnych responsu
            await Response.Body.WriteAsync(responseBinaryData, 0, responseBinaryData.Length);
        }
    }
}
