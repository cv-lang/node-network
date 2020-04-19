using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Cvl.NodeNetwork.Communication.TransportLayer;
using Cvl.NodeNetwork.Server;
using Cvl.NodeNetwork.WebHub.SignalR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Cvl.NodeNetwork.WebHub.Controllers
{
    /// <summary>
    /// Kontroler odpowiedzialny za hostowanie serwisów w hubie
    /// Które hostowane są lokalnie
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class NodeNetworkController : ControllerBase, IServiceHostNotification
    {

        public IHubContext<NodeNetworkHub> _strongChatHubContext { get; }

        public NodeNetworkController(IHubContext<NodeNetworkHub> chatHubContext)
        {
            _strongChatHubContext = chatHubContext;
        }

        [HttpGet]
        public string Get(string message)
        {
            return "ping " + message;
        }


        [RequestSizeLimit(400_000_000)]
        public async Task Post()
        {
            //Pobieranie danych binarnych
            byte[] requestBinaryData = null;
            using (var ms = new MemoryStream())
            {
                await Request.Body.CopyToAsync(ms);
                requestBinaryData = ms.ToArray();
            }

            var responseBinaryData= await NodeNetworkControllerBase.Post(requestBinaryData, this);
            
            //wysłanie danych binarnych responsu
            await Response.Body.WriteAsync(responseBinaryData, 0, responseBinaryData.Length);
        }

        [Route("Notification")]
        [HttpPost]
        public void Notification(
            string serviceContractTypeFullName, 
            string serviceId, 
            Guid requestId)
        {
            NodeNetworkHub.SendNotificationToServiceHost(serviceContractTypeFullName, serviceId,
                requestId, _strongChatHubContext);
        }
    }
}
