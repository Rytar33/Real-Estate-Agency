using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class FixTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Client_IDClient1",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Service_IDService1",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Worker_IDWorker1",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Worker_IDUser",
                table: "Worker");

            migrationBuilder.DropIndex(
                name: "IX_Client_IDUser",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "IDUser",
                table: "Worker");

            migrationBuilder.DropColumn(
                name: "IDWorker",
                table: "SalaryWorker");

            migrationBuilder.DropColumn(
                name: "IDUser",
                table: "Client");

            migrationBuilder.RenameColumn(
                name: "IDWorker1",
                table: "Order",
                newName: "WorkerIDWorker");

            migrationBuilder.RenameColumn(
                name: "IDService1",
                table: "Order",
                newName: "ServiceIDService");

            migrationBuilder.RenameColumn(
                name: "IDClient1",
                table: "Order",
                newName: "ClientIDClient");

            migrationBuilder.RenameIndex(
                name: "IX_Order_IDWorker1",
                table: "Order",
                newName: "IX_Order_WorkerIDWorker");

            migrationBuilder.RenameIndex(
                name: "IX_Order_IDService1",
                table: "Order",
                newName: "IX_Order_ServiceIDService");

            migrationBuilder.RenameIndex(
                name: "IX_Order_IDClient1",
                table: "Order",
                newName: "IX_Order_ClientIDClient");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(25)",
                oldMaxLength: 25);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Client_ClientIDClient",
                table: "Order",
                column: "ClientIDClient",
                principalTable: "Client",
                principalColumn: "IDClient",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Service_ServiceIDService",
                table: "Order",
                column: "ServiceIDService",
                principalTable: "Service",
                principalColumn: "IDService",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Worker_WorkerIDWorker",
                table: "Order",
                column: "WorkerIDWorker",
                principalTable: "Worker",
                principalColumn: "IDWorker");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Client_ClientIDClient",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Service_ServiceIDService",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Worker_WorkerIDWorker",
                table: "Order");

            migrationBuilder.RenameColumn(
                name: "WorkerIDWorker",
                table: "Order",
                newName: "IDWorker1");

            migrationBuilder.RenameColumn(
                name: "ServiceIDService",
                table: "Order",
                newName: "IDService1");

            migrationBuilder.RenameColumn(
                name: "ClientIDClient",
                table: "Order",
                newName: "IDClient1");

            migrationBuilder.RenameIndex(
                name: "IX_Order_WorkerIDWorker",
                table: "Order",
                newName: "IX_Order_IDWorker1");

            migrationBuilder.RenameIndex(
                name: "IX_Order_ServiceIDService",
                table: "Order",
                newName: "IX_Order_IDService1");

            migrationBuilder.RenameIndex(
                name: "IX_Order_ClientIDClient",
                table: "Order",
                newName: "IX_Order_IDClient1");

            migrationBuilder.AddColumn<int>(
                name: "IDUser",
                table: "Worker",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "User",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "IDWorker",
                table: "SalaryWorker",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IDUser",
                table: "Client",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Worker_IDUser",
                table: "Worker",
                column: "IDUser",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Client_IDUser",
                table: "Client",
                column: "IDUser",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Client_IDClient1",
                table: "Order",
                column: "IDClient1",
                principalTable: "Client",
                principalColumn: "IDClient",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Service_IDService1",
                table: "Order",
                column: "IDService1",
                principalTable: "Service",
                principalColumn: "IDService",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Worker_IDWorker1",
                table: "Order",
                column: "IDWorker1",
                principalTable: "Worker",
                principalColumn: "IDWorker");
        }
    }
}
