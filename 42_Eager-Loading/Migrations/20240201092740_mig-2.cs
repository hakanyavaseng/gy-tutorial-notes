using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _42_43_44_Loading_Related_Data.Migrations
{
    public partial class mig2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Ankara" });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Yozgat" });

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "Id", "Discriminator", "Name", "RegionId", "Salary", "Surname" },
                values: new object[,]
                {
                    { 1, "Employee", "Gençay", 1, 1500, "Yıldız" },
                    { 2, "Employee", "Mahmut", 2, 1500, "Yıldız" },
                    { 3, "Employee", "Rıfkı", 1, 1500, "Yıldız" },
                    { 4, "Employee", "Cüneyt", 2, 1500, "Yıldız" }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "EmployeeId", "OrderDate" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 2, 1, 12, 27, 40, 481, DateTimeKind.Local).AddTicks(3510) },
                    { 2, 1, new DateTime(2024, 2, 1, 12, 27, 40, 481, DateTimeKind.Local).AddTicks(3519) },
                    { 3, 2, new DateTime(2024, 2, 1, 12, 27, 40, 481, DateTimeKind.Local).AddTicks(3520) },
                    { 4, 2, new DateTime(2024, 2, 1, 12, 27, 40, 481, DateTimeKind.Local).AddTicks(3521) },
                    { 5, 3, new DateTime(2024, 2, 1, 12, 27, 40, 481, DateTimeKind.Local).AddTicks(3521) },
                    { 6, 3, new DateTime(2024, 2, 1, 12, 27, 40, 481, DateTimeKind.Local).AddTicks(3522) },
                    { 7, 3, new DateTime(2024, 2, 1, 12, 27, 40, 481, DateTimeKind.Local).AddTicks(3523) },
                    { 8, 4, new DateTime(2024, 2, 1, 12, 27, 40, 481, DateTimeKind.Local).AddTicks(3523) },
                    { 9, 4, new DateTime(2024, 2, 1, 12, 27, 40, 481, DateTimeKind.Local).AddTicks(3524) },
                    { 10, 1, new DateTime(2024, 2, 1, 12, 27, 40, 481, DateTimeKind.Local).AddTicks(3524) },
                    { 11, 2, new DateTime(2024, 2, 1, 12, 27, 40, 481, DateTimeKind.Local).AddTicks(3525) }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
