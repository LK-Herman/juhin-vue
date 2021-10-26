using Microsoft.EntityFrameworkCore.Migrations;

namespace JuhinAPI.Migrations
{
    public partial class AddRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                Insert into AspNetRoles (Id, [name], [NormalizedName])
                values ('4a27ca19-5b8d-4fe7-863e-f11da00179bd', 'Admin', 'ADMIN')
                ");
            migrationBuilder.Sql(@"
                Insert into AspNetRoles (Id, [name], [NormalizedName])
                values ('d90f458f-92b1-4eeb-b566-3432a4d40c6d', 'Specialist', 'SPECIALIST')
                ");
            migrationBuilder.Sql(@"
                Insert into AspNetRoles (Id, [name], [NormalizedName])
                values ('eb6693e8-70b5-4b71-bae1-e6b9819f201f', 'Warehouseman', 'WAREHOUSEMAN')
                ");
            migrationBuilder.Sql(@"
                Insert into AspNetRoles (Id, [name], [NormalizedName])
                values ('812d23b5-a53b-4156-b643-12c2bdfbe62d', 'Guest', 'GUEST')
                ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"delete AspNetRoles where id = '4a27ca19-5b8d-4fe7-863e-f11da00179bd'");
            migrationBuilder.Sql(@"delete AspNetRoles where id = 'd90f458f-92b1-4eeb-b566-3432a4d40c6d'");
            migrationBuilder.Sql(@"delete AspNetRoles where id = 'eb6693e8-70b5-4b71-bae1-e6b9819f201f'");
            migrationBuilder.Sql(@"delete AspNetRoles where id = '812d23b5-a53b-4156-b643-12c2bdfbe62d'");
        }
    }
}
