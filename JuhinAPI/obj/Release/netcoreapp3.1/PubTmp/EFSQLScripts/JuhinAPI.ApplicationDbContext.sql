IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210828205126_InitialCreation')
BEGIN
    CREATE TABLE [AspNetRoles] (
        [Id] nvarchar(450) NOT NULL,
        [Name] nvarchar(256) NULL,
        [NormalizedName] nvarchar(256) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210828205126_InitialCreation')
BEGIN
    CREATE TABLE [AspNetUsers] (
        [Id] nvarchar(450) NOT NULL,
        [UserName] nvarchar(256) NULL,
        [NormalizedUserName] nvarchar(256) NULL,
        [Email] nvarchar(256) NULL,
        [NormalizedEmail] nvarchar(256) NULL,
        [EmailConfirmed] bit NOT NULL,
        [PasswordHash] nvarchar(max) NULL,
        [SecurityStamp] nvarchar(max) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [PhoneNumberConfirmed] bit NOT NULL,
        [TwoFactorEnabled] bit NOT NULL,
        [LockoutEnd] datetimeoffset NULL,
        [LockoutEnabled] bit NOT NULL,
        [AccessFailedCount] int NOT NULL,
        CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210828205126_InitialCreation')
BEGIN
    CREATE TABLE [Currency] (
        [CurrencyId] int NOT NULL IDENTITY,
        [Name] nvarchar(40) NOT NULL,
        [Code] nvarchar(3) NOT NULL,
        [ValuePLN] int NOT NULL,
        CONSTRAINT [PK_Currency] PRIMARY KEY ([CurrencyId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210828205126_InitialCreation')
BEGIN
    CREATE TABLE [Forwarders] (
        [ForwarderId] int NOT NULL IDENTITY,
        [Name] nvarchar(100) NOT NULL,
        [PhoneNumber] nvarchar(18) NULL,
        [Email] nvarchar(60) NULL,
        [Rating] float NOT NULL,
        CONSTRAINT [PK_Forwarders] PRIMARY KEY ([ForwarderId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210828205126_InitialCreation')
BEGIN
    CREATE TABLE [Pallets] (
        [PalletId] int NOT NULL IDENTITY,
        [Name] nvarchar(20) NOT NULL,
        [Xdimension] int NOT NULL,
        [Ydimension] int NOT NULL,
        CONSTRAINT [PK_Pallets] PRIMARY KEY ([PalletId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210828205126_InitialCreation')
BEGIN
    CREATE TABLE [Statuses] (
        [StatusId] int NOT NULL IDENTITY,
        [Name] nvarchar(20) NOT NULL,
        [Description] nvarchar(200) NOT NULL,
        CONSTRAINT [PK_Statuses] PRIMARY KEY ([StatusId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210828205126_InitialCreation')
BEGIN
    CREATE TABLE [Units] (
        [UnitId] int NOT NULL IDENTITY,
        [Name] nvarchar(20) NOT NULL,
        [ShortName] nvarchar(6) NULL,
        CONSTRAINT [PK_Units] PRIMARY KEY ([UnitId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210828205126_InitialCreation')
BEGIN
    CREATE TABLE [Vendors] (
        [VendorId] uniqueidentifier NOT NULL,
        [VendorCode] nvarchar(5) NOT NULL,
        [ShortName] nvarchar(15) NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        [Address] nvarchar(max) NOT NULL,
        [Country] nvarchar(max) NOT NULL,
        [Email] nvarchar(max) NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [IsActive] bit NOT NULL,
        CONSTRAINT [PK_Vendors] PRIMARY KEY ([VendorId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210828205126_InitialCreation')
BEGIN
    CREATE TABLE [Warehouses] (
        [WarehouseId] int NOT NULL IDENTITY,
        [Description] nvarchar(max) NOT NULL,
        [ShortName] nvarchar(max) NOT NULL,
        [MaxPalletsQty] int NOT NULL,
        CONSTRAINT [PK_Warehouses] PRIMARY KEY ([WarehouseId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210828205126_InitialCreation')
BEGIN
    CREATE TABLE [AspNetRoleClaims] (
        [Id] int NOT NULL IDENTITY,
        [RoleId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210828205126_InitialCreation')
BEGIN
    CREATE TABLE [AspNetUserClaims] (
        [Id] int NOT NULL IDENTITY,
        [UserId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210828205126_InitialCreation')
BEGIN
    CREATE TABLE [AspNetUserLogins] (
        [LoginProvider] nvarchar(450) NOT NULL,
        [ProviderKey] nvarchar(450) NOT NULL,
        [ProviderDisplayName] nvarchar(max) NULL,
        [UserId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
        CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210828205126_InitialCreation')
BEGIN
    CREATE TABLE [AspNetUserRoles] (
        [UserId] nvarchar(450) NOT NULL,
        [RoleId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
        CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210828205126_InitialCreation')
BEGIN
    CREATE TABLE [AspNetUserTokens] (
        [UserId] nvarchar(450) NOT NULL,
        [LoginProvider] nvarchar(450) NOT NULL,
        [Name] nvarchar(450) NOT NULL,
        [Value] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
        CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210828205126_InitialCreation')
BEGIN
    CREATE TABLE [Deliveries] (
        [DeliveryId] uniqueidentifier NOT NULL,
        [CreatedAt] datetime2 NOT NULL,
        [ETADate] datetime2 NOT NULL,
        [DeliveryDate] datetime2 NOT NULL,
        [Rating] int NOT NULL,
        [Comment] nvarchar(max) NULL,
        [ForwarderId] int NOT NULL,
        [StatusId] int NOT NULL,
        CONSTRAINT [PK_Deliveries] PRIMARY KEY ([DeliveryId]),
        CONSTRAINT [FK_Deliveries_Forwarders_ForwarderId] FOREIGN KEY ([ForwarderId]) REFERENCES [Forwarders] ([ForwarderId]) ON DELETE CASCADE,
        CONSTRAINT [FK_Deliveries_Statuses_StatusId] FOREIGN KEY ([StatusId]) REFERENCES [Statuses] ([StatusId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210828205126_InitialCreation')
BEGIN
    CREATE TABLE [PurchaseOrders] (
        [OrderId] uniqueidentifier NOT NULL,
        [OrderNumber] nvarchar(50) NOT NULL,
        [VendorId] uniqueidentifier NOT NULL,
        [UserId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_PurchaseOrders] PRIMARY KEY ([OrderId]),
        CONSTRAINT [FK_PurchaseOrders_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_PurchaseOrders_Vendors_VendorId] FOREIGN KEY ([VendorId]) REFERENCES [Vendors] ([VendorId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210828205126_InitialCreation')
BEGIN
    CREATE TABLE [Items] (
        [ItemId] uniqueidentifier NOT NULL,
        [Name] nvarchar(16) NOT NULL,
        [Description] nvarchar(50) NOT NULL,
        [RevisionNumber] nvarchar(5) NOT NULL,
        [Price] int NOT NULL,
        [MaxEuroPalQty] int NOT NULL,
        [IsICP] bit NOT NULL,
        [HSCode] nvarchar(max) NULL,
        [HSCodeDescription] nvarchar(max) NULL,
        [CountryOfOrigin] nvarchar(max) NULL,
        [PalletQty] int NOT NULL,
        [IsActive] bit NOT NULL,
        [WarehouseId] int NOT NULL,
        [VendorId] uniqueidentifier NOT NULL,
        [CurrencyId] int NOT NULL,
        [PalletId] int NOT NULL,
        [UnitId] int NOT NULL,
        CONSTRAINT [PK_Items] PRIMARY KEY ([ItemId]),
        CONSTRAINT [FK_Items_Currency_CurrencyId] FOREIGN KEY ([CurrencyId]) REFERENCES [Currency] ([CurrencyId]) ON DELETE CASCADE,
        CONSTRAINT [FK_Items_Pallets_PalletId] FOREIGN KEY ([PalletId]) REFERENCES [Pallets] ([PalletId]) ON DELETE CASCADE,
        CONSTRAINT [FK_Items_Units_UnitId] FOREIGN KEY ([UnitId]) REFERENCES [Units] ([UnitId]) ON DELETE CASCADE,
        CONSTRAINT [FK_Items_Vendors_VendorId] FOREIGN KEY ([VendorId]) REFERENCES [Vendors] ([VendorId]) ON DELETE CASCADE,
        CONSTRAINT [FK_Items_Warehouses_WarehouseId] FOREIGN KEY ([WarehouseId]) REFERENCES [Warehouses] ([WarehouseId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210828205126_InitialCreation')
BEGIN
    CREATE TABLE [WarehouseVolumes] (
        [VolumeId] uniqueidentifier NOT NULL,
        [Date] datetime2 NOT NULL,
        [PalletsVolume] int NOT NULL,
        [WarehouseId] int NOT NULL,
        CONSTRAINT [PK_WarehouseVolumes] PRIMARY KEY ([VolumeId]),
        CONSTRAINT [FK_WarehouseVolumes_Warehouses_WarehouseId] FOREIGN KEY ([WarehouseId]) REFERENCES [Warehouses] ([WarehouseId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210828205126_InitialCreation')
BEGIN
    CREATE TABLE [Documents] (
        [DocumentId] uniqueidentifier NOT NULL,
        [Type] nvarchar(20) NOT NULL,
        [Number] nvarchar(30) NOT NULL,
        [DocumentFile] nvarchar(max) NULL,
        [DeliveryId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_Documents] PRIMARY KEY ([DocumentId]),
        CONSTRAINT [FK_Documents_Deliveries_DeliveryId] FOREIGN KEY ([DeliveryId]) REFERENCES [Deliveries] ([DeliveryId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210828205126_InitialCreation')
BEGIN
    CREATE TABLE [Subscriptions] (
        [SubscriptionId] uniqueidentifier NOT NULL,
        [DeliveryId] uniqueidentifier NOT NULL,
        [UserId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_Subscriptions] PRIMARY KEY ([SubscriptionId]),
        CONSTRAINT [FK_Subscriptions_Deliveries_DeliveryId] FOREIGN KEY ([DeliveryId]) REFERENCES [Deliveries] ([DeliveryId]) ON DELETE CASCADE,
        CONSTRAINT [FK_Subscriptions_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210828205126_InitialCreation')
BEGIN
    CREATE TABLE [PurchaseOrders_Deliveries] (
        [PurchaseOrderId] uniqueidentifier NOT NULL,
        [DeliveryId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_PurchaseOrders_Deliveries] PRIMARY KEY ([DeliveryId], [PurchaseOrderId]),
        CONSTRAINT [FK_PurchaseOrders_Deliveries_Deliveries_DeliveryId] FOREIGN KEY ([DeliveryId]) REFERENCES [Deliveries] ([DeliveryId]) ON DELETE CASCADE,
        CONSTRAINT [FK_PurchaseOrders_Deliveries_PurchaseOrders_PurchaseOrderId] FOREIGN KEY ([PurchaseOrderId]) REFERENCES [PurchaseOrders] ([OrderId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210828205126_InitialCreation')
BEGIN
    CREATE TABLE [PackedItems] (
        [PackedItemId] uniqueidentifier NOT NULL,
        [Quantity] int NOT NULL,
        [DeliveryId] uniqueidentifier NOT NULL,
        [ItemId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_PackedItems] PRIMARY KEY ([PackedItemId]),
        CONSTRAINT [FK_PackedItems_Deliveries_DeliveryId] FOREIGN KEY ([DeliveryId]) REFERENCES [Deliveries] ([DeliveryId]) ON DELETE CASCADE,
        CONSTRAINT [FK_PackedItems_Items_ItemId] FOREIGN KEY ([ItemId]) REFERENCES [Items] ([ItemId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210828205126_InitialCreation')
BEGIN
    CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210828205126_InitialCreation')
BEGIN
    CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210828205126_InitialCreation')
BEGIN
    CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210828205126_InitialCreation')
BEGIN
    CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210828205126_InitialCreation')
BEGIN
    CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210828205126_InitialCreation')
BEGIN
    CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210828205126_InitialCreation')
BEGIN
    CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210828205126_InitialCreation')
BEGIN
    CREATE INDEX [IX_Deliveries_ForwarderId] ON [Deliveries] ([ForwarderId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210828205126_InitialCreation')
BEGIN
    CREATE INDEX [IX_Deliveries_StatusId] ON [Deliveries] ([StatusId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210828205126_InitialCreation')
BEGIN
    CREATE INDEX [IX_Documents_DeliveryId] ON [Documents] ([DeliveryId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210828205126_InitialCreation')
BEGIN
    CREATE INDEX [IX_Items_CurrencyId] ON [Items] ([CurrencyId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210828205126_InitialCreation')
BEGIN
    CREATE INDEX [IX_Items_PalletId] ON [Items] ([PalletId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210828205126_InitialCreation')
BEGIN
    CREATE INDEX [IX_Items_UnitId] ON [Items] ([UnitId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210828205126_InitialCreation')
BEGIN
    CREATE INDEX [IX_Items_VendorId] ON [Items] ([VendorId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210828205126_InitialCreation')
BEGIN
    CREATE INDEX [IX_Items_WarehouseId] ON [Items] ([WarehouseId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210828205126_InitialCreation')
BEGIN
    CREATE INDEX [IX_PackedItems_DeliveryId] ON [PackedItems] ([DeliveryId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210828205126_InitialCreation')
BEGIN
    CREATE UNIQUE INDEX [IX_PackedItems_ItemId] ON [PackedItems] ([ItemId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210828205126_InitialCreation')
BEGIN
    CREATE INDEX [IX_PurchaseOrders_UserId] ON [PurchaseOrders] ([UserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210828205126_InitialCreation')
BEGIN
    CREATE INDEX [IX_PurchaseOrders_VendorId] ON [PurchaseOrders] ([VendorId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210828205126_InitialCreation')
BEGIN
    CREATE INDEX [IX_PurchaseOrders_Deliveries_PurchaseOrderId] ON [PurchaseOrders_Deliveries] ([PurchaseOrderId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210828205126_InitialCreation')
BEGIN
    CREATE INDEX [IX_Subscriptions_DeliveryId] ON [Subscriptions] ([DeliveryId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210828205126_InitialCreation')
BEGIN
    CREATE INDEX [IX_Subscriptions_UserId] ON [Subscriptions] ([UserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210828205126_InitialCreation')
BEGIN
    CREATE INDEX [IX_WarehouseVolumes_WarehouseId] ON [WarehouseVolumes] ([WarehouseId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210828205126_InitialCreation')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210828205126_InitialCreation', N'3.1.9');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210828220230_AddRoles')
BEGIN

                    Insert into AspNetRoles (Id, [name], [NormalizedName])
                    values ('4a27ca19-5b8d-4fe7-863e-f11da00179bd', 'Admin', 'ADMIN')
                    
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210828220230_AddRoles')
BEGIN

                    Insert into AspNetRoles (Id, [name], [NormalizedName])
                    values ('d90f458f-92b1-4eeb-b566-3432a4d40c6d', 'Specialist', 'SPECIALIST')
                    
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210828220230_AddRoles')
BEGIN

                    Insert into AspNetRoles (Id, [name], [NormalizedName])
                    values ('eb6693e8-70b5-4b71-bae1-e6b9819f201f', 'Warehouseman', 'WAREHOUSEMAN')
                    
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210828220230_AddRoles')
BEGIN

                    Insert into AspNetRoles (Id, [name], [NormalizedName])
                    values ('812d23b5-a53b-4156-b643-12c2bdfbe62d', 'Guest', 'GUEST')
                    
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210828220230_AddRoles')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210828220230_AddRoles', N'3.1.9');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211007154451_AdminUser')
BEGIN

                    INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'433a7b51-85f1-49f6-bca5-96a27b96e464', N'lkuczma@gmail.com', N'LKUCZMA@GMAIL.COM', N'lkuczma@gmail.com', N'LKUCZMA@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAEOJ/IHqqqb9C0b1dytJPbNK75v6eG9Tm4H/62FzsZ8lvp7AzmqnDDVksGQQOfAhKgQ==', N'XYB5A273YH74W2TUSXBVW7MZPDOZGIDO', N'6b2a8724-13ed-4a26-b329-2c3c33d048d0', NULL, 0, 0, NULL, 1, 0)
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211007154451_AdminUser')
BEGIN
    SET IDENTITY_INSERT [dbo].[AspNetUserClaims] ON 
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211007154451_AdminUser')
BEGIN
    INSERT [dbo].[AspNetUserClaims] ([Id], [UserId], [ClaimType], [ClaimValue]) VALUES (1, N'433a7b51-85f1-49f6-bca5-96a27b96e464', N'http://schemas.microsoft.com/ws/2008/06/identity/claims/role', N'Admin')
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211007154451_AdminUser')
BEGIN
    SET IDENTITY_INSERT [dbo].[AspNetUserClaims] OFF
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211007154451_AdminUser')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20211007154451_AdminUser', N'3.1.9');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211009143957_SetVendors')
BEGIN

                INSERT [dbo].[Vendors] ([VendorId], [VendorCode], [ShortName], [Name], [Address], [Country], [Email], [PhoneNumber], [IsActive]) VALUES (N'445f2b47-d04f-4956-e8ed-08d989b7e659', N'89004', N'PHONDA', N'Phonda Industry Poland', N'Kazimierza Wielkiego 56, 59-300 Lubin', N'Poland', N'phonda@phonda.pl', N'999888555', 1)
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211009143957_SetVendors')
BEGIN
    INSERT [dbo].[Vendors] ([VendorId], [VendorCode], [ShortName], [Name], [Address], [Country], [Email], [PhoneNumber], [IsActive]) VALUES (N'32dddc26-d0d8-43ed-e8ee-08d989b7e659', N'40404', N'BURSUS', N'Bursus Industry Poland', N'Żeromskiego 12, 62-400 Poznań', N'Poland', N'bursus@bursus.pl', N'111888111', 1)
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211009143957_SetVendors')
BEGIN
    INSERT [dbo].[Vendors] ([VendorId], [VendorCode], [ShortName], [Name], [Address], [Country], [Email], [PhoneNumber], [IsActive]) VALUES (N'f8dddc24-d0d8-43ed-e8ee-08d989b7e65a', N'60604', N'VMI', N'Vegas Metal Inc.', N'Elizy Orzeszkowej 23, 02-600 Warszawa', N'Poland', N'vmi@vmi.pl', N'689147887', 1)
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211009143957_SetVendors')
BEGIN
    INSERT [dbo].[Vendors] ([VendorId], [VendorCode], [ShortName], [Name], [Address], [Country], [Email], [PhoneNumber], [IsActive]) VALUES (N'1e5f2b47-d041-4956-e8e1-08d981b7e652', N'19010', N'SKY', N'Sky High Boards Inc.', N'1717 N Harwood St, Dallas, TX 75201', N'USA', N'skya@boards.com', N'119888551', 1)
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211009143957_SetVendors')
BEGIN
    INSERT [dbo].[Vendors] ([VendorId], [VendorCode], [ShortName], [Name], [Address], [Country], [Email], [PhoneNumber], [IsActive]) VALUES (N'6eaf2b47-a048-6956-28e2-01d981b7e688', N'44010', N'BFOREST', N'Blue Forest Sp. z o.o.', N'Pomorska 123, 72-300 Szczecin', N'Polska', N'pomorska@bforest.eu', N'606828523', 1)
                
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211009143957_SetVendors')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20211009143957_SetVendors', N'3.1.9');
END;

GO

