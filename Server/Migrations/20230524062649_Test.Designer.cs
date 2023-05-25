﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Server.Context;

#nullable disable

namespace Server.Migrations
{
    [DbContext(typeof(DataBaseContext))]
    [Migration("20230524062649_Test")]
    partial class Test
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Server.Models.Client", b =>
                {
                    b.Property<int>("IDClient")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IDClient"));

                    b.Property<int>("CountPurchasedServices")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecondName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserIDUser")
                        .HasColumnType("int");

                    b.HasKey("IDClient");

                    b.HasIndex("UserIDUser");

                    b.ToTable("Client");
                });

            modelBuilder.Entity("Server.Models.Order", b =>
                {
                    b.Property<int>("IDOrder")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IDOrder"));

                    b.Property<int>("ClientIDClient")
                        .HasColumnType("int");

                    b.Property<string>("DesriptionForCompletedOrder")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IDClient")
                        .HasColumnType("int");

                    b.Property<int>("IDService")
                        .HasColumnType("int");

                    b.Property<int?>("IDWorker")
                        .HasColumnType("int");

                    b.Property<bool>("IsOrderAccepted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsRegularCustomer")
                        .HasColumnType("bit");

                    b.Property<string>("OrderDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price_Service")
                        .HasColumnType("float");

                    b.Property<DateTime>("PublishedOrder")
                        .HasColumnType("datetime2");

                    b.Property<int>("Sale")
                        .HasColumnType("int");

                    b.Property<int?>("ScoreForWork")
                        .HasColumnType("int");

                    b.Property<int>("ServiceIDService")
                        .HasColumnType("int");

                    b.Property<DateTime?>("TransactionDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("WorkerIDWorker")
                        .HasColumnType("int");

                    b.HasKey("IDOrder");

                    b.HasIndex("ClientIDClient");

                    b.HasIndex("ServiceIDService");

                    b.HasIndex("WorkerIDWorker");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("Server.Models.SalaryWorker", b =>
                {
                    b.Property<int>("IDSalary")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IDSalary"));

                    b.Property<int>("DaysPlanWorked")
                        .HasColumnType("int");

                    b.Property<int>("DaysWorked")
                        .HasColumnType("int");

                    b.Property<DateTime>("End_Month")
                        .HasColumnType("datetime2");

                    b.Property<int>("IncomeTaxPercentage")
                        .HasColumnType("int");

                    b.Property<int>("PremiumPercentage")
                        .HasColumnType("int");

                    b.Property<int>("Salary")
                        .HasColumnType("int");

                    b.Property<int>("Sales")
                        .HasColumnType("int");

                    b.Property<int>("SalesPlan")
                        .HasColumnType("int");

                    b.Property<DateTime>("Start_Month")
                        .HasColumnType("datetime2");

                    b.Property<int>("WorkerIDWorker")
                        .HasColumnType("int");

                    b.HasKey("IDSalary");

                    b.HasIndex("WorkerIDWorker");

                    b.ToTable("SalaryWorker");
                });

            modelBuilder.Entity("Server.Models.Service", b =>
                {
                    b.Property<int>("IDService")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IDService"));

                    b.Property<string>("DescriptionService")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("NameService")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<double>("PriceService")
                        .HasColumnType("float");

                    b.Property<string>("TypeService")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IDService");

                    b.ToTable("Service");
                });

            modelBuilder.Entity("Server.Models.Shift", b =>
                {
                    b.Property<int>("IDShift")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IDShift"));

                    b.Property<DateTime?>("EndShift")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartShift")
                        .HasColumnType("datetime2");

                    b.Property<int>("WorkerIDWorker")
                        .HasColumnType("int");

                    b.HasKey("IDShift");

                    b.HasIndex("WorkerIDWorker");

                    b.ToTable("Shift");
                });

            modelBuilder.Entity("Server.Models.User", b =>
                {
                    b.Property<int>("IDUser")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IDUser"));

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecondName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("TypeAccount")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IDUser");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Server.Models.Worker", b =>
                {
                    b.Property<int>("IDWorker")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IDWorker"));

                    b.Property<DateTime?>("End_Date_To_Work")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("JobTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("RightCRUAccountant")
                        .HasColumnType("bit");

                    b.Property<bool>("RightChangeWorkers")
                        .HasColumnType("bit");

                    b.Property<string>("SecondName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Start_Date_To_Work")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserIDUser")
                        .HasColumnType("int");

                    b.HasKey("IDWorker");

                    b.HasIndex("UserIDUser");

                    b.ToTable("Worker");
                });

            modelBuilder.Entity("Server.Models.Client", b =>
                {
                    b.HasOne("Server.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserIDUser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Server.Models.Order", b =>
                {
                    b.HasOne("Server.Models.Client", "Client")
                        .WithMany()
                        .HasForeignKey("ClientIDClient")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Server.Models.Service", "Service")
                        .WithMany()
                        .HasForeignKey("ServiceIDService")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Server.Models.Worker", "Worker")
                        .WithMany()
                        .HasForeignKey("WorkerIDWorker");

                    b.Navigation("Client");

                    b.Navigation("Service");

                    b.Navigation("Worker");
                });

            modelBuilder.Entity("Server.Models.SalaryWorker", b =>
                {
                    b.HasOne("Server.Models.Worker", "Worker")
                        .WithMany()
                        .HasForeignKey("WorkerIDWorker")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Worker");
                });

            modelBuilder.Entity("Server.Models.Shift", b =>
                {
                    b.HasOne("Server.Models.Worker", "Worker")
                        .WithMany()
                        .HasForeignKey("WorkerIDWorker")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Worker");
                });

            modelBuilder.Entity("Server.Models.Worker", b =>
                {
                    b.HasOne("Server.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserIDUser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
