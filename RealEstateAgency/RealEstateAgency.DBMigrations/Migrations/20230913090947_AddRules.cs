using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstateAgency.DBMigrations.Migrations
{
    /// <inheritdoc />
    public partial class AddRules : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AcessToActions",
                columns: table => new
                {
                    UserIdUser = table.Column<int>(type: "int", nullable: false),
                    UserTypeAccount = table.Column<int>(type: "int", nullable: false),
                    GetInformationYourSelf = table.Column<bool>(type: "bit", nullable: false),
                    GetYourOrders = table.Column<bool>(type: "bit", nullable: false),
                    GetListAllOrders = table.Column<bool>(type: "bit", nullable: false),
                    GetListAllClients = table.Column<bool>(type: "bit", nullable: false),
                    GetListAllWorkers = table.Column<bool>(type: "bit", nullable: false),
                    GetListAllAdmins = table.Column<bool>(type: "bit", nullable: false),
                    SetScoreForOrders = table.Column<bool>(type: "bit", nullable: false),
                    ChangeYourUserName = table.Column<bool>(type: "bit", nullable: false),
                    ChangeYourPassword = table.Column<bool>(type: "bit", nullable: false),
                    ChangeYourEmail = table.Column<bool>(type: "bit", nullable: false),
                    ChangeYourFirstName = table.Column<bool>(type: "bit", nullable: false),
                    ChangeYourLastName = table.Column<bool>(type: "bit", nullable: false),
                    ChangeYourSecondName = table.Column<bool>(type: "bit", nullable: false),
                    ChangeAnotherUserId = table.Column<bool>(type: "bit", nullable: false),
                    ChangeAnotherUserName = table.Column<bool>(type: "bit", nullable: false),
                    ChangeAnotherPassword = table.Column<bool>(type: "bit", nullable: false),
                    ChangeAnotherEmail = table.Column<bool>(type: "bit", nullable: false),
                    ChangeAnotherFirstName = table.Column<bool>(type: "bit", nullable: false),
                    ChangeAnotherLastName = table.Column<bool>(type: "bit", nullable: false),
                    ChangeAnotherSecondName = table.Column<bool>(type: "bit", nullable: false),
                    ChangeAnotherStatus = table.Column<bool>(type: "bit", nullable: false),
                    AcceptOrdersClient = table.Column<bool>(type: "bit", nullable: false),
                    CreateOrder = table.Column<bool>(type: "bit", nullable: false),
                    IneractionWithShifts = table.Column<bool>(type: "bit", nullable: false),
                    CreateService = table.Column<bool>(type: "bit", nullable: false),
                    EditService = table.Column<bool>(type: "bit", nullable: false),
                    DeleteService = table.Column<bool>(type: "bit", nullable: false),
                    GiveRoleClient = table.Column<bool>(type: "bit", nullable: false),
                    GiveRoleWorker = table.Column<bool>(type: "bit", nullable: false),
                    GiveRoleAdmin = table.Column<bool>(type: "bit", nullable: false),
                    DeleteUser = table.Column<bool>(type: "bit", nullable: false),
                    DeleteWorker = table.Column<bool>(type: "bit", nullable: false),
                    DeleteClient = table.Column<bool>(type: "bit", nullable: false),
                    AcceptNewWorker = table.Column<bool>(type: "bit", nullable: false),
                    FireAnEmployeWorker = table.Column<bool>(type: "bit", nullable: false),
                    AcceptNewAdmin = table.Column<bool>(type: "bit", nullable: false),
                    FireAnEmployeAdmin = table.Column<bool>(type: "bit", nullable: false),
                    DeleteAdmin = table.Column<bool>(type: "bit", nullable: false),
                    MakeAnyActions = table.Column<bool>(type: "bit", nullable: false),
                    GetSalaryAnyWorkers = table.Column<bool>(type: "bit", nullable: false),
                    AddSalayWorkersForMonth = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_AcessToActions_User_UserIdUser",
                        column: x => x.UserIdUser,
                        principalTable: "User",
                        principalColumn: "IDUser",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Admin",
                columns: table => new
                {
                    AdminId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserIdUser = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admin", x => x.AdminId);
                    table.ForeignKey(
                        name: "FK_Admin_User_UserIdUser",
                        column: x => x.UserIdUser,
                        principalTable: "User",
                        principalColumn: "IDUser",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReportProblems",
                columns: table => new
                {
                    IDReport = table.Column<int>(type: "int", nullable: false),
                    UserIdUser = table.Column<int>(type: "int", nullable: false),
                    Problem = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsAccept = table.Column<bool>(type: "bit", nullable: false),
                    CommentAnAccess = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_ReportProblems_User_UserIdUser",
                        column: x => x.UserIdUser,
                        principalTable: "User",
                        principalColumn: "IDUser",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AcessToActions_UserIdUser",
                table: "AcessToActions",
                column: "UserIdUser");

            migrationBuilder.CreateIndex(
                name: "IX_Admin_UserIdUser",
                table: "Admin",
                column: "UserIdUser");

            migrationBuilder.CreateIndex(
                name: "IX_ReportProblems_UserIdUser",
                table: "ReportProblems",
                column: "UserIdUser");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AcessToActions");

            migrationBuilder.DropTable(
                name: "Admin");

            migrationBuilder.DropTable(
                name: "ReportProblems");
        }
    }
}
