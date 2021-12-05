using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FishingBooker.Startup))]
namespace FishingBooker
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
