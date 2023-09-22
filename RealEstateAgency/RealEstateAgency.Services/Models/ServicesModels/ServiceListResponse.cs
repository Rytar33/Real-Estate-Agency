using RealEstateAgency.Services.Models.Pages;

namespace RealEstateAgency.Services.Models.ServicesModels
{
    public class ServiceListResponse : BaseResponse
    {
        public List<ServiceListItem> Items { get; set; }
        public PageResponse? Page { get; set; }
    }
}
