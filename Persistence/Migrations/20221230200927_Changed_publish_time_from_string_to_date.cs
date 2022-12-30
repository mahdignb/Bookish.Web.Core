using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class Changed_publish_time_from_string_to_date : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "02cf771e-f4a7-41d4-93c0-225aa8074ac3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5364b854-a361-412d-acd9-dfb17f6d9533");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f6308260-0f1d-4715-adc3-7ec9968714d5");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PublishTime",
                table: "Books",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "40805ff7-792b-4fc8-af8b-72c5748a1404", "b3a389b3-7b56-4c7e-868f-7bc64e64bcb6", "User", "USER" },
                    { "a2e6425b-344a-4e7d-8131-7138f4b07c21", "9ff4beb2-2dce-48df-a9f8-0055a4b4bc1b", "Admin", "ADMIN" },
                    { "c9e074a7-5e62-4f38-8d1d-fcceda41a312", "34cd657e-2b29-49d5-9e06-fa2547406f0e", "SuperAdmin", "SUPERADMIN" }
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "AuthorId", "FirstName", "LastName", "MiddleName" },
                values: new object[] { "1", "Robert", "Martin", "C" });

            migrationBuilder.InsertData(
                table: "BookAuthors",
                columns: new[] { "AuthorId", "BookId" },
                values: new object[] { "1", "1" });

            migrationBuilder.InsertData(
                table: "Publisher",
                columns: new[] { "PublisherId", "Address", "City", "Country", "Name" },
                values: new object[] { "1", "Shelter Island", "New York", "United States", "Manning" });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "BookId", "Description", "Edition", "ISBN", "Language", "PublishTime", "PublisherId", "Title" },
                values: new object[] { "1", "Uncle bobs clean code", "1", "1234", "English", new DateTime(2022, 12, 30, 20, 9, 26, 944, DateTimeKind.Utc).AddTicks(8126), "1", "Clean code" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "AuthorId",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "BookAuthors",
                keyColumns: new[] { "AuthorId", "BookId" },
                keyValues: new object[] { "1", "1" });

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "Publisher",
                keyColumn: "PublisherId",
                keyValue: "1");

            migrationBuilder.AlterColumn<string>(
                name: "PublishTime",
                table: "Books",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "02cf771e-f4a7-41d4-93c0-225aa8074ac3", "9f8d50b9-940b-4e66-81e7-78fb32e800b3", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5364b854-a361-412d-acd9-dfb17f6d9533", "e23054af-2dac-4e54-bb9c-226c56e74c12", "SuperAdmin", "SUPERADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f6308260-0f1d-4715-adc3-7ec9968714d5", "58916107-731c-4362-abc3-5ac06573213c", "User", "USER" });
        }
    }
}
