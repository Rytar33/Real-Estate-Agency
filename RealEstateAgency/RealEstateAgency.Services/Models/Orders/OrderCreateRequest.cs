namespace RealEstateAgency.Services.Models.Orders
{
    public class OrderCreateRequest
    {
        public int IDClient { get; set; }
        public int IDService { get; set; }
        public bool IsRegularCustomer { get; set; }
        public int Sale { get; set; }
        public double Price_Service { get; set; }
        public string? OrderDescription { get; set; }
        public string? Adress { get; set; }
        public DateTime PublishedOrder { get; set; }
        public bool IsOrderAccepted { get; set; }

    }
}
