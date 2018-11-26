using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Bekhar.Startup))]
namespace Bekhar
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
