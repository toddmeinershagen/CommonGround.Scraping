using System.ComponentModel.DataAnnotations;

namespace CommonGround.Scrape.Service.Models
{
    public class ServiceType
    {
        [Required]
        public string Code { get; set; }
        [Required]
        public string Description { get; set; }
    }
}