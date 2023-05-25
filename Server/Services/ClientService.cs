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
        public Client? FoundClient(User user)
        {
            using var db = new DataBaseContext();
            var client = db.Client.FirstOrDefault(x => x.UserIDUser == user.IDUser);
            return client != null ? client : null;
        }
        public void CreateClient(User user)
        {
            using var db = new DataBaseContext();
            db.Client.Add(new Client() { UserIDUser = user.IDUser, CountPurchasedServices = 0, FirstName = user.FirstName, LastName = user.LastName, SecondName = user.SecondName});
            db.SaveChanges();
        }
        public string GetInformationClient(int IDUser)
        {
            using var db = new DataBaseContext();
            var userFind = db.User.FirstOrDefault(u => u.IDUser == IDUser);
            if (userFind == null) return $"Пользователь №{IDUser} не был найден.";
            var clientFind = db.Client.FirstOrDefault(w => w.UserIDUser == IDUser);
            string userString =
                   "==========================================\n"
                + $"Пользователь №{userFind.IDUser}\n"
                + $"Клиент №{clientFind!.IDClient}\n"
                + $"Имя пользователя: {userFind.UserName}\n"
                + $"Полное ФИО: {userFind.FullName}\n"
                + $"Количество оформленных заказов: {clientFind!.CountPurchasedServices}\n";
            if (clientFind!.IsRegularCustomer) userString += "Частый покупатель\n";
            userString += $"Статус: {userFind.Status}\n"
                + "==========================================";
            return userString;
        }
    }
}
