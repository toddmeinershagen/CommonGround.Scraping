using System.Web.Http;
using CommonGround.Scraping.Service.Models;

namespace CommonGround.Scraping.Service.Controllers
{
    [RoutePrefix("api/ScrapeRequests/Eligibility")]
    public class EligibilityController : ApiController
    {
        [Route]
        public IHttpActionResult Post(EligibilityScrapingRequest request)
        {
            return Created($"{Request.RequestUri}/{request.RequestId}", request);
        }
    }
}
