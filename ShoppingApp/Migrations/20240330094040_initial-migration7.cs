using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingApp.Migrations
{
    /// <inheritdoc />
    public partial class initialmigration7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Transaction_TransactionId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_TransactionId",
                table: "Order");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Transaction",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TransactionId1",
                table: "Order",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Order_TransactionId1",
                table: "Order",
                column: "TransactionId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Transaction_TransactionId1",
                table: "Order",
                column: "TransactionId1",
                principalTable: "Transaction",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Transaction_TransactionId1",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_TransactionId1",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "TransactionId1",
                table: "Order");

            migrationBuilder.CreateIndex(
                name: "IX_Order_TransactionId",
                table: "Order",
                column: "TransactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Transaction_TransactionId",
                table: "Order",
                column: "TransactionId",
                principalTable: "Transaction",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
