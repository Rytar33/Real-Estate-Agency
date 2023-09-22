using RealEstateAgency.Services.Models.Pages;

namespace RealEstateAgency.Services.Models.Clients
{
    public class ClientListRequest
    {
        public string? Search { get; set; }
        public PageRequest? Page { get; set; }
    }
}
