using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Models
{
    public class Order
    {
        [Key]
        public int IDOrder { get; set; }
        public Client Client { get; set; }
        public Worker? Worker { get; set; }
        public Service Service { get; set; }
        public bool IsRegularCustomer { get; set; }
        public int Sale { get; set; }
        public double Price_Service { get; set; }
        public string OrderDescription { get; set; }

        public DateTime? TransactionDate { get; set; }

        public DateTime PublishedOrder { get; set; }

        public bool IsOrderAccepted { get; set; }
    }
}
