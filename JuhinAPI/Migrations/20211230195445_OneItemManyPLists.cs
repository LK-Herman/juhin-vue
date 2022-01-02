using Microsoft.EntityFrameworkCore.Migrations;

namespace JuhinAPI.Migrations
{
    public partial class OneItemManyPLists : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PackedItems_ItemId",
                table: "PackedItems");

            migrationBuilder.CreateIndex(
                name: "IX_PackedItems_ItemId",
                table: "PackedItems",
                column: "ItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PackedItems_ItemId",
                table: "PackedItems");

            migrationBuilder.CreateIndex(
                name: "IX_PackedItems_ItemId",
                table: "PackedItems",
                column: "ItemId",
                unique: true);
        }
    }
}
