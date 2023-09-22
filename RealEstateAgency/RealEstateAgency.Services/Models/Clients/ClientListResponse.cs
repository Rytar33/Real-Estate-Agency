using RealEstateAgency.Services.Models.Pages;

namespace RealEstateAgency.Services.Models.Clients
{
    public class ClientListResponse
    {
        public List<ClientListItem>? Items { get; set; }
        public PageResponse? Page { get; set; }
    }
}
