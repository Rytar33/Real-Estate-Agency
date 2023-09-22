using RealEstateAgency.Models;
using RealEstateAgency.Services.Models.Orders;
using RealEstateAgency.Services.Models.Pages;
using RealEstateAgency.Services.Models.ServicesModels;
using RealEstateAgency.Services.Services;

namespace RealEstateAgency.ConsoleApp.AccessType
{
    public class ClientInteraction
    {
        public Client client { get; set; }
        public User User { get; set; }
        public ClientInteraction(User user) 
            => User = user;
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
                Console.Clear();
                switch (choise)
                {
                    case 1:
                        int sizePageServices = 5;
                        string searchServices = "";
                        double? priceFrom = null;
                        double? priceTo = null;

                        new PagesInteraction(sizePageServices)
                        {
                            user = User,
                            client = client
                        }
                        .ServicesChekcList(searchServices, priceFrom, priceTo);
                        break;
                    case 2:
                        int sizePageOrders = 5;
                        string searchOrders = "";
                        bool? isOrderAccepted = null;
                        int? IDClient = client.IDClient;
                        int? IDWorker = null;

                        new PagesInteraction(sizePageOrders)
                        {
                            user = User,
                            client = client
                        }
                        .OrdersChekcList(searchOrders, IDClient, IDWorker, isOrderAccepted);
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
