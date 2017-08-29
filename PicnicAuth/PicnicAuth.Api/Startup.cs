using Microsoft.Owin;
using Owin;
using PicnicAuth.Api;

[assembly: OwinStartup(typeof(Startup))]

namespace PicnicAuth.Api
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
