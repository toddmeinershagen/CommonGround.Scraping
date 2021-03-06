﻿using System;
using System.ComponentModel.DataAnnotations;

namespace CommonGround.Scrape.Models
{
    public class EligibilityScrapeRequest
    {
        public DateTimeOffset? RequestExpiration { get; set; }
        [NonDefaultRequired]
        public Guid ApplicationId { get; set; }
        [Url]
        public string ResponseAddress { get; set; }
        [Required]
        public string PlanCode { get; set; }
        [Required]
        public string PlanDescription { get; set; }
        [Required]
        public string SubscriberId { get; set; }
        [NonDefaultRequired]
        public DateTime PatientDateOfBirth { get; set; }
        [Required]
        public string ProviderId { get; set; }
        [NonDefaultRequired]
        public DateTime ServiceDate { get; set; }
        [Required]
        [ValidateObject]
        public ServiceType[] ServiceTypes { get; set; }
    }
}