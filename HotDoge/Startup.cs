using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HotDoge.Startup))]
namespace HotDoge
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
