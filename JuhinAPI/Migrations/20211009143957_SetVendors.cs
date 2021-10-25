using Microsoft.EntityFrameworkCore.Migrations;

namespace JuhinAPI.Migrations
{
    public partial class SetVendors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            INSERT Vendors ([VendorId], [VendorCode], [ShortName], [Name], [Address], [Country], [Email], [PhoneNumber], [IsActive]) VALUES (N'445f2b47-d04f-4956-e8ed-08d989b7e659', N'89004', N'PHONDA', N'Phonda Industry Poland', N'Kazimierza Wielkiego 56, 59-300 Lubin', N'Poland', N'phonda@phonda.pl', N'999888555', 1)
            GO
            INSERT Vendors ([VendorId], [VendorCode], [ShortName], [Name], [Address], [Country], [Email], [PhoneNumber], [IsActive]) VALUES (N'32dddc26-d0d8-43ed-e8ee-08d989b7e659', N'40404', N'BURSUS', N'Bursus Industry Poland', N'Żeromskiego 12, 62-400 Poznań', N'Poland', N'bursus@bursus.pl', N'111888111', 1)
            GO
            INSERT Vendors ([VendorId], [VendorCode], [ShortName], [Name], [Address], [Country], [Email], [PhoneNumber], [IsActive]) VALUES (N'f8dddc24-d0d8-43ed-e8ee-08d989b7e65a', N'60604', N'VMI', N'Vegas Metal Inc.', N'Elizy Orzeszkowej 23, 02-600 Warszawa', N'Poland', N'vmi@vmi.pl', N'689147887', 1)
            GO
            INSERT Vendors ([VendorId], [VendorCode], [ShortName], [Name], [Address], [Country], [Email], [PhoneNumber], [IsActive]) VALUES (N'1e5f2b47-d041-4956-e8e1-08d981b7e652', N'19010', N'SKY', N'Sky High Boards Inc.', N'1717 N Harwood St, Dallas, TX 75201', N'USA', N'skya@boards.com', N'119888551', 1)
            GO
            INSERT Vendors ([VendorId], [VendorCode], [ShortName], [Name], [Address], [Country], [Email], [PhoneNumber], [IsActive]) VALUES (N'6eaf2b47-a048-6956-28e2-01d981b7e688', N'44010', N'BFOREST', N'Blue Forest Sp. z o.o.', N'Pomorska 123, 72-300 Szczecin', N'Polska', N'pomorska@bforest.eu', N'606828523', 1)
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                delete Vendors where [Id] = '445f2b47-d04f-4956-e8ed-08d989b7e659'
                delete Vendors where [Id] = '62dddc26-d0d8-43ed-e8ee-08d989b7e659'
                delete Vendors where [Id] = 'f8dddc24-d0d8-43ed-e8ee-08d989b7e65a'
                delete Vendors where [Id] = '6eaf2b47-a048-6956-28e2-01d981b7e688'
                delete Vendors where [Id] = '1e5f2b47-d041-4956-e8e1-08d981b7e652'
                
            ");
        }
    }
}
