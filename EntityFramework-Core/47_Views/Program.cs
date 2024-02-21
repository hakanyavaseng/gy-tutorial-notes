using System.Collections.Generic;
using System.Reflection.Emit;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

ApplicationDbContext context = new();

#region EF Core ile View Kullanımı 

#region View Oluşturma
/*
1. Adım: Boş bir migration oluşturulmalıdır.
2. Adım: Migration içindeki Up fonksiyonunda View'ın create komutları, Down fonksiyonunda ise drop komutları yazılmalıdır.
3. Adım: Migrate!
*/
#endregion

#region View'i DbSet olarak oluşturma
//View'den gelecek kolonlara özel bir Entity oluşturulması gerekmektedir. (Name ve Count)
//Fluent API içerisinden de hangi view'a karşılık geldiği bildirilmelidir.
#endregion

var personOrders = await context.PersonOrders
	.ToListAsync();

Console.WriteLine();

#region Bilgiler
//View'lerde primary key olmaz, bu yüzden HasNoKey ile işaretlenmelidir.
//Gelen veriler Change Tracker ile takip edilmezler.
#endregion

#endregion


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
			.ToView("vm_PersonOrders")
			.HasNoKey();
	}
	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseSqlServer(@"Server =.\SQLEXPRESS;Database =SqlQueriesDb;Trusted_Connection = True;TrustServerCertificate=True;");
	}
}