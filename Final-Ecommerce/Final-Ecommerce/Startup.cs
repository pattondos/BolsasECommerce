using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Final_Ecommerce.Startup))]
namespace Final_Ecommerce
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
