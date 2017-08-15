using System;
using System.ComponentModel.DataAnnotations;

namespace CommonGround.Scraping.Service.Models
{
    public class EligibilityScrapingRequest
    {
        [Required]
        public Guid CorrelationId { get; set; }
        public DateTimeOffset? RequestExpiration { get; set; }
        [Required]
        public Guid ApplicationId { get; set; }
        [Url]
        public string ResponseAddress { get; set; }
        [Required]
        public string PlanCode { get; set; }
        [Required]
        public string PlanDescription { get; set; }
        [Required]
        public string SubscriberId { get; set; }
        [Required]
        public DateTime PatientDateOfBirth { get; set; }
        [Required]
        public string ProviderId { get; set; }
        [Required]
        public ServiceType[] ServiceTypes { get; set; }
    }
}