using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Cvl.NodeNetwork.WebHub.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NodeNetworkController
    {
        [HttpPost]
        [HttpGet]
        public string Get(string message)
        {
            return "ping " + message;
        }
    }
}
