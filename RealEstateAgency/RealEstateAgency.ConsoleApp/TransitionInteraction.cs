using RealEstateAgency.Models;
using RealEstateAgency.ConsoleApp.AccessType;

namespace RealEstateAgency.ConsoleApp
{
    public class TransitionInteraction
    {
        public User User { get; set; }
        public TransitionInteraction(User user) => User = user;
        public User? WrapperInteraction()
            => User.TypeAccount switch
            {
                Enums.EnumUserRanked.Client => new ClientInteraction(User).Interaction(),
                Enums.EnumUserRanked.Worker => new WorkerInteraction(User).Interaction(),
                Enums.EnumUserRanked.Admin => new AdminInteraction(User).Interaction(),
                _ => null
            };
    }
}
