using System.ComponentModel.DataAnnotations;

namespace RealEstateAgency.Models
{
    public class ReportProblem
    {
        [Key]
        public int IDReport { get; set; }
        public int UserIdUser { get; set; }
        public User User { get; set; }
        public string Problem { get; set; } = null!;
        public bool IsAccept { get; set; } = false;
        public string? CommentAnAccess { get; set; }
    }
}
