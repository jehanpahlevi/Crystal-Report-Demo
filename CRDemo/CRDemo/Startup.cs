using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CRDemo.Startup))]
namespace CRDemo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
