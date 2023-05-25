using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Models
{
    public class Order
    {
        [Key]
        public int IDOrder { get; set; }
        public int ClientIDClient { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Client Client { get; set; }
        public int? WorkerIDWorker { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Worker? Worker { get; set; }
        public int ServiceIDService { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Service Service { get; set; }
        public bool IsRegularCustomer { get; set; }
        public int Sale { get; set; }
        public double Price_Service { get; set; }
        public string OrderDescription { get; set; }

        public DateTime? TransactionDate { get; set; }

        public DateTime PublishedOrder { get; set; }

        public bool IsOrderAccepted { get; set; }
        public string? DesriptionForCompletedOrder { get; set; }
        public int? ScoreForWork { get; set; }
    }
}
