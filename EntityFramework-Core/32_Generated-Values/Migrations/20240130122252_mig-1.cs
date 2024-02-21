using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _32_Generated_Values.Migrations
{
    public partial class mig1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Premium = table.Column<int>(type: "int", nullable: false),
                    Salary = table.Column<int>(type: "int", nullable: false, defaultValueSql: "FLOOR(RAND() * 1000)"),
                    TotalGain = table.Column<int>(type: "int", nullable: false, computedColumnSql: "([Premium] + [Salary]) * 10"),
                    PersonCode = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEW_ID()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.PersonId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Persons");
        }
    }
}
