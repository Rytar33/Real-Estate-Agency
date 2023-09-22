using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstateAgency.Models
{
    public class Client
    {
        [Key]
        public int IDClient { get; set; }
        public int UserIDUser { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public User User { get; set; }
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
        public int CountPurchasedServices { get; set; }
        public bool IsRegularCustomer { get { return CountPurchasedServices > 6 ? true : false; } }
    }
}
