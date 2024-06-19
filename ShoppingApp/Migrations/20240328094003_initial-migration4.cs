using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ShoppingApp.Migrations
{
    /// <inheritdoc />
    public partial class initialmigration4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Brand",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "Developer",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "Platform",
                table: "Item");

            migrationBuilder.AddColumn<int>(
                name: "BrandId",
                table: "Item",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeveloperId",
                table: "Item",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PlatformId",
                table: "Item",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Brand",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BrandName = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brand", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Developer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DeveloperName = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Developer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Platform",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PlatformName = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Platform", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Item_BrandId",
                table: "Item",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Item_DeveloperId",
                table: "Item",
                column: "DeveloperId");

            migrationBuilder.CreateIndex(
                name: "IX_Item_PlatformId",
                table: "Item",
                column: "PlatformId");

            migrationBuilder.AddForeignKey(
                name: "FK_Item_Brand_BrandId",
                table: "Item",
                column: "BrandId",
                principalTable: "Brand",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Item_Developer_DeveloperId",
                table: "Item",
                column: "DeveloperId",
                principalTable: "Developer",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Item_Platform_PlatformId",
                table: "Item",
                column: "PlatformId",
                principalTable: "Platform",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_Brand_BrandId",
                table: "Item");

            migrationBuilder.DropForeignKey(
                name: "FK_Item_Developer_DeveloperId",
                table: "Item");

            migrationBuilder.DropForeignKey(
                name: "FK_Item_Platform_PlatformId",
                table: "Item");

            migrationBuilder.DropTable(
                name: "Brand");

            migrationBuilder.DropTable(
                name: "Developer");

            migrationBuilder.DropTable(
                name: "Platform");

            migrationBuilder.DropIndex(
                name: "IX_Item_BrandId",
                table: "Item");

            migrationBuilder.DropIndex(
                name: "IX_Item_DeveloperId",
                table: "Item");

            migrationBuilder.DropIndex(
                name: "IX_Item_PlatformId",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "BrandId",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "DeveloperId",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "PlatformId",
                table: "Item");

            migrationBuilder.AddColumn<string>(
                name: "Brand",
                table: "Item",
                type: "character varying(40)",
                maxLength: 40,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Developer",
                table: "Item",
                type: "character varying(40)",
                maxLength: 40,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Platform",
                table: "Item",
                type: "character varying(40)",
                maxLength: 40,
                nullable: true);
        }
    }
}
