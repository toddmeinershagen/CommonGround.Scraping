using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CommonGround.Scraping.Service.Models;
using FluentAssertions;
using NUnit.Framework;

namespace CommonGround.Scraping.Service.AcceptanceTests.api.ScrapeRequests
{
    [TestFixture]
    public class given_a_valid_non_existing_request_when_posting
    {
        private HttpResponseMessage _response;
        private EligibilityScrapingRequest _request;

        [OneTimeSetUp]
        public async Task Before()
        {
            _request = new EligibilityScrapingRequest
            {
                RequestId = Guid.NewGuid(),
                RequestExpiration = null,
                ApplicationId = Guid.NewGuid(),
                ResponseAddress = "http://mylocation.nthrive.com",
                PlanCode = "BCHP100",
                PlanDescription = "BlueChoice HealthPlan",
                SubscriberId = "ZCL87410574",
                PatientDateOfBirth = new DateTime(1979, 12, 8),
                ProviderId = "1154373843",
                ServiceTypes = new[] { new ServiceType { Code = "30", Description = "General" } }
            };
            var content = new ObjectContent(typeof(EligibilityScrapingRequest), _request, new JsonMediaTypeFormatter());
            _response = await Server.Instance.HttpClient.PostAsync("api/ScrapeRequests/Eligibility", content);
        }

        [Test]
        public void should_return_ok_status()
        {
            _response.StatusCode
                .Should().Be(HttpStatusCode.Created);
        }

        [Test]
        public void should_return_the_location()
        {
            var location = _response.Headers
                .Location.OriginalString;

            location.Should().StartWith($"{Server.Instance.HttpClient.BaseAddress}api/ScrapeRequests/Eligibility/");

            const int guidLength = 36;
            var id = location.Substring(location.Length - guidLength, guidLength);

            id.IsGuid().Should().BeTrue();
        }

        [Test]
        public void should_return_the_request()
        {
            
        }
    }

    public static class StringExtensions
    {
        private static readonly Regex GuidFormat = new Regex(
            "^[A-Fa-f0-9]{32}$|" +
            "^({|\\()?[A-Fa-f0-9]{8}-([A-Fa-f0-9]{4}-){3}[A-Fa-f0-9]{12}(}|\\))?$|" +
            "^({)?[0xA-Fa-f0-9]{3,10}(, {0,1}[0xA-Fa-f0-9]{3,6}){2}, {0,1}({)([0xA-Fa-f0-9]{3,4}, {0,1}){7}[0xA-Fa-f0-9]{3,4}(}})$");

        /// <summary>
        /// Converts the string representation of a Guid to its Guid 
        /// equivalent. A return value indicates whether the operation 
        /// succeeded. 
        /// </summary>
        /// <param name="value">A string containing a Guid to convert.</param>
        /// <param name="result">
        /// When this method returns, contains the Guid value equivalent to 
        /// the Guid contained in <paramref name="value"/>, if the conversion 
        /// succeeded, or <see cref="Guid.Empty"/> if the conversion failed. 
        /// The conversion fails if the <paramref name="value"/> parameter is a 
        /// <see langword="null" /> reference (<see langword="Nothing" /> in 
        /// Visual Basic), or is not of the correct format. 
        /// </param>
        /// <value>
        /// <see langword="true" /> if <paramref name="value"/> was converted 
        /// successfully; otherwise, <see langword="false" />.
        /// </value>
        /// <exception cref="ArgumentNullException">
        ///        Thrown if <pararef name="value"/> is <see langword="null"/>.
        /// </exception>
        /// <remarks>
        /// Original code at https://connect.microsoft.com/VisualStudio/feedback/ViewFeedback.aspx?FeedbackID=94072&wa=wsignin1.0#tabs
        /// 
        /// </remarks>
        public static bool IsGuid(this string value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            var match = GuidFormat.Match(value);

            return match.Success;
        }
    }
}
