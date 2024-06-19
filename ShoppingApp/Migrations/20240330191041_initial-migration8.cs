using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingApp.Migrations
{
    /// <inheritdoc />
    public partial class initialmigration8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.DropColumn(
                name: "TransactionId",
                table: "Order");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_OrderId",
                table: "Transaction",
                column: "OrderId");

            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Order_OrderId",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_OrderId",
                table: "Transaction");

            migrationBuilder.AddColumn<int>(
                name: "TransactionId",
                table: "Order",
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
    }
}
