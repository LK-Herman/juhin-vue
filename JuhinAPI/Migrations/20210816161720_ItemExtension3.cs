using Microsoft.EntityFrameworkCore.Migrations;

namespace JuhinAPI.Migrations
{
    public partial class ItemExtension3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxPalletQty",
                table: "Items");

            migrationBuilder.RenameColumn(
                name: "isICP",
                table: "Items",
                newName: "IsICP");

            migrationBuilder.AddColumn<int>(
                name: "MaxEuroPalQty",
                table: "Items",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxEuroPalQty",
                table: "Items");

            migrationBuilder.RenameColumn(
                name: "IsICP",
                table: "Items",
                newName: "isICP");

            migrationBuilder.AddColumn<int>(
                name: "MaxPalletQty",
                table: "Items",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
