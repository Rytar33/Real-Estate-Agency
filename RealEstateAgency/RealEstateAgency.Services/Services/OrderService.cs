using Aspose.Words;
using Aspose.Words.Replacing;
using RealEstateAgency.Services.Models;
using RealEstateAgency.DBMigrations;
using RealEstateAgency.Models;
using RealEstateAgency.Services.Models.Orders;
using RealEstateAgency.Services.Models.Pages;
using RealEstateAgency.Services.Validators;

namespace RealEstateAgency.Services.Services
{
    public class OrderService
    {
        public BaseResponse Add(OrderCreateRequest request)
        {
            using var db = new DataBaseContext();
            var service = db.Service.FirstOrDefault(s => s.IDService == request.IDService);
            if (service == null)
                return new BaseResponse() { IsSuccess = false, Message = $"Сервиса №{request.IDService} не было найденно." };

            request.Price_Service = request.IsRegularCustomer ? service.PriceService * 0.9 : service.PriceService;
            request.Sale = request.IsRegularCustomer ? 10 : 0;

            db.Add(new Order()
            {
                ClientIDClient = request.IDClient,
                IsRegularCustomer = request.IsRegularCustomer,
                ServiceIDService = request.IDService,
                Sale = request.Sale,
                Price_Service = request.Price_Service,
                OrderDescription = request.OrderDescription!,
                PublishedOrder = request.PublishedOrder,
                IsOrderAccepted = request.IsOrderAccepted,
                Adress = request.Adress
            });
            db.SaveChanges();
            return new BaseResponse() { IsSuccess = true, Message = "Ваш заказ успешно отправлен. Ожидайте пока сотрудник обработает его." };
        }
        public BaseResponse Accept(int IDWorker, int IDOrder)
        {
            using var db = new DataBaseContext();
            var orderAccept = db.Order.FirstOrDefault(o => o.IDOrder == IDOrder);
            var worker = db.Worker.FirstOrDefault(w => w.IDWorker == IDWorker);
            if (orderAccept == null)
                return new BaseResponse() { IsSuccess = false, Message = $"Заказ с номером {IDOrder} не найден." };
            if (worker == null)
                return new BaseResponse()
                {
                    IsSuccess = false,
                    Message = $"Работника под номером {IDWorker} не было найдено."
                };
            var client = db.Client.FirstOrDefault(c => c.IDClient == orderAccept.ClientIDClient);
            client!.CountPurchasedServices++;
            orderAccept.WorkerIDWorker = worker.IDWorker;
            orderAccept.IsOrderAccepted = true;
            orderAccept.TransactionDate = DateTime.Now;
            db.SaveChanges();
            return new BaseResponse() { IsSuccess = true, Message = $"Заказ №{IDOrder} успешно принят." };
        }
        public BaseResponse SetScore(OrderSetScoreRequest request)
        {
            using var db = new DataBaseContext();
            var order = db.Order.FirstOrDefault(o => o.IDOrder == request.IDOrder && o.ClientIDClient == request.IDClient);
            if (order == null) return new BaseResponse() { IsSuccess = false, Message = "Такого заказа не было найденно и/или вы его не оформляли." };
            if (!order.IsOrderAccepted) return new BaseResponse() { IsSuccess = false, Message = "Данный заказ ещё не обработан." };
            if (order.ScoreForWork != null) return new BaseResponse() { IsSuccess = false, Message = "Данный заказ уже имеет оценку." };

            var checkValidation = request.CheckValidation();
            if (!checkValidation.IsSuccess) return checkValidation;

            order.ScoreForWork = request.Score;
            order.DesriptionForCompletedOrder = request.Description;
            db.SaveChanges();
            return new BaseResponse() { IsSuccess = true, Message = "Ваша оценка успешно выставленна. Спасибо за оставленный вами отзыв!" };
        }
        public OrderListResponse GetList(OrderListRequest request)
        {
            using var db = new DataBaseContext();

            var ordersForConditions = db.Order.AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Search))
                ordersForConditions = ordersForConditions
                    .Where(o => o.OrderDescription.Contains(request.Search)
                    || o.Adress != null && o.Adress.Contains(request.Search)
                    || o.DesriptionForCompletedOrder != null && o.DesriptionForCompletedOrder.Contains(request.Search));
            if (request.IsOrderAccepted != null)
                ordersForConditions = ordersForConditions
                    .Where(o => o.IsOrderAccepted == request.IsOrderAccepted);
            if (request.IDClient != null)
                ordersForConditions = ordersForConditions
                    .Where(o => o.ClientIDClient == request.IDClient);
            if (request.IDWorker != null)
                ordersForConditions = ordersForConditions
                    .Where(o => o.WorkerIDWorker == request.IDWorker);

            int countOrders = ordersForConditions.Count();
            if(countOrders == 0)
                return new OrderListResponse()
                {
                    Page = new PageResponse()
                    {
                        Page = request.Page!.Page!.Value,
                        PageSize = request.Page!.PageSize!.Value,
                        Count = countOrders
                    },
                    IsSuccess = false,
                    Message = "По вашему запросу, заказов не найденно."
                };
            int countNotCompleted = ordersForConditions
                .Where(o => !o.IsOrderAccepted).Count();
            int countCompleted = ordersForConditions
                .Where(o => o.IsOrderAccepted).Count();
            int countWithoutSetScore = ordersForConditions
                .Where(o => o.IsOrderAccepted && o.ScoreForWork == null).Count();

            ordersForConditions = ordersForConditions
                .Skip((request.Page!.Page!.Value - 1) * request.Page!.PageSize!.Value)
                .Take(request.Page!.PageSize!.Value);

