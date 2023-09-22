using RealEstateAgency.Enums;

namespace RealEstateAgency.Services.Models.Users
{
    public class UserDetailResponse
    {
        public int IDUser { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public EnumUserRanked TypeAccount { get; set; }
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
        public EnumUserStatus Status { get; set; }
        public DateTime DateRegistration { get; set; }
    }
}
