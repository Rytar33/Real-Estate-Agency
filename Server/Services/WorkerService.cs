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
            var workerFind = db.Worker.FirstOrDefault(x => x.IDWorker == IDWorker);
            if(workerFind == null)
                return new BaseResponse() { IsSuccess = false, Message = "Такого работника не найдено." };
            workerFind.End_Date_To_Work = DateTime.Now;
            var user = db.User.FirstOrDefault(u => u.IDUser == workerFind.UserIDUser);
            user!.TypeAccount = "Client";

            return new BaseResponse() { IsSuccess = true };
        }
        public List<Worker> GetList()
        {
            using var db = new DataBaseContext();
            return db.Worker.ToList();
        }
        public BaseResponse Change(Worker worker)
        {
            using var db = new DataBaseContext();
            return new BaseResponse() { IsSuccess = true };
        }
        public Worker? FoundWorker(User user)
        {
            using var db = new DataBaseContext();
            var worker = db.Worker.FirstOrDefault(x => x.UserIDUser == user.IDUser);
            return worker;
        }
        public string GetUserWorker(int IDUser)
        {
            using var db = new DataBaseContext();
            var userFind = db.User.FirstOrDefault(u => u.IDUser == IDUser);
            if (userFind == null) return $"Пользователь №{IDUser} не был найден.";
            var workerFind = db.Worker.FirstOrDefault(w => w.UserIDUser == IDUser);
            var workerShiftAll = db.Shift.Where(s => s.WorkerIDWorker == workerFind!.IDWorker).ToList();
            string userString =
                   "==========================================\n"
                + $"Пользователь №{userFind.IDUser}\n"
                + $"Работник №{workerFind!.IDWorker}\n"
                + $"Имя пользователя: {userFind.UserName}\n"
                + $"Полное ФИО: {userFind.FullName}\n"
                + $"Должность: {workerFind.JobTitle}\n"
                + $"Количество отработанных дней: {workerShiftAll.Count()}\n";
            if (workerFind.JobTitle != "Accountant")
            {
                var countAcceptOrder = db.Order.Where(o => o.WorkerIDWorker == workerFind.IDWorker).Count();
                userString += $"Количество обработанных услуг: {countAcceptOrder}\n";
            }
            userString += $"Начало работы: {workerFind.Start_Date_To_Work}\n"
                    + $"Конец работы: {workerFind.End_Date_To_Work}\n"
                    + $"Статус: {userFind.Status}\n"
                    + "==========================================";
            return userString;
        }
    }
}
