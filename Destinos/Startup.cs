using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Destinos.Startup))]
namespace Destinos
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}