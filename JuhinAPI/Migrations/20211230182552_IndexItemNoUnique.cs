using Microsoft.EntityFrameworkCore.Migrations;

namespace JuhinAPI.Migrations
{
    public partial class IndexItemNoUnique : Migration
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

        }
    }
}
