using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Emit;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

ApplicationDbContext context = new();

#region EF Core ile Stored Procedure Kullanımı
/*
 * 1. Adım => Boş migration oluşturulur.
 * 2. Adım => Up ve Down fonksiyonlarını SP'ye göre doldur.
 * 3. Adım => Migrate!
*/

//var personOrders = await context.PersonOrders.FromSql($"EXEC sp_PersonOrders")
//	.ToListAsync();

#region Geriye Değer Döndüren Stored Procedure Kullanımı 
//sp_BestSellingEmployee

SqlParameter countParameter = new() { 
	Direction = System.Data.ParameterDirection.Output,
	ParameterName = "Count", 
	SqlDbType = System.Data.SqlDbType.Int};
var count = await context.Database.ExecuteSqlRawAsync($"EXECUTE @Count = sp_BestSellingEmployee", countParameter);

#endregion

#region Parametreli Stored Procedure Kullanımı 
SqlParameter nameParameter = new()
{
	ParameterName = "name",
	SqlDbType = System.Data.SqlDbType.NVarChar,
	Direction = System.Data.ParameterDirection.Output,
	Size = 1000
};
context.Database.ExecuteSqlRawAsync($"EXEC sp_PersonOrders2 5, @name OUTPUT");
#endregion

#endregion


Console.WriteLine();



public class Person
{
	public int PersonId { get; set; }
	public string Name { get; set; }

	public ICollection<Order> Orders { get; set; }
}
public class Order
{
	public int OrderId { get; set; }
	public int PersonId { get; set; }
	public string Description { get; set; }

	public Person Person { get; set; }
}

[NotMapped]
public class PersonOrder
{
	public string Name { get; set; }
	public int Count { get; set; }
}
class ApplicationDbContext : DbContext
{
	public DbSet<Person> Persons { get; set; }
	public DbSet<Order> Orders { get; set; }
	public DbSet<PersonOrder> PersonOrders { get; set; }
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

		modelBuilder.Entity<PersonOrder>()
			.HasNoKey();

		modelBuilder.Entity<Person>()
			.HasMany(p => p.Orders)
			.WithOne(o => o.Person)
			.HasForeignKey(o => o.PersonId);
	}
	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseSqlServer(@"Server =.\SQLEXPRESS;Database =StoredProceduresQueriesDb;Trusted_Connection = True;TrustServerCertificate=True;");
	}
}