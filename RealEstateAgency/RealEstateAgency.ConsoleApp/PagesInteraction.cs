using RealEstateAgency.Enums;
using RealEstateAgency.Models;
using RealEstateAgency.Services.Models.Orders;
using RealEstateAgency.Services.Models.Pages;
using RealEstateAgency.Services.Models.ServicesModels;
using RealEstateAgency.Services.Services;

namespace RealEstateAgency.ConsoleApp
{
    public class PagesInteraction
    {
        public Client? client { get; set; }
        public Worker? worker { get; set; }
        public User? user { get; set; }
        public int SizePage { get; set; }
        public PagesInteraction(int sizePage) => SizePage = sizePage;
        public void ServicesChekcList(string? search, double? priceFrom, double? priceTo)
        {
            bool isWork = true;
            int pageLocation = 1;
            do
            {
                Console.WriteLine("Список загружается, пожалуйста подождите...");
                var serviceService = new ServiceService();
                var serviceListPage = serviceService.GetList(
                    new ServiceListRequest()
                    {
                        Page = new PageRequest()
                        {
                            Page = pageLocation,
                            PageSize = SizePage
                        },
                        Search = search,
                        PricesFrom = priceFrom,
                        PricesTo = priceTo
                    });
                if (!serviceListPage.IsSuccess)
                {
                    Console.WriteLine(serviceListPage.Message);
                    break;
                }

                double allPage = Math.Ceiling((double)serviceListPage.Page!.Count / serviceListPage.Page.PageSize);
                Console.Clear();
                serviceListPage.Items!.ForEach(s => Console.WriteLine(
                    serviceService.GetService(new Service()
                    {
                        IDService = s.IDService,
                        NameService = s.NameService,
                        DescriptionService = s.DescriptionService,
                        PriceService = s.PriceService,
                        TypeService = s.TypeService
                    })));

                Console.WriteLine($"Страница {pageLocation} из {allPage}. Кол-во записей {serviceListPage.Page.Count}");
                Console.Write("Действие(<, >, Y, !): ");
                char choiseDo = Convert.ToChar(Console.ReadLine()!);
                switch (choiseDo)
                {
                    case '>':
                        if (pageLocation < allPage) pageLocation++;
                        break;
                    case '<':
                        if (pageLocation > 1) pageLocation--;
                        break;
                    case 'Y':
                        Console.Write("Выберите идентификатор услуги которую вы будете выбирать: ");
                        int choisesService = int.Parse(Console.ReadLine()!);
                        Console.WriteLine("Введите описание к вашему заказу ↓");
                        string descriptionOrder = Console.ReadLine()!;
                        Console.WriteLine("Введите ваш адресс/адресс продаваемой/сдаваемой квартиры ↓");
                        string clientAdress = Console.ReadLine()!;
                        var request = new OrderService().Add(
                            new OrderCreateRequest()
                            {
                                IDClient = client!.IDClient,
                                IDService = choisesService,
                                OrderDescription = descriptionOrder,
                                Adress = clientAdress,
                                IsOrderAccepted = false,
                                PublishedOrder = DateTime.Now,
                                IsRegularCustomer = client.IsRegularCustomer
                            });
                        Console.WriteLine(request.Message);
                        if (request.IsSuccess) isWork = false;
                        break;
                    case '!':
                        isWork = false;
                        break;
                }
            } while (isWork);
        }
        public void OrdersChekcList(string? search, int? IDClient, int? IDWorker, bool? isOrderAccepted)
        {
            int countNotCompleted = 0, 
                countCompleted = 0, 
                countWithoutScore = 0,
                pageLocation = 1;
            bool isWork = true;
            do
            {
                Console.WriteLine("Список загружается, пожалуйста подождите...");
                var orderService = new OrderService();
                var orderListPage = orderService.GetList(
                    new OrderListRequest()
                    {
                        Page = new PageRequest()
                        {
                            Page = pageLocation,
                            PageSize = SizePage
                        },
                        Search = search,
                        IDClient = IDClient,
                        IDWorker = IDWorker,
                        IsOrderAccepted = isOrderAccepted
                    });
                if (!orderListPage.IsSuccess)
                {
                    Console.WriteLine(orderListPage.Message);
                    break;
                }

                double allPage = Math.Ceiling((double)orderListPage.Page!.Count / orderListPage.Page.PageSize);
                Console.Clear();
                orderListPage.Items!.ForEach(o => Console.WriteLine(
                    orderService.GetOrder(new Order()
                    {
                        IDOrder = o.IDOrder,
                        ClientIDClient = o.ClientIDClient,
                        ServiceIDService = o.ServiceIDService,
                        WorkerIDWorker = o.WorkerIDWorker,
                        OrderDescription = o.OrderDescription,
                        IsOrderAccepted = o.IsOrderAccepted,
                        Adress = o.Adress,
                        Price_Service = o.Price_Service,
                        IsRegularCustomer = o.IsRegularCustomer,
                        Sale = o.Sale,
                        PublishedOrder = o.PublishedOrder,
                        TransactionDate = o.TransactionDate,
                        ScoreForWork = o.ScoreForWork,
                        DesriptionForCompletedOrder = o.DesriptionForCompletedOrder
                    })));

                Console.WriteLine($"Страница {pageLocation} из {allPage}");
                Console.Write($"Количество ваших заказов {orderListPage.Page.Count}");
                if (countCompleted > 0 || countNotCompleted > 0 || countWithoutScore > 0)
                {
                    Console.Write("Из них");
                    if (countNotCompleted > 0) Console.WriteLine($"{countNotCompleted} не выполненных");
                    if (countCompleted > 0) Console.Write($"{countCompleted} выполненных");
                    if (countWithoutScore > 0) Console.WriteLine($"{countWithoutScore} без оставленного отзыва");
                }
                string doText = "\nДействие(<, >, !" 
                    + (user!.TypeAccount == EnumUserRanked.Client ? ", Y" : string.Empty)
                    + (user!.TypeAccount == EnumUserRanked.Worker ? ", I" : string.Empty)
                    + "): ";
                Console.Write(doText);
                char choiseDo = Convert.ToChar(Console.ReadLine()!);
                switch (choiseDo)
                {
                    case '>':
                        if (pageLocation < allPage) pageLocation++;
                        break;
                    case '<':
                        if (pageLocation > 1) pageLocation--;
                        break;
                    case 'Y':
                        if (user!.TypeAccount != EnumUserRanked.Client)
                        {
                            Console.WriteLine("У вас нету прав ставить отзывы на заказы!");
                            Console.Write("Нажмите чтобы продолжить: ");
                            Console.ReadKey();
                            break;
                        }
                        if (countWithoutScore <= 0)
                        {
                            Console.WriteLine("У вас нету оформленных заказов.");
                            Console.Write("Нажмите чтобы продолжить: ");
                            Console.ReadKey();
                            break;
                        }
                        Console.Write("Выберите идентификатор заказ за который вы бы хотели поставить оценку: ");
                        int idOrder = int.Parse(Console.ReadLine()!);
                        Console.Write("Какую вы бы поставили оценку по шкале от 1 до 10: ");
                        int score = int.Parse(Console.ReadLine()!);
                        Console.Write("Комментарий к выполненному заказу: ");
                        string description = Console.ReadLine()!;
                        var response = orderService.SetScore(
                            new OrderSetScoreRequest()
                            {
                                IDOrder = idOrder,
                                IDClient = (int)IDClient!,
                                Score = score,
                                Description = description
                            });
                        Console.WriteLine(response.Message);
                        if (response.IsSuccess) isWork = false;
                        break;
                    case 'I':
                        if(user!.TypeAccount != EnumUserRanked.Worker)
                        {
                            Console.WriteLine("У вас нету прав обрабатывать заказы!");
                            Console.Write("Нажмите чтобы продолжить: ");
                            Console.ReadKey();
                            break;
                        }
                        Console.Write("Введите идентификатор заказа: ");
                        int IDOrder = int.Parse(Console.ReadLine()!);
                        var responseAccept = orderService.Accept((int)IDWorker!, IDOrder);
                        Console.WriteLine(responseAccept.Message);
                        break;
                    case '!':
                        isWork = false;
                        break;
                }
            } while (isWork);
        }
        public void WorkersChekcList()
        {

        }
        public void SalaryWorkersChekcList()
        {

        }
        public void ClientsChekcList()
        {

        }
        public void UsersChekcList()
        {

        }
    }
}
