using Server.Context;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services
{
    public class SalaryWorkerService
    {
        public SalaryWorker? CheckSalaryWorker(DateTime dateMonthYear, int IDWorker)
        {
            using var db = new DataBaseContext();
            return db.SalaryWorker
                .Where(sw => sw.Worker.IDWorker == IDWorker)
                .FirstOrDefault(w => w.Start_Month == dateMonthYear);
        }
        public string PrintSalaryWorkerForMonth(SalaryWorker salary)
        {
            using var db = new DataBaseContext();
            var jobTitle = db.Worker.FirstOrDefault(w => w.IDWorker == salary.Worker.IDWorker)!.JobTitle;
            string salaryInformation = "========================================\n"
                + $"Вычет заработной платы №{salary.IDSalary}\n"
                + $"Работник №{salary.Worker.IDWorker}\n"
                + $"Рабочий месяц с {salary.Start_Month} по {salary.End_Month}\n"
                + $"Ставка: {salary.Salary}\n"
                + $"Премия: {salary.PremiumPercentage}%\n"
                + $"Отработанных дней: {salary.DaysWorked} из запланированных {salary.DaysPlanWorked}\n"
                + $"Подоходный налог: {salary.IncomeTaxPercentage}%\n";
            if (jobTitle == "Realtor")
                salaryInformation += $"Количество выполненных заказов: {salary.Sales} из {salary.SalesPlan}\n";
            salaryInformation += $"Итого на руки: {salary.OnHand}\n"
                + $"========================================";
            return salaryInformation;
        }
        public void AddSalaryWorkers(SalaryWorker salary)
        {
            using var db = new DataBaseContext();
            db.SalaryWorker.Add(salary);
            db.SaveChanges();
        }
    }
}
