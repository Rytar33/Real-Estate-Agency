namespace RealEstateAgency.Services.Models.Clients
{
    public class ClientListItem
    {
        public int IDClient { get; set; }
        public int IDUser { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? SecondName { get; set; }
        public string FullName
        {
            get
            {
                return !string.IsNullOrWhiteSpace(SecondName) ?
                    $"{LastName} {FirstName} {SecondName}" :
                    $"{LastName} {FirstName}";
            }
        }
        public DateTime DateRegistration { get; set; }
        public int CountPurchasedServices { get; set; }
        public bool IsRegularCustomer { get { return CountPurchasedServices > 6 ? true : false; } }
    }
}
