using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingApp.Migrations
{
    /// <inheritdoc />
    public partial class initialmigration9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            

            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderItemId",
                table: "Order",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Order_OrderItemId",
                table: "Order",
                column: "OrderItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_OrderItem_OrderItemId",
                table: "Order",
                column: "OrderItemId",
                principalTable: "OrderItem",
                principalColumn: "Id");
        }
    }
}
