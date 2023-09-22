using RealEstateAgency.Services.Models.Pages;

namespace RealEstateAgency.Services.Models.Orders
{
    public class OrderListResponse : BaseResponse
    {
        public List<OrderListItem> Items { get; set; }
        public int CountCompletedOrder { get; set; }
        public int CountNotCompletedOrder { get; set; }
        public int CountWithoutSetScore { get; set; }
        public PageResponse? Page { get; set; }
    }
}
