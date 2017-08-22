using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using CommonGround.Scrape.Models;
using FluentAssertions;
using Microservices.Http.Models;
using NUnit.Framework;

namespace CommonGround.Scrape.Service.AcceptanceTests.api.ScrapeRequests
{
    [TestFixture]
    public class given_a_valid_non_existing_request_when_posting
    {
        private HttpResponseMessage _response;
        private EligibilityScrapeRequest _request;

        [OneTimeSetUp]
        public async Task Before()
        {
            _request = new EligibilityScrapeRequest
            {
                RequestExpiration = null,
                ApplicationId = Guid.NewGuid(),
                ResponseAddress = "http://mylocation.nthrive.com",
                PlanCode = "BCHP100",
                PlanDescription = "BlueChoice HealthPlan",
                SubscriberId = "ZCL87410574",
                PatientDateOfBirth = new DateTime(1979, 12, 8),
                ProviderId = "1154373843",
                ServiceDate = DateTime.Now,
                ServiceTypes = new[] { new ServiceType { Code = "30", Description = "General" } }
            };
            var content = new ObjectContent(typeof(EligibilityScrapeRequest), _request, new JsonMediaTypeFormatter());
            _response = await Server.Instance.HttpClient.PostAsync("api/ScrapeRequests/Eligibility", content);
        }

        [Test]
        public void should_return_ok_status()
        {
            _response.StatusCode
                .Should().Be(HttpStatusCode.Created);
        }

        [Test]
        public void should_return_the_location_with_an_id()
        {
            var location = _response.Headers
                .Location.OriginalString;

            location.Should().StartWith($"{Server.Instance.HttpClient.BaseAddress}api/ScrapeRequests/Eligibility/");

            const int guidLength = 36;
            var id = location.Substring(location.Length - guidLength, guidLength);

            id.IsGuid().Should().BeTrue();
        }
    }

    [TestFixture]
    public class given_a_valid_existing_request_when_posting
    {
        private HttpResponseMessage _response1;
        private HttpResponseMessage _response2;
        private EligibilityScrapeRequest _request;

        [OneTimeSetUp]
        public async Task Before()
        {
            _request = new EligibilityScrapeRequest
            {
                RequestExpiration = null,
                ApplicationId = Guid.NewGuid(),
                ResponseAddress = "http://mylocation.nthrive.com",
                PlanCode = "BCHP100",
                PlanDescription = "BlueChoice HealthPlan",
                SubscriberId = "ZCL87410574",
                PatientDateOfBirth = new DateTime(1979, 12, 8),
                ProviderId = "1154373843",
                ServiceDate = DateTime.Now,
                ServiceTypes = new[] { new ServiceType { Code = "30", Description = "General" } }
            };

            var content1 = new ObjectContent(typeof(EligibilityScrapeRequest), _request, new JsonMediaTypeFormatter());
            _response1 = await Server.Instance.HttpClient.PostAsync("api/ScrapeRequests/Eligibility", content1);

            var content2 = new ObjectContent(typeof(EligibilityScrapeRequest), _request, new JsonMediaTypeFormatter());
            _response2 = await Server.Instance.HttpClient.PostAsync("api/ScrapeRequests/Eligibility", content2);
        }

        [Test]
        public void should_return_created_status_for_second_request()
        {
            _response2.StatusCode
                .Should().Be(HttpStatusCode.Created);
        }

        [Test]
        public void should_return_the_location_with_id_that_does_not_match_the_first_request_in_the_second_request()
        {
            var location1 = _response1.Headers
                .Location.OriginalString;

            var location2 = _response2.Headers
                .Location.OriginalString;

            location2.Should().StartWith($"{Server.Instance.HttpClient.BaseAddress}api/ScrapeRequests/Eligibility/");

            const int guidLength = 36;
            var id1 = location1.Substring(location1.Length - guidLength, guidLength);
            var id2 = location2.Substring(location2.Length - guidLength, guidLength);

            new Guid(id2).Should().NotBe(new Guid(id1));
        }
    }

    [TestFixture]
    public class given_an_invalid_non_existing_request_when_posting
    {
        private HttpResponseMessage _response;
        private EligibilityScrapeRequest _request;

        [OneTimeSetUp]
        public async Task Before()
        {
            _request = new EligibilityScrapeRequest
            {
                RequestExpiration = null,
                ApplicationId = Guid.Empty,
                ResponseAddress = "todd@meinershagen.net",
                PlanCode = null,
                PlanDescription = null,
                SubscriberId = null,
                PatientDateOfBirth = DateTime.MinValue,
                ProviderId = null,
                ServiceDate = DateTime.MinValue,
                ServiceTypes = null
            };
            var content = new ObjectContent(typeof(EligibilityScrapeRequest), _request, new JsonMediaTypeFormatter());
            _response = await Server.Instance.HttpClient.PostAsync("api/ScrapeRequests/Eligibility", content);
        }

        [Test]
        public void should_return_badrequest_status()
        {
            _response.StatusCode
                .Should().Be(HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task should_return_error_messages_for_invalid_request()
        {
            var error = await _response.Content.ReadAsAsync<Error>();
            error.Message.Should().Be("The request is invalid.");
            error.ModelState.Count.Should().BeGreaterThan(1);
        }
    }
}
