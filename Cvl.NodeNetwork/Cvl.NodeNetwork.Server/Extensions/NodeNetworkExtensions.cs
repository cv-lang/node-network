using Cvl.NodeNetwork.ServiceHost;
using Cvl.NodeNetwork.Test;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Internal;

namespace Cvl.NodeNetwork.Server.Extensions
{
    public static class NodeNetworkExtensions
    {
        public static IApplicationBuilder UseNodeNetwork(this IApplicationBuilder app)
        {
            //Microsoft.AspNetCore.Builder.EndpointRoutingApplicationBuilderExtensions
            //Microsoft.AspNetCore.Builder.

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapHub<NodeNetworkHub>("/NodeNetworkHub");
            //});
            //app.UseMiddleware().UseSignalR(); //.UseEndpoint
            NodeNetworkServiceHost.RegisterService<TestService,ITestService>();
            NodeNetworkServiceHost.RegisterService<Server.Hub.NodeNetworkHubService,Server.Hub.INodeNetworkHubService>();



            return app;
        }
    }
}
