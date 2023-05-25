using Server.Context;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Aspose.Words;
using Aspose.Words.Replacing;

namespace Server.Services
{
    public class OrderService
    {
        public BaseResponse Add(int IDClient, int IDService, string descriptionOrder)
        {
            using var db = new DataBaseContext();
            var client = db.Client.FirstOrDefault(c => c.IDClient == IDClient);
            var service = db.Service.FirstOrDefault(s => s.IDService == IDService);
            db.Add(new Order() 
            {
                Client = client,
                IsRegularCustomer = client.IsRegularCustomer,
                Service = service,
                Sale = (client.IsRegularCustomer) ? 10 : 0,
                Price_Service = service.PriceService,
                OrderDescription = descriptionOrder,
                PublishedOrder = DateTime.Now
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
                    Message = $"Работника под номером {IDWorker} не было найдено. Обратитесь к администратору."
                };
            orderAccept.Worker = worker;
            orderAccept.IsOrderAccepted = true;
            orderAccept.TransactionDate = DateTime.Now;
            db.SaveChanges();
            return new BaseResponse() { IsSuccess = true, Message = $"Заказ №{IDOrder} успешно принят." };
        }
        public BaseResponse SetScore(int IDClient, int IDOrder, int score, string description = "")
        {
            using var db = new DataBaseContext();
            var order = db.Order.FirstOrDefault(o => o.IDOrder == IDOrder && o.Client.IDClient == IDClient);
            if (order == null) return new BaseResponse() { IsSuccess = false, Message = "Такого заказа не было найденно и/или вы его не оформляли." };
            if (order.ScoreForWork != null) return new BaseResponse() { IsSuccess = false, Message = "Данный заказ уже имеет оценку." };
            order.ScoreForWork = score;
            order.DesriptionForCompletedOrder = description;
            db.SaveChanges();
            return new BaseResponse() { IsSuccess = true, Message = "Ваша оценка успешно выставленна. Спасибо за оставленный вами отзыв!" };
        }
        public List<Order> GetList(bool? IsAccept = null, int? IDClient = null, int? IDWorker = null)
        {
            using var db = new DataBaseContext();
            var listOrders = db.Order.AsQueryable();
            if (IDClient != null)
                listOrders = listOrders
                    .Where(o => o.Client.IDClient == IDClient);
            if (IDWorker != null)
                listOrders = listOrders
                    .Where(o => o.Worker != null && o.Worker.IDWorker == IDWorker);
            if (IsAccept != null)
                listOrders = listOrders
                    .Where(o => o.IsOrderAccepted == IsAccept);
            return listOrders.ToList();
        }
        public void CreateWordFile(int IDOrder)
        {
            using var db = new DataBaseContext();
            var order = db.Order.FirstOrDefault(o => o.IDOrder == IDOrder);
            if (order != null) Console.WriteLine(GetOrder(order));
            else Console.WriteLine($"Заказ под номером {IDOrder} не был найден.");
            Dictionary<string, string> keyValuesTextReplace = new Dictionary<string, string>()
            {
                ["_Place_Of_Contract_"] =                                         "Место заключение договора",
                ["_Date_Of_Signing_The_Contract_"] =                              "Дата подписание договора",
                ["_Full_Name_Real_Estate_Agency_"] =                              "Полное наименовение агенства недвижимости",
                ["_LSFName_Realtor_And_Passport_Data_"] =                         "ФИО, паспортные данные риэлтора",
                ["_Date_And_Issue_Of_Power_Attorney_"] =                          "Номер и дата выдачи доверенности",
                ["_LSFName_Customer_"] =                                          "Фамилия, имя, отчество заказчика",
                ["_Pass_Series_Customer_"] =                                      "Заказчик) Паспорт серии",
                ["_Pass_Customer_"] =                                             "№ паспорта",
                ["_Date_Issue_Of_The_Passport_And_The_Name_Issuing_Authority_"] = "Дата выдачи паспорта и наименование выдавшего его органа",
                ["_Place_Of_Living_Customer_"] =                                  "Место проживание заказчика",
                ["_The_City_Of_"] =                                               "Квартира должна находиться в городе",
                ["_Rooms_"] =                                                     "Квартира должна состоять из комнат в количестве",
                ["_Apartment_Must_Be_In_House_Type_"] =                           "Квартира должна находиться в доме (кирпичном, панельном, деревянном и т.д.)",
                ["_Floors_House_No_More_"] =                                      "Этажность дома не более",
                ["_Living_Area_Apartment_Not_Less_Than_"] =                       "Жилая площадь квартиры не менее",
                ["_Total_Area_Apartment_Not_Less_Than_"] =                        "Общая площадь квартиры не менее",
                ["_Area_Infrastructure_Requirements_"] =                          "Требования по инфраструктуре района",
                ["_Price_To_"] =                                                  "Стоимость квартиры до",
                ["_Sign_Act_No_Later_Than_Days_"] =                               "Не позднее скольки рабочих дней заказчик обязуется подписать акт",
                ["_Bank_Days_"] =                                                 "Заказчик оплачивает услугу исполнителя в течении (банковских дней)",
                ["_In_Case_Of_Delay_-_A_Surcharge_"] =                            "В случае просрочки оплаты Исполнитель вправе взыскать с Заказчика штраф из расчета(%)",
            };
            var doc = new Aspose.Words.Document("blank_.doc");
            var builder = new DocumentBuilder(doc);
            foreach (var line in keyValuesTextReplace)
            {
                Console.Write(line.Value + ": ");
                string replaceText = Console.ReadLine()!;
                doc.Range.Replace(line.Key, replaceText, new FindReplaceOptions());
            }
            doc.Save($"{DateTime.Now.ToShortDateString()}Output.doc");
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
                    + $"Цена за заказ(с учетом скидки): {order.Price_Service - (order.Price_Service * (order.Sale / 100))}\n"
                    + $"Скидка {order.Sale}%\n"
                    + $"Дата отправки заказа: {order.PublishedOrder}\n";

            if (!order.IsOrderAccepted) return orderInf + border;

            var worker = db.Worker.FirstOrDefault(w => w.IDWorker == order.WorkerIDWorker);
            orderInf += $"Дата принятия заказа: {order.TransactionDate}\n"
                + $"Принял сотрудник: {worker.FullName}\n";

            if(order.ScoreForWork != null)
                orderInf += $"Оценка за выполненный заказ: {order.ScoreForWork}/10\n";
            if (!string.IsNullOrWhiteSpace(order.DesriptionForCompletedOrder))
                orderInf += $"Комментарий к тому как выполнен заказ: {order.DesriptionForCompletedOrder}\n";
            return orderInf + border;
        }
    }
}
