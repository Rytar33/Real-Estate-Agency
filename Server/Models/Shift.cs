using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Models
{
    public class Shift
    {
        [Key]
        public int IDShift { get; set; }
        public int IDWorker { get; set; }
        public Worker Worker { get; set; }
        public DateTime StartShift { get; set; }
        public DateTime? EndShift { get; set; }
    }
}
