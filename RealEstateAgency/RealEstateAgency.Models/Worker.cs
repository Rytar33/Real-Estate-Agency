using RealEstateAgency.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstateAgency.Models
{
    public class Worker
    {
        [Key]
        [Display(Name = "ID_Worker")]
        public int IDWorker { get; set; }
        [Required]
        public int UserIDUser { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public User User { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string? SecondName { get; set; }

        public string FullName
        {
            get
            {
                return !string.IsNullOrWhiteSpace(SecondName) ?
                    $"{LastName} {FirstName} {SecondName}" :
                    $"{LastName} {FirstName}";
            }
        }
        [DataType(DataType.Text)]
        [Display(Name = "Job_Title")]
        public EnumWorkerRanked JobTitle { get; set; }

        [Display(Name = "Right_Change_Workers")]
        public bool RightChangeWorkers { get; set; } = false;

        [Display(Name = "Right_CRU_Accountant")]
        public bool RightCRUAccountant { get; set; } = false;

        [DataType(DataType.Date)]
        public DateTime Start_Date_To_Work { get; set; }

        [DataType(DataType.Date)]
        public DateTime? End_Date_To_Work { get; set; }
    }
}
