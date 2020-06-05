using Cvl.NodeNetwork.Server.SignalR;
using Cvl.NodeNetwork.ServiceHost;
using Cvl.NodeNetwork.Test;
using Microsoft.AspNetCore.Builder;


namespace Cvl.NodeNetwork.Server.Extensions
{
    public static class NodeNetworkExtensions
    {
        public static IApplicationBuilder UseNodeNetwork(this IApplicationBuilder app)
        {
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<NodeNetworkHub>("/NodeNetworkHub");
            });
            
            NodeNetworkServiceHost.RegisterService<TestService,ITestService>();
            NodeNetworkServiceHost.RegisterService<Server.Hub.NodeNetworkHubService,Server.Hub.INodeNetworkHubService>();

            return app;
        }
    }
}
