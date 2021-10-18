using Microsoft.EntityFrameworkCore.Migrations;

namespace JuhinAPI.Migrations
{
    public partial class PurchaseOrderNameUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_OrderNumber",
                table: "PurchaseOrders",
                column: "OrderNumber",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PurchaseOrders_OrderNumber",
                table: "PurchaseOrders");
        }
    }
}
