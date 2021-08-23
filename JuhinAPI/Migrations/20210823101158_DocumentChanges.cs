using Microsoft.EntityFrameworkCore.Migrations;

namespace JuhinAPI.Migrations
{
    public partial class DocumentChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                table: "Documents");

            migrationBuilder.AddColumn<string>(
                name: "DocumentFile",
                table: "Documents",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DocumentFile",
                table: "Documents");

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Documents",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
