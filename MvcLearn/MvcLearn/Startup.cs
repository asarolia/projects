using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MvcLearn.Startup))]
namespace MvcLearn
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
