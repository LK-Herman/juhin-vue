using Microsoft.EntityFrameworkCore.Migrations;

namespace JuhinAPI.Migrations
{
    public partial class AdminUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'333a7b51-85f1-49f6-bca5-96a27b96e465', N'lkuczma@gmail.com', N'LKUCZMA@GMAIL.COM', N'lkuczma@gmail.com', N'LKUCZMA@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAEOJ/IHqqqb9C0b1dytJPbNK75v6eG9Tm4H/62FzsZ8lvp7AzmqnDDVksGQQOfAhKgQ==', N'XYB5A273YH74W2TUSXBVW7MZPDOZGIDO', N'6b2a8724-13ed-4a26-b329-2c3c33d048d0', NULL, 0, 0, NULL, 1, 0)
                GO
                SET IDENTITY_INSERT [dbo].[AspNetUserClaims] ON 
                GO
                INSERT [dbo].[AspNetUserClaims] ([Id], [UserId], [ClaimType], [ClaimValue]) VALUES (1, N'333a7b51-85f1-49f6-bca5-96a27b96e465', N'http://schemas.microsoft.com/ws/2008/06/identity/claims/role', N'Admin')
                GO
                SET IDENTITY_INSERT [dbo].[AspNetUserClaims] OFF
                GO
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"

                        delete [dbo].[AspNetUsers] where [Id] = '333a7b51-85f1-49f6-bca5-96a27b96e465'
                        delete [dbo].[AspNetUserClaims] where [Id] = 1

            ");

        }
    }
}
