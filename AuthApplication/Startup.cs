using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AuthApplication.Startup))]
namespace AuthApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
