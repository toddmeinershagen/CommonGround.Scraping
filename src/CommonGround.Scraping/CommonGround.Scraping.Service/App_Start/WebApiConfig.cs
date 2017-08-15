using System.Web.Http;

namespace CommonGround.Scraping.Service
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            config.AddErrorHandling();
        }
    }
}
