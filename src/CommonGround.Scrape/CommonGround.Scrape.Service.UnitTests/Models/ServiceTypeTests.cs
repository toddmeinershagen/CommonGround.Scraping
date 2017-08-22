using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using CommonGround.Scrape.Models;
using FluentAssertions;
using NUnit.Framework;

namespace CommonGround.Scrape.Service.UnitTests.Models
{
    [TestFixture]
    public class ServiceTypeTests
    {
        [Test]
        public void given_valid_service_type_when_validating_should_return_no_errors()
        {
            var serviceType = new ServiceType {Code = "CODE", Description = "DESCRIPTION"};

            ICollection<ValidationResult> results;
            var validator = new DataAnnotationsValidator();
            validator.TryValidate(serviceType, out results).Should().BeTrue();
            results.Should().BeEmpty();
        }

        [Test]
        public void given_invalid_service_type_when_validating_should_return_errors()
        {
            var serviceType = new ServiceType { Code = string.Empty, Description = null };

            ICollection<ValidationResult> results;
            var validator = new DataAnnotationsValidator();
            validator.TryValidate(serviceType, out results).Should().BeFalse();

            results.Count.Should().Be(2);
            var messages = results.Select(r => r.ErrorMessage).ToList();

            messages.Should().Contain("The Code field is required.");
            messages.Should().Contain("The Description field is required.");
        }
    }
}
