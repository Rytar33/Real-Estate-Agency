namespace RealEstateAgency.Services.Models.Recoverys
{
    public class RecoveryEndRequest
    {
        public string Email { get; set; }
        public string NewPasswordHash { get; set; }
        public string RepeatePasswordHash { get; set; }
    }
}
