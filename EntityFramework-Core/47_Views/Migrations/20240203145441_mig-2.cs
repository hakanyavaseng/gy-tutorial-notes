using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _47_Views.Migrations
{
    /// <inheritdoc />
    public partial class mig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"
            CREATE VIEW vm_PersonOrders1
            AS
	            SELECT Name, COUNT(*) [Count]
	            FROM PERSONS P 
	            JOIN ORDERS O ON P.PersonId = O.PersonId
	            GROUP BY Name
             ");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"DROP VIEW vm_PersonOrders1");

        }
    }
}
