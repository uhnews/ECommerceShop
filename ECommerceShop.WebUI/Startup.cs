using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ECommerceShop.WebUI.Startup))]
namespace ECommerceShop.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
