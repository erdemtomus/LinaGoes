using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LinaGoes.Startup))]
namespace LinaGoes
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
