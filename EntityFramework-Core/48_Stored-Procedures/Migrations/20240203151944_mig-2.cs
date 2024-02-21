using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _48_Stored_Procedures.Migrations
{
    /// <inheritdoc />
    public partial class mig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"
            CREATE PROC sp_PersonOrders
            AS 
	            BEGIN 
		            SELECT Name, COUNT(*) [Count]
		            FROM PERSONS P 
		            JOIN ORDERS O ON P.PersonId = O.PersonId
		            GROUP BY Name
		            Order BY [Count] DESC
	            END
            ");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"DROP PROC sp_PersonOrders");

        }
    }
}
