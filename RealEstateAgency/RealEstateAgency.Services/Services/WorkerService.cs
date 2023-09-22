using RealEstateAgency.DBMigrations;
using RealEstateAgency.Enums;
using RealEstateAgency.Models;
using RealEstateAgency.Services.Models;

namespace RealEstateAgency.Services.Services
{
    public class WorkerService
    {
        public BaseResponse AddWorker(int IDUser)
        {
            using var db = new DataBaseContext();
            var user = db.User.FirstOrDefault(u => u.IDUser == IDUser);
            if (user == null)
                return new BaseResponse() { IsSuccess = false, Message = "Пользователь не был найден." };
            if (db.Worker.Any(w => w.UserIDUser == IDUser))
            {
                var worker = db.Worker.FirstOrDefault(w => w.UserIDUser == IDUser);
                worker!.End_Date_To_Work = null;
            }
            else
            {
                db.Worker.Add(new Worker()
                {
                    UserIDUser = IDUser,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    SecondName = user.SecondName,
                    JobTitle = Enums.EnumWorkerRanked.Trainee,
                    RightChangeWorkers = false,
                    RightCRUAccountant = false,
                    Start_Date_To_Work = DateTime.Now
                });
            }
            db.SaveChanges();
            return new BaseResponse() { IsSuccess = true };
        }
        public BaseResponse FireAnEmployee(int IDWorker)
        {
            using var db = new DataBaseContext();
            var workerFind = db.Worker.FirstOrDefault(x => x.IDWorker == IDWorker);
            if (workerFind == null)
                return new BaseResponse() { IsSuccess = false, Message = "Такого работника не найдено." };
            workerFind.End_Date_To_Work = DateTime.Now;
            var user = db.User.FirstOrDefault(u => u.IDUser == workerFind.UserIDUser);
            user!.TypeAccount = Enums.EnumUserRanked.Client;
            db.SaveChanges();
            return new BaseResponse() { IsSuccess = true };
        }
        public List<Worker> GetList()
        {
            using var db = new DataBaseContext();
            return db.Worker.ToList();
        }
        public BaseResponse Change(int IDWorker, bool? isRightChangeWorker = null, bool? isAccountant = null, EnumWorkerRanked? jobTitle = null)
        {
            using var db = new DataBaseContext();
            var worker = db.Worker.FirstOrDefault(w => w.IDWorker == IDWorker);
            if (worker == null)
                return new BaseResponse() { IsSuccess = false, Message = "Такого работника не найдено." };
            if (isRightChangeWorker != null) worker.RightChangeWorkers = (bool)isRightChangeWorker;
            if (isAccountant != null) worker.RightCRUAccountant = (bool)isAccountant;
            if (jobTitle != null) worker.JobTitle = (EnumWorkerRanked)jobTitle;
            db.SaveChanges();
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
            if (workerFind.JobTitle != Enums.EnumWorkerRanked.Аccountant)
            {
                var countAcceptOrder = db.Order.Where(o => o.WorkerIDWorker == workerFind.IDWorker).Count();
                userString += $"Количество обработанных услуг: {countAcceptOrder}\n";
            }
            userString += $"Начало работы: {workerFind.Start_Date_To_Work}\n"
                    + $"Конец работы: {workerFind.End_Date_To_Work}\n"
                    + $"Статус: {userFind.Status}\n"
                    + $"Дата регистрации: {userFind.DateRegistration}\n"
                    + "==========================================";
            return userString;
        }
    }
}
