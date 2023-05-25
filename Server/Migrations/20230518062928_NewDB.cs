using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class NewDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Service",
                columns: table => new
                {
                    IDService = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameService = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    DescriptionService = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PriceService = table.Column<double>(type: "float", nullable: false),
                    TypeService = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Service", x => x.IDService);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    IDUser = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeAccount = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SecondName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.IDUser);
                });

            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    IDClient = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserIDUser = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SecondName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountPurchasedServices = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.IDClient);
                    table.ForeignKey(
                        name: "FK_Client_User_UserIDUser",
                        column: x => x.UserIDUser,
                        principalTable: "User",
                        principalColumn: "IDUser",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Worker",
                columns: table => new
                {
                    IDWorker = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserIDUser = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SecondName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JobTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RightChangeWorkers = table.Column<bool>(type: "bit", nullable: false),
                    RightCRUAccountant = table.Column<bool>(type: "bit", nullable: false),
                    Start_Date_To_Work = table.Column<DateTime>(type: "datetime2", nullable: false),
                    End_Date_To_Work = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Worker", x => x.IDWorker);
                    table.ForeignKey(
                        name: "FK_Worker_User_UserIDUser",
                        column: x => x.UserIDUser,
                        principalTable: "User",
                        principalColumn: "IDUser",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    IDOrder = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientIDClient = table.Column<int>(type: "int", nullable: false),
                    WorkerIDWorker = table.Column<int>(type: "int", nullable: true),
                    ServiceIDService = table.Column<int>(type: "int", nullable: false),
                    IsRegularCustomer = table.Column<bool>(type: "bit", nullable: false),
                    Sale = table.Column<int>(type: "int", nullable: false),
                    Price_Service = table.Column<double>(type: "float", nullable: false),
                    OrderDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PublishedOrder = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsOrderAccepted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.IDOrder);
                    table.ForeignKey(
                        name: "FK_Order_Client_ClientIDClient",
                        column: x => x.ClientIDClient,
                        principalTable: "Client",
                        principalColumn: "IDClient",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Order_Service_ServiceIDService",
                        column: x => x.ServiceIDService,
                        principalTable: "Service",
                        principalColumn: "IDService",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Order_Worker_WorkerIDWorker",
                        column: x => x.WorkerIDWorker,
                        principalTable: "Worker",
                        principalColumn: "IDWorker");
                });

            migrationBuilder.CreateTable(
                name: "SalaryWorker",
                columns: table => new
                {
                    IDSalary = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkerIDWorker = table.Column<int>(type: "int", nullable: false),
                    Start_Month = table.Column<DateTime>(type: "datetime2", nullable: false),
                    End_Month = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DaysPlanWorked = table.Column<int>(type: "int", nullable: false),
                    DaysWorked = table.Column<int>(type: "int", nullable: false),
                    Salary = table.Column<int>(type: "int", nullable: false),
                    IncomeTaxPercentage = table.Column<int>(type: "int", nullable: false),
                    SalesPlan = table.Column<int>(type: "int", nullable: false),
                    Sales = table.Column<int>(type: "int", nullable: false),
                    PremiumPercentage = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalaryWorker", x => x.IDSalary);
                    table.ForeignKey(
                        name: "FK_SalaryWorker_Worker_WorkerIDWorker",
                        column: x => x.WorkerIDWorker,
                        principalTable: "Worker",
                        principalColumn: "IDWorker",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Shift",
                columns: table => new
                {
                    IDShift = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkerIDWorker = table.Column<int>(type: "int", nullable: false),
                    StartShift = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndShift = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shift", x => x.IDShift);
                    table.ForeignKey(
                        name: "FK_Shift_Worker_WorkerIDWorker",
                        column: x => x.WorkerIDWorker,
                        principalTable: "Worker",
                        principalColumn: "IDWorker",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Client_UserIDUser",
                table: "Client",
                column: "UserIDUser");

            migrationBuilder.CreateIndex(
                name: "IX_Order_ClientIDClient",
                table: "Order",
                column: "ClientIDClient");

            migrationBuilder.CreateIndex(
                name: "IX_Order_ServiceIDService",
                table: "Order",
                column: "ServiceIDService");

            migrationBuilder.CreateIndex(
                name: "IX_Order_WorkerIDWorker",
                table: "Order",
                column: "WorkerIDWorker");

            migrationBuilder.CreateIndex(
                name: "IX_SalaryWorker_WorkerIDWorker",
                table: "SalaryWorker",
                column: "WorkerIDWorker");

            migrationBuilder.CreateIndex(
                name: "IX_Shift_WorkerIDWorker",
                table: "Shift",
                column: "WorkerIDWorker");

            migrationBuilder.CreateIndex(
                name: "IX_Worker_UserIDUser",
                table: "Worker",
                column: "UserIDUser");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "SalaryWorker");

            migrationBuilder.DropTable(
                name: "Shift");

            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.DropTable(
                name: "Service");

            migrationBuilder.DropTable(
                name: "Worker");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
