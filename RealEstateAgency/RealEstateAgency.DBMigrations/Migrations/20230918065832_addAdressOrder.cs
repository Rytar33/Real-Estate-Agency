using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstateAgency.DBMigrations.Migrations
{
    /// <inheritdoc />
    public partial class addAdressOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReportProblems");

            migrationBuilder.AddColumn<string>(
                name: "Adress",
                table: "Order",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ReportProblem",
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
                        name: "FK_ReportProblem_User_UserIdUser",
                        column: x => x.UserIdUser,
                        principalTable: "User",
                        principalColumn: "IDUser",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReportProblem_UserIdUser",
                table: "ReportProblem",
                column: "UserIdUser");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReportProblem");

            migrationBuilder.DropColumn(
                name: "Adress",
                table: "Order");

            migrationBuilder.CreateTable(
                name: "ReportProblems",
                columns: table => new
                {
                    UserIdUser = table.Column<int>(type: "int", nullable: false),
                    CommentAnAccess = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IDReport = table.Column<int>(type: "int", nullable: false),
                    IsAccept = table.Column<bool>(type: "bit", nullable: false),
                    Problem = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                name: "IX_ReportProblems_UserIdUser",
                table: "ReportProblems",
                column: "UserIdUser");
        }
    }
}
