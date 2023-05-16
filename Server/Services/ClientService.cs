using Server.Context;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services
{
    public class ClientService
    {
        public Client FoundClient(User user)
        {
            using var db = new DataBaseContext();
            var client = db.Client.FirstOrDefault(x => x.User.IDUser == user.IDUser);
            return client != null ? client : null;
        }
        public void CreateClient(User user)
        {
            using var db = new DataBaseContext();
            db.Client.Add(new Client() { User = user, CountPurchasedServices = 0, FirstName = user.FirstName, LastName = user.LastName, SecondName = user.SecondName});
            db.SaveChanges();
        }
    }
}
