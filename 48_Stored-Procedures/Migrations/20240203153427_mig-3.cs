using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _48_Stored_Procedures.Migrations
{
    /// <inheritdoc />
    public partial class mig3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"
            CREATE PROC sp_BestSellingEmployee
            AS  
	            DECLARE @NAME AS NVARCHAR(MAX), @COUNT INT
	            BEGIN 
		            SELECT TOP 1 @NAME = Name, @COUNT = COUNT(*)
		            FROM PERSONS P 
		            JOIN ORDERS O ON P.PersonId = O.PersonId
		            GROUP BY Name
		            Order BY COUNT(*) DESC
		            RETURN @COUNT
	            END
               ");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROC sp_BestSellingEmployee");

        }
    }
}
