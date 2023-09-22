using RealEstateAgency.Services.Models.Pages;

namespace RealEstateAgency.Services.Models.Orders
{
    public class OrderListRequest
    {
        public string? Search {  get; set; }
        public bool? IsOrderAccepted { get; set; }
        public int? IDWorker { get; set; }
        public int? IDClient {  get; set; }
        public PageRequest? Page { get; set; }
    }
}
