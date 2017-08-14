using System.Web.Http;
using Owin;

namespace CommonGround.Scraping.Service.AcceptanceTests
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            WebApiConfig.Register(config);

            app.UseWebApi(config);
        }
    }
}
