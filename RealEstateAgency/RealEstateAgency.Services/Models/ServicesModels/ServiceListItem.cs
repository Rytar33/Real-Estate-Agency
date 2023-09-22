namespace RealEstateAgency.Services.Models.ServicesModels
{
    public class ServiceListItem
    {
        public int IDService { get; set; }
        public string NameService { get; set; } = null!;
        public string? DescriptionService { get; set; }
        public double PriceService { get; set; }
        public string TypeService { get; set; } = null!;
    }
}
