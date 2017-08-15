using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using CommonGround.Scraping.Service.Models;
using FluentAssertions;
using NUnit.Framework;

namespace CommonGround.Scraping.Service.UnitTests.Models
{
    [TestFixture]
    public class EligibilityScrapingRequestTests
    {
        [Test]
        public void given_valid_request_when_validating_should_return_no_errors()
        {
            var request = new EligibilityScrapingRequest
            {
                CorrelationId = Guid.NewGuid(),
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

            ICollection<ValidationResult> results;
            var validator = new DataAnnotationsValidator();
            validator.TryValidate(request, out results).Should().BeTrue();
            results.Count.Should().Be(0);
        }

        [Test]
        public void given_invalid_request_when_validating_should_return_errors()
        {
            var request = new EligibilityScrapingRequest
            {
                CorrelationId = Guid.Empty,
                RequestExpiration = null,
                ApplicationId = Guid.Empty,
                ResponseAddress = "todd@meinershagen.net",
                PlanCode = null,
                PlanDescription = null,
                SubscriberId = null,
                PatientDateOfBirth = DateTime.MinValue,
                ProviderId = null,
                ServiceTypes = null
            };

            ICollection<ValidationResult> results;
            var validator = new DataAnnotationsValidator();
            validator.TryValidate(request, out results).Should().BeFalse();

            
            results.Count.Should().BeGreaterThan(1);
            var messages = results.Select(r => r.ErrorMessage);

            messages.Contains("The CorrelationId field requires a non-default value.");
            messages.Contains("The ApplicationId field requires a non-default value.");
            messages.Contains("The ResponseAddress field is not a valid fully-qualified http, https, or ftp URL.");
            messages.Contains("The PlanCode field is required.");
            messages.Contains("The PlanDescription field is required.");
            messages.Contains("The SubscriberId field is required.");
            messages.Contains("The PatientDateOfBirth field requires a non-default value.");
            messages.Contains("The ProviderId field is required.");
            messages.Contains("The ServiceTypes field is required.");
        }
    }
}
