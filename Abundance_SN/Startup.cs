using Microsoft.Owin;
using Owin;


[assembly: OwinStartupAttribute(typeof(Abundance_SN.Startup))]
namespace Abundance_SN
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
