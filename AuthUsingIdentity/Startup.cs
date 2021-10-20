using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AuthUsingIdentity.Startup))]
namespace AuthUsingIdentity
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
