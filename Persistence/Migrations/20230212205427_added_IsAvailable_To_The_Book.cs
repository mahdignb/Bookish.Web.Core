using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class added_IsAvailable_To_The_Book : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "132df912-ba91-418f-8fe8-a00e90fd0209");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "85a33f9e-eccf-44d7-a32c-388ea3ac7652");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "afa349d0-0931-412e-b40d-9ab28d213a28");

            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "Books",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "Books");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "132df912-ba91-418f-8fe8-a00e90fd0209", "ca74ac25-0bef-47fa-b4cb-c17040ce99aa", "Admin", "ADMIN" },
                    { "85a33f9e-eccf-44d7-a32c-388ea3ac7652", "d345b4d1-4d0a-44cd-b37d-439f598f1c71", "SuperAdmin", "SUPERADMIN" },
                    { "afa349d0-0931-412e-b40d-9ab28d213a28", "9c29b810-5268-4567-a4c5-dedfa2eb1284", "User", "USER" }
                });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: "1",
                column: "PublishTime",
                value: new DateTime(2023, 2, 12, 19, 16, 59, 635, DateTimeKind.Utc).AddTicks(4683));
        }
    }
}
