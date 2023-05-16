using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Models
{
    public class User
    {
        [Key]
        [Display(Name = "ID_User")]
        public int IDUser { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]+[0-9]*$")]
        [Display(Name = "User_Name")]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Type_Account")]
        [StringLength(30)]
        public string TypeAccount { get; set; }

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
                return !SecondName.IsNullOrEmpty() ?
                    $"{LastName} {FirstName} {SecondName}" :
                    $"{LastName} {FirstName}";
            }
        }
        public EnumStatus EnumStatus { get; set; }
    }
}
