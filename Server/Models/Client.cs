﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Models
{
    public class Client
    {
        [Key]
        public int IDClient { get; set; }
        public int UserIDUser { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public User User { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? SecondName { get; set; }
        public int CountPurchasedServices { get; set; }
        public bool IsRegularCustomer { get { return CountPurchasedServices > 6 ? true : false; } }
    }
}
