using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ShoppingApp.Migrations
{
    /// <inheritdoc />
    public partial class initialmigration6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Order_OrderId",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_OrderId",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Transaction");

            migrationBuilder.AddColumn<int>(
                name: "OrderItemId",
                table: "Order",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PayTypeId",
                table: "Order",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TransactionId",
                table: "Order",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "ItemName",
                table: "Item",
                type: "character varying(60)",
                maxLength: 60,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(40)",
                oldMaxLength: 40);

            migrationBuilder.CreateTable(
                name: "PayType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PayTypeName = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Order_OrderItemId",
                table: "Order",
                column: "OrderItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_PayTypeId",
                table: "Order",
                column: "PayTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_TransactionId",
                table: "Order",
                column: "TransactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_OrderItem_OrderItemId",
                table: "Order",
                column: "OrderItemId",
                principalTable: "OrderItem",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_PayType_PayTypeId",
                table: "Order",
                column: "PayTypeId",
                principalTable: "PayType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Transaction_TransactionId",
                table: "Order",
                column: "TransactionId",
                principalTable: "Transaction",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_OrderItem_OrderItemId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_PayType_PayTypeId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Transaction_TransactionId",
                table: "Order");

            migrationBuilder.DropTable(
                name: "PayType");

            migrationBuilder.DropIndex(
                name: "IX_Order_OrderItemId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_PayTypeId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_TransactionId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "OrderItemId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "PayTypeId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "TransactionId",
                table: "Order");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Transaction",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "ItemName",
                table: "Item",
                type: "character varying(40)",
                maxLength: 40,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(60)",
                oldMaxLength: 60);

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_OrderId",
                table: "Transaction",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Order_OrderId",
                table: "Transaction",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
