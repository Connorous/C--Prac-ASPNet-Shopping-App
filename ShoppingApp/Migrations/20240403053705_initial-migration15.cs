using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingApp.Migrations
{
    /// <inheritdoc />
    public partial class initialmigration15 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StoreItem_Item_ItemsId",
                table: "StoreItem");

            migrationBuilder.RenameColumn(
                name: "ItemsId",
                table: "StoreItem",
                newName: "ItemId");

            migrationBuilder.RenameIndex(
                name: "IX_StoreItem_ItemsId",
                table: "StoreItem",
                newName: "IX_StoreItem_ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_StoreItem_Item_ItemId",
                table: "StoreItem",
                column: "ItemId",
                principalTable: "Item",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StoreItem_Item_ItemId",
                table: "StoreItem");

            migrationBuilder.RenameColumn(
                name: "ItemId",
                table: "StoreItem",
                newName: "ItemsId");

            migrationBuilder.RenameIndex(
                name: "IX_StoreItem_ItemId",
                table: "StoreItem",
                newName: "IX_StoreItem_ItemsId");

            migrationBuilder.AddForeignKey(
                name: "FK_StoreItem_Item_ItemsId",
                table: "StoreItem",
                column: "ItemsId",
                principalTable: "Item",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
