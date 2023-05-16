using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Models
{
    public class Service
    {
        [Key]
        public int IDService { get; set; }
        [StringLength(60, MinimumLength = 4)]
        public string NameService { get; set; }
        [StringLength(500)]
        public string? DescriptionService { get; set; }
        public double PriceService { get; set; }
        public string TypeService { get; set; }
    }
}
