using System.ComponentModel.DataAnnotations;

namespace CommonGround.Scrape.Models
{
    public class ServiceType
    {
        [Required]
        public string Code { get; set; }
        [Required]
        public string Description { get; set; }
    }
}