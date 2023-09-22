using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstateAgency.Models
{
    public class Admin
    {
        public int AdminId { get; set; }
        public int UserIdUser { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public User User { get; set; }
    }
}
