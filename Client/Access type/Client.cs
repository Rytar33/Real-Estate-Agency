using Server.Models;
using Server.Services;
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
        public User? Interaction()
        {
            client = new ClientService().FoundClient(User)!;
            Console.Write(new ModelMenu()
            {
                lines = new List<string>() 
                {
                    "Посмотреть список услуг",
                    "Ваши заказы",
                    "Просмотреть информацию о своём аккаунте",
                    "Выйти из аккаунта",
                },
                IsChoise = true }.GetMenu());
            int choise = 0;
            if (!int.TryParse(Console.ReadLine(), out choise))
                Console.WriteLine("Вы ввели не числовое значение.");
            else
            {
                switch (choise)
                {
                    case 1:
                        new ServiceService().PrintList();
                        Console.Write("Будете ли вы оформлять заказ(Y), или же вернуться назад(B - default)?: ");
                        string charChoise = Console.ReadLine()!;
                        if (charChoise == "Y")
                        {
                            Console.Write("Выберите идентификатор услуги которую вы будете выбирать: ");
                            int choisesService = int.Parse(Console.ReadLine()!);
                            Console.WriteLine("Введите описание к вашему заказу ↓");
                            string descriptionOrder = Console.ReadLine()!;
                            var request = new OrderService().Add(client.IDClient, choisesService, descriptionOrder);
                            Console.WriteLine(request.Message);
                        }
                        break;
                    case 2:
                        var serviceOrder = new OrderService();
                        var listOrder = serviceOrder.GetList(IDClient: client.IDClient);
                        if (listOrder.Count() == 0)
                            Console.WriteLine("Вы не делали пока что заказов.");
                        else
                        {
                            int countNotCompleted = 0, countCompleted = 0, countWithoutScore = 0;
                            foreach (var order in listOrder)
                            {
                                if (order.IsOrderAccepted) countCompleted++;
                                else countNotCompleted++;
                                if (order.ScoreForWork == null) countWithoutScore++;
                                Console.WriteLine(serviceOrder.GetOrder(order));
                            }
                            Console.Write($"Количество ваших заказов {listOrder.Count()}\n" + "Из них: ");
                            if (countNotCompleted > 0) Console.Write($"{countNotCompleted} не выполненных");
                            if (countCompleted > 0) Console.Write($", {countCompleted} выполненных");
                            if (countWithoutScore > 0) Console.Write($", {countWithoutScore} без оставленного отзыва.");
                        }
                        break;
                    case 3:
                        Console.WriteLine(new ClientService().GetInformationClient(User.IDUser));
                        break;
                    case 4:
                        new UserService().Exit(User.IDUser);
                        User = null!;
                        break;
                }
            }
            return User;
        }
    }
}
