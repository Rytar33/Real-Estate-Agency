using System.ComponentModel.DataAnnotations;

namespace RealEstateAgency.Models
{
    public class Service
    {
        [Key]
        public int IDService { get; set; }
        [StringLength(60, MinimumLength = 4)]
        public string NameService { get; set; }
        [StringLength(500)]
        public string? DescriptionService { get; set; }
        public double PriceService { get; set; }
        public string TypeService { get; set; }
    }
}
