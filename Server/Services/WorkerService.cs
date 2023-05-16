using Server.Context;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services
{
    public class WorkerService
    {
        public BaseResponse CreateWorker(User newUser)
        {
            var response = new UserService().SignUp(newUser);
            if (!response.IsSuccess) return response;
            using var db = new DataBaseContext();
            return new BaseResponse() { IsSuccess = true };
        }
        public BaseResponse AddWorker(int IDUser)
        {
            using var db = new DataBaseContext();
            return new BaseResponse() { IsSuccess = true };
        }
        public BaseResponse FireAnEmployee(int IDWorker)
        {
            using var db = new DataBaseContext();
            return new BaseResponse() { IsSuccess = true };
        }
        public void PrintList()
        {
            using var db = new DataBaseContext();
            db.Worker.ToList().ForEach(worker => Console.WriteLine(GetUserWorker(worker.User.IDUser)));
        }
        public BaseResponse Change(Worker worker)
        {
            using var db = new DataBaseContext();
            return new BaseResponse() { IsSuccess = true };
        }
        public Worker FoundWorker(User user)
        {
            using var db = new DataBaseContext();
            var worker = db.Worker.FirstOrDefault(x => x.User.IDUser == user.IDUser);
            return worker != null ? worker : null;
        }
        public string GetUserWorker(int IDUser)
        {
            using var db = new DataBaseContext();
            var userFind = db.User.FirstOrDefault(u => u.IDUser == IDUser);
            if (userFind == null) return $"Пользователь №{IDUser} не был найден.";
            var workerFind = db.Worker.FirstOrDefault(w => w.User.IDUser == IDUser);
            var workerShiftAll = db.Shift.TakeWhile(s => s.IDWorker == workerFind.IDWorker);
            string userString =
                   "==========================================\n"
                + $"Пользователь №{userFind.IDUser}\n"
                + $"Работник №{workerFind.IDWorker}\n"
                + $"Имя пользователя: {userFind.UserName}\n"
                + $"Полное ФИО: {userFind.FullName}\n"
                + $"Должность: {workerFind.JobTitle}\n"
                + $"Количество отработанных дней: {workerShiftAll.Count()}\n";
            if (workerFind.JobTitle != "Accountant")
            {
                var countAcceptOrder = db.Order.TakeWhile(o => o.Worker.IDWorker == workerFind.IDWorker).Count();
                userString += $"Количество обработанных услуг: {countAcceptOrder}\n";
            }
            userString += $"Начало работы: {workerFind.Start_Date_To_Work}\n"
                    + $"Конец работы: {workerFind.End_Date_To_Work}\n"
                    + $"Статус: {userFind.EnumStatus}\n"
                    + "==========================================";
            return userString;
        }
    }
}
