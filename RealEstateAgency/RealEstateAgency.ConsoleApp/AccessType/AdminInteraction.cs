using RealEstateAgency.Models;

namespace RealEstateAgency.ConsoleApp.AccessType
{
    public class AdminInteraction
    {
        public Admin Admin { get; set; }
        public User User { get; set; }
        public AdminInteraction(User user) 
            => User = user;
        public User? Interaction()
        {
            Console.WriteLine("Пока в разработке...");
            return null;
        }
    }
}
