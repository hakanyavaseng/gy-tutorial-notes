using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

ApplicationDbContext context = new();

#region TPT - Table Per Type Nedir?
//Entity'lerin aralarında kalıtımsal ilişkiye sahip olduğu durumlarda her bir türe/entity'e/tip/referans karşılık bir 
//tablo oluşturulur. 
//Generate edilen her tablo hiyerarşik düzlemde kendi aralarında birebir ilişkiye sahiptir.
#endregion

#region TPT Nasıl Uygulanır?
//TPT Uygulamak için öncelikle entitylerin kendi aralarında olması gereken mantıkta inşa edilmeleri gerekmektedir.
//Tüm entityler DbContext içine DbSet olarak eklenir.
//Hiyerarşik olarak aralarında kalıtımsal ilişki olan tüm entityler OnModelCreating içerisinde ToTable metodu ile konfigüre edilir.
//Böylece EF Core kalıtımsal ilişki olan tablolar arasında TPT uygulamasını gerçekleştirir.
#endregion

#region Veri Ekleme, Silme, Güncelleme, Sorgulama
//Tüm işlemler EF Core'da aynı şekilde gerçekleştirilir.
//Mimariye göre ekleme, silme, güncelleme ve sorgulama işlemleri değişmez.

#region Veri Ekleme
/*
Technician technician = new()
{
	Name = "Hakan",
	Surname = "Yavaş",
	Department = "IT",
	Branch = "Eskişehir"
};

await context.Technicians.AddAsync(technician);
await context.SaveChangesAsync();

Customer customer = new()
{
	Name = "Mehmet",
	Surname = "Yılmaz",
	CompanyName = "Yılmazlar A.Ş"
};

await context.Customers.AddAsync(customer);
await context.SaveChangesAsync();
*/
#endregion

#region Veri Silme
/*
Employee? deletedEmployee = await context.Employees.FindAsync(1);
if (deletedEmployee is not null)
{
	context.Employees.Remove(deletedEmployee);
	await context.SaveChangesAsync();
}

Person? deletedPerson = await context.Persons.FindAsync(2);
if (deletedPerson is not null)
{
	context.Persons.Remove(deletedPerson);
	await context.SaveChangesAsync();
}
*/

#endregion

#region Veri Güncelleme
/*
Technician technician = await context.Technicians.FindAsync(1);
if (technician is not null)
{
	technician.Name = "Hakan1";
	technician.Surname = "Yavaş1";
	technician.Department = "IT1";
	technician.Branch = "Eskişehir1";
	await context.SaveChangesAsync();
}
*/


#endregion

#endregion

abstract class Person
{
	public int Id { get; set; }
	public string? Name { get; set; }
	public string? Surname { get; set; }
}
class Employee : Person
{
	public string? Department { get; set; }
}
class Customer : Person
{
	public string? CompanyName { get; set; }
}
class Technician : Employee
{
	public string? Branch { get; set; }
}

class ApplicationDbContext : DbContext
{
	public DbSet<Person> Persons { get; set; }
	public DbSet<Employee> Employees { get; set; }
	public DbSet<Customer> Customers { get; set; }
	public DbSet<Technician> Technicians { get; set; }
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		//TPT uygulaması için her bir entity için ayrı ayrı tablo oluşturulması gerekiyor.
		modelBuilder.Entity<Person>().ToTable("Persons");
		modelBuilder.Entity<Employee>().ToTable("Employees");
		modelBuilder.Entity<Customer>().ToTable("Customers");
		modelBuilder.Entity<Technician>().ToTable("Technicians");
	}
	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=ApplicationDb;Trusted_Connection=true;");
	}
}