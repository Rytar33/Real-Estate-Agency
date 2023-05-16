using Microsoft.EntityFrameworkCore;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Context
{
    public class DataBaseContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<Worker> Worker { get; set; }
        public DbSet<SalaryWorker> SalaryWorker { get; set; }
        public DbSet<Service> Service { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Shift> Shift { get; set; }
        public DataBaseContext() { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=real_estate_agency;TrustServerCertificate=True;Trusted_Connection=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>();
            modelBuilder.Entity<Client>();
            modelBuilder.Entity<Worker>();
            modelBuilder.Entity<SalaryWorker>();
            modelBuilder.Entity<Service>();
            modelBuilder.Entity<Order>();
            modelBuilder.Entity<Shift>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
