using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cvl.NodeNetwork.Client;
using Cvl.NodeNetwork.Test;

namespace TestApp48
{
    class Program
    {
        static void Main(string[] args)
        {
            var endpoint = "nodenetwork://" + "https://localhost:44398/";
            using (var mychannelFactory = new ChannelFactory<ITestService>(endpoint))
            {
                var serviceProxy = mychannelFactory.CreateChannel();
                var ret = serviceProxy.Ping(2);
            }
        }
    }
}
