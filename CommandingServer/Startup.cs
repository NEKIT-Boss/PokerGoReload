using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(CommandingServer.Startup))]

namespace CommandingServer
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}
