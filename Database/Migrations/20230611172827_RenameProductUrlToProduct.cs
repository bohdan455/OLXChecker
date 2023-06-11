using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Migrations
{
    public partial class RenameProductUrlToProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Emails_ProductUrls_ProductUrlId",
                table: "Emails");

            migrationBuilder.DropTable(
                name: "ProductUrls");

            migrationBuilder.RenameColumn(
                name: "ProductUrlId",
                table: "Emails",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Emails_ProductUrlId",
                table: "Emails",
                newName: "IX_Emails_ProductId");

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    LastPrice = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Emails_Products_ProductId",
                table: "Emails",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Emails_Products_ProductId",
                table: "Emails");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Emails",
                newName: "ProductUrlId");

            migrationBuilder.RenameIndex(
                name: "IX_Emails_ProductId",
                table: "Emails",
                newName: "IX_Emails_ProductUrlId");

            migrationBuilder.CreateTable(
                name: "ProductUrls",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastPrice = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Url = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductUrls", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Emails_ProductUrls_ProductUrlId",
                table: "Emails",
                column: "ProductUrlId",
                principalTable: "ProductUrls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
