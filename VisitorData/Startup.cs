using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VisitorData.Startup))]
namespace VisitorData
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
