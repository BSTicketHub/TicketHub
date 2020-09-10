using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(TicketHubApp.Startup))]
namespace TicketHubApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
