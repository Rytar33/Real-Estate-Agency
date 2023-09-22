using RealEstateAgency.Services.Models.Pages;

namespace RealEstateAgency.Services.Models.ServicesModels
{
    public class ServiceListRequest
    {
        public string? Search { get; set; }
        public double? PricesFrom { get; set; }
        public double? PricesTo { get; set; }
        public PageRequest? Page { get; set; }
    }
}
