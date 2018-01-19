using Microsoft.Owin;
using Owin;
using System.Web.Http;

[assembly: OwinStartup(typeof(GymLog.API.Startup))]

namespace GymLog.API
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //app.UseCors(CorsOptions.AllowAll);
            var config = new HttpConfiguration();
            ConfigureAuth(app);         
            app.UseWebApi(config);
        }
    }
}
