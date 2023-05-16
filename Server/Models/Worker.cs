using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Models
{
    public class Worker
    {
        [Key]
        [Display(Name = "ID_Worker")]
        public int IDWorker { get; set; }
        public User User { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string? SecondName { get; set; }

        public string FullName
        {
            get
            {
                return !SecondName.IsNullOrEmpty() ?
                    $"{LastName} {FirstName} {SecondName}" :
                    $"{LastName} {FirstName}";
            }
        }

        [Display(Name = "Job_Title")]
        public string JobTitle { get; set; }

        [Display(Name = "Right_Change_Workers")]
        public bool RightChangeWorkers { get; set; }

        [Display(Name = "Right_CRU_Accountant")]
        public bool RightCRUAccountant { get; set; }

        [DataType(DataType.Date)]
        public DateTime Start_Date_To_Work { get; set; }

        [DataType(DataType.Date)]
        public DateTime? End_Date_To_Work { get; set; }
    }
}
