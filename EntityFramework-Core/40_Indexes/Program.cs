using Microsoft.EntityFrameworkCore;

ApplicationDbContext context = new();

#region Index nedir?
//Bir sütuna dayalı sorgulamaları daha verimli ve performanslı hale getirmek için kullanılan yapıdır.
#endregion

#region Index'leme Nasıl Yapılır?
//PK, FK ve AK olan kolonlar otomatik olarak indexlenir.
#region Index Attribute
/*
context.Employees.Where(e=>e.Name=="Ahmet").ToList(); // Name kolonuna index ekledikten sonra bu sorgu daha performanslı çalışır. 
*/

#endregion
#region HasIndex 

#endregion
#endregion

#region Composite Index
//Bir tabloda birden fazla kolona index eklemek için kullanılır. 
context.Employees.Where(e => e.Name == "Ahmet" && e.Surname == "Yılmaz").ToList(); // Name ve Surname kolonlarına index ekledikten sonra bu sorgu daha performanslı çalışır.

#endregion

#region Unique Index
//Bir kolona unique index eklemek için kullanılır.
#endregion

#region Index Sort Order - Sıralama Düzeni (EF Core 7.0)

#region AllDescending - Attribute
//Tüm indexlerde descending davranışının bütünsel olarak konfigüre edilmesini sağlar.
#endregion

#region IsDescending - Attribute

#endregion

#region IsDescending Fluent API Method

#endregion

#endregion

#region Index Filter 
//Fluent API üzerinde HasFilter methodu ile index filtreleme yapılabilir.
#endregion

#region Included Columns




#endregion



//3 farklı sorgulama durumuna göre index'leme yapılabilir. Bu sayede her ihtiyaca göre index'leme yapılabilir.
/*
[Index(nameof(Employee.Name))] // => context.Employees.Where(e=>e.Name=="Ahmet").ToList(); 
[Index(nameof(Employee.Surname))] // => context.Employees.Where(e=>e.Surname=="Yılmaz").ToList();
[Index(nameof(Employee.Name), nameof(Employee.Surname))] // => context.Employees.Where(e=>e.Name=="Ahmet" && e.Surname=="Yılmaz").ToList();

[Index(nameof(Employee.Email), IsUnique = true)] // Email kolonuna unique index ekledik. 
*/

//EF Core 7.0 ile gelen özellikler
//[Index(nameof(Employee.Name), AllDescending = true)] // Name ve Surname kolonlarına unique index ekledik.
//[Index(nameof(Employee.Name), nameof(Employee.Surname), IsDescending = new[] { true,false})] => Name kolonu descending, Surname kolonu ascending olarak indexlendi.
class Employee
{
	public int Id { get; set; }

    public string Email { get; set; }
    public string? Name { get; set; }
	public string? Surname { get; set; }
	public int Salary { get; set; }
}

class ApplicationDbContext : DbContext
{
	public DbSet<Employee> Employees { get; set; }
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		//HasIndex
		modelBuilder.Entity<Employee>()
			.HasIndex(e => new { e.Name, e.Surname }); // Name ve Surname kolonlarına index ekledik.
													   // Class üzerindeki Index attribute'u ile aynı işi yapar.
	       //.HasIndex(nameof(Employee.Name), nameof(Employee.Surname)); 

		// IsDescending Fluent API Method - EF Core 7.0
		/*
		modelBuilder.Entity<Employee>()
			.HasIndex(e => new { e.Name, e.Surname })
			.IsDescending(); // Name ve Surname kolonlarına descending index ekledik.

		modelBuilder.Entity<Employee>()
			.HasIndex(e => new { e.Name, e.Surname })
			.IsDescending(new[] { true, false }); // Name kolonu descending, Surname kolonu ascending olarak indexlendi.
		*/

		// Index Filter
		/*
		modelBuilder.Entity<Employee>()
			.HasIndex(e => e.Name)
			.HasFilter("[NAME] IS NOT NULL");
		*/

		// Included Columns
		modelBuilder.Entity<Employee>()
			.HasIndex(e => e.Name)
			.IncludeProperties(e => new { e.Surname, e.Salary }); // Bu sayede Name kolonuna index eklerken
																  // Surname ve Salary kolonlarını da index'e dahil ettik.
		// => context.Employees.Where(e=>e.Name=="Ahmet").Select(e=>new {e.Surname,e.Salary}).ToList();
	}
	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=IndexDb;Trusted_Connection=True;");
	}
}