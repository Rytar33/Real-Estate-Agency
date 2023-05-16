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
        public BaseResponse Add(Order order)
        {
            using var db = new DataBaseContext();
            db.Add(order);
            db.SaveChanges();
            return new BaseResponse() { IsSuccess = true, Message = "Ваш заказ успешно отправлен. Ожидайте пока сотрудник обработает его." };
        }
        public BaseResponse Accept(int IDWorker, int IDOrder)
        {
            using var db = new DataBaseContext();
            var orderAccept = db.Order.FirstOrDefault(o => o.IDOrder == IDOrder);
            if (orderAccept == null)
                return new BaseResponse() { IsSuccess = false, Message = $"Заказ с номером {IDOrder} не найден." };
            orderAccept.Worker.IDWorker = IDWorker;
            orderAccept.IsOrderAccepted = true;
            orderAccept.TransactionDate = DateTime.Now;
            db.Order.Update(orderAccept);
            db.SaveChanges();
            return new BaseResponse() { IsSuccess = true, Message = $"Заказ №{IDOrder} успешно принят." };
        }
        public void GetList(bool? IsNotAccept)
        {
            using var db = new DataBaseContext();
            var listOrders = db.Order.AsQueryable();
            if (IsNotAccept != null)
            {
                if ((bool)IsNotAccept)
                    listOrders = listOrders
                        .Where(o => !o.IsOrderAccepted);
            }
            db.Order.ToList().ForEach(o => Console.WriteLine(PrintOrder(o)));
        }
        public void CreateWordFile(int IDOrder)
        {
            using var db = new DataBaseContext();
            var order = db.Order.FirstOrDefault(o => o.IDOrder == IDOrder);
            if (order != null) Console.WriteLine(PrintOrder(order));
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
                string replaceText = Console.ReadLine();
                doc.Range.Replace(line.Key, replaceText, new FindReplaceOptions());
            }
            doc.Save($"{DateTime.Now.ToShortDateString()}Output.doc");
        }
        public string PrintOrder(Order order)
        {
            using var db = new DataBaseContext();
            var userName = db.User.FirstOrDefault(u => u.IDUser == order.Client.User.IDUser);
            var service = db.Service.FirstOrDefault(s => s.IDService == order.Service.IDService);
            string orderInf = "====================================\n"
                    + $"Заказ №{order.IDOrder}\n"
                    + $"Пользователь {userName.UserName}\n"
                    + $"Услуга: {service.NameService}\n"
                    + $"Описание к заказу: {order.OrderDescription}\n"
                    + $"Цена за заказ(с учетом скидки): {order.Price_Service - (order.Price_Service * (order.Sale / 100))}\n"
                    + $"Скидка {order.Sale}%\n"
                    + $"Дата отправки заказа: {order.PublishedOrder}\n";
            if (!order.IsOrderAccepted)
                return orderInf + "====================================";

            var worker = db.Worker.FirstOrDefault(w => w.IDWorker == order.Worker.IDWorker);
            return orderInf 
                + $"Дата принятия заказа: {order.TransactionDate}\n"
                + $"Принял сотрудник: {worker.FullName}\n"
                + "====================================";
        }
    }
}
