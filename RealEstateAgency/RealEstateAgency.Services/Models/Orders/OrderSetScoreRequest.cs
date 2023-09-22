namespace RealEstateAgency.Services.Models.Orders
{
    public class OrderSetScoreRequest
    {
        public int IDOrder { get; set; }
        public int IDClient { get; set; }
        public int Score { get; set; }
        public string? Description { get; set; }
    }
}
