using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;

Console.WriteLine();
ApplicationDbContext context = new();

#region TPH - Table Per Hierarchy Neden İhtiyaç Duyulur?
//İçerisinde benzer alanlara sahip olan entity'leri migrate ettiğimizde her entity'e karşılık bir tablo oluşturmaktansa
//Bu entity'leri tek bir tabloda modellemek isteyebilir ve bu tablodaki kayıtları discriminator kolonu üzerinden birbirinden ayırabiliriz.
//Bu tarz bir tablonun oluşturulması veri ekleme, silme, güncelleme işlemleri için TPH kullanılabilir.
#endregion

#region TPH Nasıl Uygulanır?
//EF Core'da entity arasında temel bir kalıtımsal ilişki söz konusuysa default olarak TPH kullanılır.
//Herhangi bir konfigürasyon gerektirmez.

//Entity'ler kendi aralarında kalıtımsal ilişkiye sahip olmalı ve tüm entityler DbContext içerisine DbSet<T> olarak eklenmelidir.
#endregion

#region Discriminator Kolonu Nedir?
//TPH yaklaşımı neticesinde kümülatif olarak inşa edilmiş tablonun hangi entity'e karşılık veri tuttuğunu ayırt edebilmemizi sağlayan bir kolondur.
//EF Core tarafından otomatik olarak oluşturulur ve varsayılan olarak Discriminator kolonu adını alır.
//Discriminator kolonu Fluent API ile özelleştirilebilir.
#endregion

#region Discriminator Değerleri Nasıl Değiştirilir?
//Hiyerarşinin başındaki entity konfigürasyonlarına gelip, HasDiscriminator fonksiyonu ile özelleştirmede bulunarak 
//HasValue fonksiyonu ile hangi entity için hangi değerin kullanılacağını belirtebiliriz.
#endregion

#region TPH'de Veri Ekleme 
//Davranışların hiç birinde (ekleme, silme, güncelleme) bir değişiklik olmaz. 
//Hangi davranış kullanılıyorsa EF Core modellemeyi ona göre yapar.

/*
Employee e1 = new() { Name = "Employee 1 FN", Surname = "Employee 1 SN", Department = "IT" };
Employee e2 = new() { Name = "Employee 2 FN", Surname = "Employee 2 SN", Department = "HR" };

Customer c1 = new() { Name = "Customer 1 FN", Surname = "Customer 1 SN", CompanyName = "Company 1" };
Customer c2 = new() { Name = "Customer 2 FN", Surname = "Customer 2 SN", CompanyName = "Company 2" };

Technician t1 = new() { Name = "Technician 1 FN", Surname = "Technician 1 SN", Department = "IT", Branch = "Branch 1" };
Technician t2 = new() { Name = "Technician 2 FN", Surname = "Technician 2 SN", Department = "HR", Branch = "Branch 2" };

await context.AddRangeAsync(e1, e2, c1, c2, t1, t2);
await context.SaveChangesAsync();

/*
Id	Name			Surname			Discriminator	CompanyName	Department	Branch
1	Customer 1 FN	Customer 1 SN	Customer		Company	1	NULL		NULL
2	Customer 2 FN	Customer 2 SN	Customer		Company 2	NULL		NULL
3	Employee 1 FN	Employee 1 SN	Employee		NULL		IT			NULL
4	Employee 2 FN	Employee 2 SN	Employee		NULL		HR			NULL
5	Technician 1 FN	Technician 1 SN	Technician		NULL		IT			Branch 1
6	Technician 2 FN	Technician 2 SN	Technician		NULL		HR			Branch 2 
*/

#endregion

#region TPH'de Veri Silme
//TPH yaklaşımında veri silme Entity üzerinden yapılır. Hangi entity üzerinden silme işlemi yapılırsa o entity'e karşılık gelen veri silinir.
/*
Employee e1 = await context.Employees.FindAsync(3);
context.Employees.Remove(e1);
await context.SaveChangesAsync();
*/

/*
var customers = await context.Customers.ToListAsync();
context.Customers.RemoveRange(customers);
await context.SaveChangesAsync();
*/
#endregion

#region TPH'de Veri Güncelleme
/*
var employee = await context.Employees.FirstOrDefaultAsync(e => e.Department == "HR");
employee.Department = "IT";
await context.SaveChangesAsync();
*/
#endregion

#region TPH'de Veri Sorgulama
//Veri sorgulama operasyonu bilinen DbSet üzerinden yapılır.
//Burada dikkat edilmesi gereken husus:
//Kalıtımsal ilişkiye göre yapılan sorgulamada üst sınıf alt sınıftaki verileri de getirir.

/*
var employees = await context.Employees.ToListAsync(); //Employees tablosundaki tüm kayıtları getirir. Ayrıca Customers ve Technicians tablosundaki kayıtları da getirir.
var techs = await context.Technicians.ToListAsync(); // Sadece Technicians tablosundaki kayıtları getirir. Çünkü Technicians tablosundan türemiş bir entity yoktur.
*/

//Sorgulama süreçlerinde EF Core sorguya bir WHERE koşulu ekler. Bu koşul Discriminator kolonu üzerinden yapılır.
#endregion

#region Farklı Entity'lerde Aynı İsimde Property'lerin Olması
//TPH yaklaşımında farklı entity'lerde aynı isimde property'lerin olması durumunda 
//EF Core tarafından mükerrer kolonlar kalıtımsal sıraya göre özelleştirir.
//A ve Technicians_A şeklinde kalıtımda üst sınıfta olana öncelik olarak A verir.

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
    public int A { get; set; }
    public string? CompanyName { get; set; }
}
class Technician : Employee
{
	public int A { get; set; }
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
		////Discriminator özelleştirme
		//modelBuilder.Entity<Person>()
		//	.HasDiscriminator<int>("Disc")
		//	.HasValue<Person>(1)
		//	.HasValue<Employee>(2)
		//	.HasValue<Customer>(3)
		//	.HasValue<Technician>(4);
	
	}
	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=ApplicationDb;Trusted_Connection=true;");
	}
}