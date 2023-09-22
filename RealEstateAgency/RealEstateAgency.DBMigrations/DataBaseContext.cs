using Microsoft.EntityFrameworkCore;
using RealEstateAgency.Enums;
using RealEstateAgency.Models;

namespace RealEstateAgency.DBMigrations
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
        public DbSet<AcessToActions> AcessToActions { get; set; }
        public DbSet<ReportProblem> ReportProblem { get; set; }
        public DbSet<Admin> Admin { get; set; }
        public DataBaseContext() { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=RealEstateAgency;TrustServerCertificate=True;Trusted_Connection=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(u => u.TypeAccount)
                .HasConversion(
                v => v.ToString(),
                v => (EnumUserRanked)Enum.Parse(typeof(EnumUserRanked), v));

            modelBuilder.Entity<Client>();

            modelBuilder.Entity<Worker>()
                .Property(w => w.JobTitle)
                .HasConversion(
                v => v.ToString(),
                v => (EnumWorkerRanked)Enum.Parse(typeof(EnumWorkerRanked), v));

            modelBuilder.Entity<SalaryWorker>();
            modelBuilder.Entity<Service>();
            modelBuilder.Entity<Order>();
            modelBuilder.Entity<Shift>();
            modelBuilder.Entity<AcessToActions>().HasNoKey();
            modelBuilder.Entity<ReportProblem>().HasNoKey();
            base.OnModelCreating(modelBuilder);
        }
    }
}
