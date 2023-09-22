using RealEstateAgency.DBMigrations;
using RealEstateAgency.Models;

namespace RealEstateAgency.Services.Services
{
    public class SalaryWorkerService
    {
        public List<SalaryWorker> GetSalarysWorker(int IDWorker)
        {
            using var db = new DataBaseContext();
            return db.SalaryWorker
                .Where(sw => sw.WorkerIDWorker == IDWorker).ToList();
        }
        public string GetSalaryWorkerForMonth(SalaryWorker salary)
        {
            using var db = new DataBaseContext();
            var jobTitle = db.Worker.FirstOrDefault(w => w.IDWorker == salary.WorkerIDWorker)!.JobTitle;
            string salaryInformation = "========================================\n"
                + $"Вычет заработной платы №{salary.IDSalary}\n"
                + $"Работник №{salary.WorkerIDWorker}\n"
                + $"Рабочий месяц с {salary.Start_Month} по {salary.End_Month}\n"
                + $"Ставка: {salary.Salary}\n"
                + $"Премия: {salary.PremiumPercentage}%\n"
                + $"Отработанных дней: {salary.DaysWorked} из запланированных {salary.DaysPlanWorked}\n"
                + $"Подоходный налог: {salary.IncomeTaxPercentage}%\n";
            if (jobTitle == Enums.EnumWorkerRanked.Realtor)
                salaryInformation += $"Количество выполненных заказов: {salary.Sales} из {salary.SalesPlan}\n";
            salaryInformation += $"Итого на руки: {salary.OnHand}\n"
                + $"========================================";
            return salaryInformation;
        }
        public void AddSalaryWorkers(int IDWorker, int year, int month, int salary, int planDay, int premiumPr, int salesPlan, int incomeTax)
        {
            int endDayMonth = DateTime.DaysInMonth(year, month);
            DateTime startMonth = Convert.ToDateTime($"{year}-{month}-01 00:00:00");
            DateTime endMonth = Convert.ToDateTime($"{year}-{month}-{endDayMonth} 23:59:59");

            using var db = new DataBaseContext();

            int daysWorked = db.Shift.Where(s => s.WorkerIDWorker == IDWorker
                && s.EndShift > startMonth && s.EndShift < endMonth)
                .Count();
            int sales = db.Order.Where(o => o.IsOrderAccepted
                && o.TransactionDate > startMonth && o.TransactionDate < endMonth)
                .Count();

            int onHand = salary;
            if (premiumPr > 0) onHand += salary * (premiumPr / 100);
            if (planDay > 0) onHand += salary / planDay * daysWorked - salary;
            if (salesPlan > 0) onHand += salary / salesPlan * sales;
            if (incomeTax > 0) onHand -= salary / incomeTax;

            db.SalaryWorker.Add(new SalaryWorker()
            {
                WorkerIDWorker = IDWorker,
                Start_Month = startMonth,
                End_Month = endMonth,
                Salary = salary,
                DaysPlanWorked = planDay,
                DaysWorked = daysWorked,
                PremiumPercentage = premiumPr,
                SalesPlan = salesPlan,
                Sales = sales,
                IncomeTaxPercentage = incomeTax,
                OnHand = onHand
            });
            db.SaveChanges();
        }
    }
}
