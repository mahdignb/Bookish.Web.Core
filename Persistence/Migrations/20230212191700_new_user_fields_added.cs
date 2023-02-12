using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class new_user_fields_added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "40805ff7-792b-4fc8-af8b-72c5748a1404");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a2e6425b-344a-4e7d-8131-7138f4b07c21");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c9e074a7-5e62-4f38-8d1d-fcceda41a312");

            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                table: "AspNetUsers",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "BorrowBooks",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserId = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BookId = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BorrowBooks", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BorrowBooks");

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

            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "40805ff7-792b-4fc8-af8b-72c5748a1404", "b3a389b3-7b56-4c7e-868f-7bc64e64bcb6", "User", "USER" },
                    { "a2e6425b-344a-4e7d-8131-7138f4b07c21", "9ff4beb2-2dce-48df-a9f8-0055a4b4bc1b", "Admin", "ADMIN" },
                    { "c9e074a7-5e62-4f38-8d1d-fcceda41a312", "34cd657e-2b29-49d5-9e06-fa2547406f0e", "SuperAdmin", "SUPERADMIN" }
                });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: "1",
                column: "PublishTime",
                value: new DateTime(2022, 12, 30, 20, 9, 26, 944, DateTimeKind.Utc).AddTicks(8126));
        }
    }
}
