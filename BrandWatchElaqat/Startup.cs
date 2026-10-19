using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BrandWatchElaqat.Startup))]
namespace BrandWatchElaqat
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