            var orders = ordersForConditions.Select(o =>
                new OrderListItem()
                {
                    IDOrder = o.IDOrder,
                    OrderDescription = o.OrderDescription,
                    ClientIDClient = o.ClientIDClient,
                    WorkerIDWorker = o.WorkerIDWorker,
                    ServiceIDService = o.ServiceIDService,
                    Adress = o.Adress!,
                    IsOrderAccepted = o.IsOrderAccepted,
                    IsRegularCustomer = o.IsRegularCustomer,
                    Price_Service = o.Price_Service,
                    PublishedOrder = o.PublishedOrder,
                    Sale = o.Sale,
                    TransactionDate = o.TransactionDate,
                    ScoreForWork = o.ScoreForWork,
                    DesriptionForCompletedOrder = o.DesriptionForCompletedOrder
                }
            ).ToList();

            return new OrderListResponse()
            {
                Items = orders,
                CountCompletedOrder = countCompleted,
                CountNotCompletedOrder = countNotCompleted,
                CountWithoutSetScore = countWithoutSetScore,
                Page = new PageResponse()
                {
                    Page = request.Page!.Page!.Value,
                    PageSize = request.Page!.PageSize!.Value,
                    Count = countOrders
                },
                IsSuccess = true
            };
        }
        public void CreateWordFile(int IDOrder)
        {
            using var db = new DataBaseContext();
            var order = db.Order.FirstOrDefault(o => o.IDOrder == IDOrder);
            if (order != null) Console.WriteLine(GetOrder(order));
            else Console.WriteLine($"Заказ под номером {IDOrder} не был найден.");
            Dictionary<string, string> keyValuesTextReplace = new Dictionary<string, string>()
            {
                ["_Place_Of_Contract_"] = "Место заключение договора",
                ["_Date_Of_Signing_The_Contract_"] = "Дата подписание договора",
                ["_Full_Name_Real_Estate_Agency_"] = "Полное наименовение агенства недвижимости",
                ["_LSFName_Realtor_And_Passport_Data_"] = "ФИО, паспортные данные риэлтора",
                ["_Date_And_Issue_Of_Power_Attorney_"] = "Номер и дата выдачи доверенности",
                ["_LSFName_Customer_"] = "Фамилия, имя, отчество заказчика",
                ["_Pass_Series_Customer_"] = "Заказчик) Паспорт серии",
                ["_Pass_Customer_"] = "№ паспорта",
                ["_Date_Issue_Of_The_Passport_And_The_Name_Issuing_Authority_"] = "Дата выдачи паспорта и наименование выдавшего его органа",
                ["_Place_Of_Living_Customer_"] = "Место проживание заказчика",
                ["_The_City_Of_"] = "Квартира должна находиться в городе",
                ["_Rooms_"] = "Квартира должна состоять из комнат в количестве",
                ["_Apartment_Must_Be_In_House_Type_"] = "Квартира должна находиться в доме (кирпичном, панельном, деревянном и т.д.)",
                ["_Floors_House_No_More_"] = "Этажность дома не более",
                ["_Living_Area_Apartment_Not_Less_Than_"] = "Жилая площадь квартиры не менее",
                ["_Total_Area_Apartment_Not_Less_Than_"] = "Общая площадь квартиры не менее",
                ["_Area_Infrastructure_Requirements_"] = "Требования по инфраструктуре района",
                ["_Price_To_"] = "Стоимость квартиры до",
                ["_Sign_Act_No_Later_Than_Days_"] = "Не позднее скольки рабочих дней заказчик обязуется подписать акт",
                ["_Bank_Days_"] = "Заказчик оплачивает услугу исполнителя в течении (банковских дней)",
                ["_In_Case_Of_Delay_-_A_Surcharge_"] = "В случае просрочки оплаты Исполнитель вправе взыскать с Заказчика штраф из расчета(%)",
            };
            var doc = new Document("blank_.doc");
            var builder = new DocumentBuilder(doc);
            foreach (var line in keyValuesTextReplace)
            {
                Console.Write(line.Value + ": ");
                string replaceText = Console.ReadLine()!;
                doc.Range.Replace(line.Key, replaceText, new FindReplaceOptions());
            }
            doc.Save($"{DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()}Output.doc");
        }
        public string GetOrder(Order order)
        {
            string border = "====================================";
            using var db = new DataBaseContext();
            var idUser = db.Client.FirstOrDefault(u => u.IDClient == order.ClientIDClient)!.UserIDUser;
            var userName = db.User.FirstOrDefault(u => u.IDUser == idUser);
            var service = db.Service.FirstOrDefault(s => s.IDService == order.ServiceIDService);
            string orderInf = $"{border}\n"
                    + $"Заказ №{order.IDOrder}\n"
                    + $"Пользователь {userName.UserName}\n"
                    + $"Услуга: {service.NameService}\n"
                    + $"Описание к заказу: {order.OrderDescription}\n"
                    + $"Цена за заказ(с учетом скидки): {order.Price_Service - order.Price_Service * (order.Sale / 100)}\n"
                    + $"Скидка {order.Sale}%\n"
                    + $"Дата отправки заказа: {order.PublishedOrder}\n";

            if (!order.IsOrderAccepted) return orderInf + border;

            var worker = db.Worker.FirstOrDefault(w => w.IDWorker == order.WorkerIDWorker);
            orderInf += $"Дата принятия заказа: {order.TransactionDate}\n"
                + $"Принял сотрудник: {worker!.FullName}\n";

            if (order.ScoreForWork != null)
                orderInf += $"Оценка за выполненный заказ: {order.ScoreForWork}/10\n";
            if (!string.IsNullOrWhiteSpace(order.DesriptionForCompletedOrder))
                orderInf += $"Комментарий к тому как выполнен заказ: {order.DesriptionForCompletedOrder}\n";
            return orderInf + border;
        }
    }
}
