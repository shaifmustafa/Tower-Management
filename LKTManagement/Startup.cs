using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LKTManagement.Startup))]
namespace LKTManagement
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
