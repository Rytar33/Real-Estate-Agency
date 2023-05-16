using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Server.Models
{
    public class SalaryWorker
    {
        [Key]
        public int IDSalary { get; set; }
        public Worker Worker { get; set; }
        [DataType(DataType.Date)]
        public DateTime Start_Month { get; set; }
        [DataType(DataType.Date)]
        public DateTime End_Month { get; set; }
        public int DaysPlanWorked { get; set; }
        public int DaysWorked { get; set; }
        public int Salary { get; set; }
        public int IncomeTaxPercentage { get; set; }
        public int SalesPlan { get; set; }
        public int Sales { get; set; }
        public int PremiumPercentage { get; set; }
        public double OnHand {
            get {
                return Salary 
                    + (Salary * (PremiumPercentage / 100))
                    + (((Salary / DaysPlanWorked) * DaysWorked) - Salary)
                    + (((Salary / SalesPlan) * Sales))
                    - (Salary / IncomeTaxPercentage); 
            }
        }
    }
}
