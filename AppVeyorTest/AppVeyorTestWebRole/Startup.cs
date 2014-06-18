using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AppVeyorTestWebRole.Startup))]
namespace AppVeyorTestWebRole
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
