using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(UtahOpenSource.Backend.Startup))]

namespace UtahOpenSource.Backend
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}
