using RealEstateAgency.Enums;
using System.ComponentModel.DataAnnotations;

namespace RealEstateAgency.Models
{
    public class User
    {
        [Key]
        [Display(Name = "ID_User")]
        public int IDUser { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]+[0-9]*$")]
        [Display(Name = "User_Name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [StringLength(311, MinimumLength = 6)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Type_Account")]
        [StringLength(30)]
        public EnumUserRanked TypeAccount { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]+[А-Я]+[а-яА-Я""'\s-]*$")]
        [Display(Name = "First_Name")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Имя должно быть в диапозоне от 2 до 50 символов.")]
        public string FirstName { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]+[А-Я]+[а-яА-Я""'\s-]*$")]
        [Display(Name = "Last_Name")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Фамилия должна быть в диапозоне от 2 до 50 символов.")]
        public string LastName { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]+[А-Я]+[а-яА-Я""'\s-]*$")]
        [Display(Name = "Second_Name")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Отчество должно быть в диапозоне от 2 до 50 символов.")]
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
        public EnumUserStatus Status { get; set; }
        public DateTime DateRegistration { get; set; }
        public string? TokenRecovery { get; set; }
    }
}
