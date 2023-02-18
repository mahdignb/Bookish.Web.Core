using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class Added_Is_Returned_To_Borrow_Book : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1b497b94-aff9-417b-b88b-25d29a434943");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5e2702d4-99a9-4deb-87f3-c1cdc6d82625");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ffb4eb2f-ef1c-442b-98f7-3f8c53dce266");

            migrationBuilder.AddColumn<bool>(
                name: "IsReturned",
                table: "BorrowBooks",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1a119d4e-be54-4fa8-8373-c0fcbd47cc4a", "69f23e37-b1b1-4cb2-934d-fa780cadac67", "User", "USER" },
                    { "4a003364-4992-4ebe-81ae-758edcf2aad8", "1bfc3a3f-0386-4371-ba81-67e593660ee1", "Admin", "ADMIN" },
                    { "eca97699-f214-42e8-82d2-aeea49a49894", "bc620310-07a5-4daf-b7e5-8c43de6d0aa8", "SuperAdmin", "SUPERADMIN" }
                });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: "1",
                column: "PublishTime",
                value: new DateTime(2023, 2, 18, 14, 21, 44, 412, DateTimeKind.Utc).AddTicks(6402));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1a119d4e-be54-4fa8-8373-c0fcbd47cc4a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4a003364-4992-4ebe-81ae-758edcf2aad8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eca97699-f214-42e8-82d2-aeea49a49894");

            migrationBuilder.DropColumn(
                name: "IsReturned",
                table: "BorrowBooks");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1b497b94-aff9-417b-b88b-25d29a434943", "17ffdd03-33bd-4b48-ad21-ef9470151588", "SuperAdmin", "SUPERADMIN" },
                    { "5e2702d4-99a9-4deb-87f3-c1cdc6d82625", "adb950e4-c937-4aca-a1da-51765ff7b4cb", "Admin", "ADMIN" },
                    { "ffb4eb2f-ef1c-442b-98f7-3f8c53dce266", "617015cd-b8ba-4880-82a7-7c9e5d732bc2", "User", "USER" }
                });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: "1",
                column: "PublishTime",
                value: new DateTime(2023, 2, 12, 20, 54, 26, 945, DateTimeKind.Utc).AddTicks(8323));
        }
    }
}
