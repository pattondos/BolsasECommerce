using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AlmacenEnvio.Startup))]
namespace AlmacenEnvio
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
