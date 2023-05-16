using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class Client
    {
        public Server.Models.Client client { get; set; }
        public User User { get; set; }
        public Client(User user) => User = user;
        public User Interaction()
        {

            Console.Write(new ModelMenu()
            {
                lines = new List<string>() 
                {
                    "Посмотреть список услуг",
                    "Посмотреть список недвижимости которое предлагает агентство недвижимости",
                    "Просмотреть информацию о своём аккаунте",
                    "Выйти из аккаунта",
                },
                IsChoise = true }.GetMenu());
            int choise = 0;
            try {
                choise = int.Parse(Console.ReadLine());
            } catch (Exception ex)
            {
                Console.WriteLine("Ошибка: " + ex.Message);
                return User;
            }
            switch (choise)
            {
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    User = null;
                    break;
            }
            return User;
        }
    }
}
