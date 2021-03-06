﻿using System;
using System.Web.Http;
using CommonGround.Scrape.Models;

namespace CommonGround.Scrape.Service.Controllers
{
    [RoutePrefix("api/ScrapeRequests/Eligibility")]
    public class EligibilityController : ApiController
    {
        /// <summary>
        /// Endpoint for posting new eligibility scraping requests.
        /// </summary>
        /// <remarks>
        /// This is not an idempotent function.  If you post the same request
        /// </remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route]
        public IHttpActionResult Post(EligibilityScrapeRequest request)
        {
            var requestId = Guid.NewGuid();

            var runner = new EligibilityRunner();
            runner.Run(request);

            return Created($"{Request.RequestUri}/{requestId}", request);
        }
    }
}
