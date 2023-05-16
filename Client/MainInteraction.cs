using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class MainInteraction
    {
        public User User { get; set; }
        public MainInteraction(User user) => User = user;
        public User WrapperInteraction()
            => User.TypeAccount switch
            {
                "Client" => new Client(User).Interaction(),
                "Worker" => new Worker(User).Interaction(),
                _ => null
            };
    }
}
