using Microsoft.EntityFrameworkCore.Migrations;

namespace JuhinAPI.Migrations
{
    public partial class DBMajorDataInput : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                SET IDENTITY_INSERT Currency ON 
                GO
                INSERT Currency ([CurrencyId], [Name], [Code], [ValuePLN]) VALUES (1, N'Euro', N'EUR', 4590)
                GO
                INSERT Currency ([CurrencyId], [Name], [Code], [ValuePLN]) VALUES (2, N'Zloty', N'PLN', 1000)
                GO
                INSERT Currency ([CurrencyId], [Name], [Code], [ValuePLN]) VALUES (3, N'US Dolar', N'USD', 3950)
                GO
                INSERT Currency ([CurrencyId], [Name], [Code], [ValuePLN]) VALUES (4, N'JP Yen', N'JPY', 0035)
                GO
                SET IDENTITY_INSERT Currency OFF
                GO
                
                SET IDENTITY_INSERT Pallets ON 
                GO
                INSERT Pallets ([PalletId], [Name], [Xdimension], [Ydimension]) VALUES (1, N'EUR Pal', 1200, 800)
                GO
                INSERT Pallets ([PalletId], [Name], [Xdimension], [Ydimension]) VALUES (2, N'EUR2 Pal', 1200, 1000)
                GO
                INSERT Pallets ([PalletId], [Name], [Xdimension], [Ydimension]) VALUES (3, N'EUR6 Pal', 800, 600)
                GO
                INSERT Pallets ([PalletId], [Name], [Xdimension], [Ydimension]) VALUES (4, N'ASIA Pal', 1100, 1100)
                GO
                SET IDENTITY_INSERT Pallets OFF
                GO

                SET IDENTITY_INSERT Statuses ON 
                GO
                INSERT Statuses ([StatusId], [Name], [Description]) VALUES (1, N'Created', N'Shipment data was created')
                GO
                INSERT Statuses ([StatusId], [Name], [Description]) VALUES (2, N'In transit', N'Shipment is in transit')
                GO
                INSERT Statuses ([StatusId], [Name], [Description]) VALUES (3, N'Delivered', N'Shipment was delivered')
                GO          
                SET IDENTITY_INSERT Statuses OFF
                GO    

                SET IDENTITY_INSERT Units ON 
                GO
                INSERT Units ([UnitId], [Name], [ShortName]) VALUES (1, N'each', N'ea')
                GO
                INSERT Units ([UnitId], [Name], [ShortName]) VALUES (2, N'kilograms', N'kg')
                GO
                SET IDENTITY_INSERT Units OFF
                GO

                SET IDENTITY_INSERT Forwarders ON 
                GO
                INSERT Forwarders ([ForwarderId], [Name], [PhoneNumber], [Email], [Rating]) VALUES (1, N'Wings Spedition Inc.', N'505000505', N'wings@sped.com', 0)
                GO
                INSERT Forwarders ([ForwarderId], [Name], [PhoneNumber], [Email], [Rating]) VALUES (2, N'Best Parcel Service', N'606000606', N'bps@bps.com', 0)
                GO
                SET IDENTITY_INSERT Forwarders OFF
                GO
                
                SET IDENTITY_INSERT Warehouses ON 
                GO
                INSERT Warehouses ([WarehouseId], [Description], [ShortName], [MaxPalletsQty]) VALUES (1, N'Raw materials', N'RM', 350)
                GO
                INSERT Warehouses ([WarehouseId], [Description], [ShortName], [MaxPalletsQty]) VALUES (2, N'Explosives', N'EX', 150)
                GO
                SET IDENTITY_INSERT Warehouses OFF
                GO
            ");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                delete Currency where [CurrencyId] = 1
                delete Currency where [CurrencyId] = 2    
                delete Currency where [CurrencyId] = 3
                delete Currency where [CurrencyId] = 4

                delete Pallets where [PalletId] = 1
                delete Pallets where [PalletId] = 2
                delete Pallets where [PalletId] = 3
                delete Pallets where [PalletId] = 4

                delete Statuses where [StatusId] = 1
                delete Statuses where [StatusId] = 2
                delete Statuses where [StatusId] = 3

                delete Units where [UnitId] = 1
                delete Units where [UnitId] = 2
               
                delete Forwarders where [ForwarderId] = 1
                delete Forwarders where [ForwarderId] = 2
                
                delete Warehouses where [WarehouseId] = 1
                delete Warehouses where [WarehouseId] = 2
            ");

        }
    }
}
