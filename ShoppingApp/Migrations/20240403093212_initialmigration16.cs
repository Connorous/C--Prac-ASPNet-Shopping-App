using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingApp.Migrations
{
    /// <inheritdoc />
    public partial class initialmigration16 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Store_StoreId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_StoreId",
                table: "Order");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Order_StoreId",
                table: "Order",
                column: "StoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Store_StoreId",
                table: "Order",
                column: "StoreId",
                principalTable: "Store",
                principalColumn: "Id");
        }
    }
}
