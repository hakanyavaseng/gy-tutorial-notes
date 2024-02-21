using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

Console.WriteLine();
ApplicationDbContext context = new();

#region Generated Value nedir?
//EF Core'da üretilen değerlerle ilgili çeşitli modellerin ayrıntılarını sağlayan bir konfigürasyondur.
#endregion

#region Default Values
//Eğer ki bir kolona değer girilmezse, o kolonun varsayılan değerini belirlemek için kullanılır.

#region HasDefaultValue
//Static veri verilir.
/*
Person p = new()
{
	Name = "Hakan",
	Surname = "Yavaş",
	PersonCode = 1,
	Premium = 100,
	TotalGain = 1000
};

await context.Persons.AddAsync(p);
await context.SaveChangesAsync();
*/


#endregion

#region HasDefaultValueSql
//SQL cümleciği verilir.
/*
Person p = new()
{
	Name = "Alperen",
	Surname = "Güneş",
	PersonCode = 1,
	Premium = 100,
	TotalGain = 1000
};

await context.Persons.AddAsync(p);
await context.SaveChangesAsync();
*/

#endregion
#endregion

#region Computed Columns
//Tablo içerisindeki kolonlar üzerinde yapılan aritmetik işlemler sonucunda elde edilen değerlerin saklanması neticesinde oluşan kolonlardır.
#endregion


#region Value Generation

#region Primary Keys 
//Tablodaki satırları unique olarak tanımlayan kolonlardır.
#endregion

#region Identity
//Identity yalnızca otomatik artan değerler için kullanılır. Bir sütun PK olmaksızın da Identity olarak tanımlanabilir. 
//Bir tablo içerisinde yalnız bir Identity kolonu olabilir.
#endregion

//Bu iki özellik, genellikle birlikte kullanılır. EF Core PK olan bir kolonu otomatik olarak Identity olarak yapılandırır.
//Ancak böyle olması için bir gereklilik yoktur. Bir tablo i

#region DatabaseGenerated
/*
Person p = new()
{
	PersonId = 956, // Burada her eklemede unique bir değer vermek zorundayız.
	Name = "Hakan",
	Surname = "Yavaş",
	Premium = 100,
	TotalGain = 1000
};

await context.Persons.AddAsync(p);
await context.SaveChangesAsync();
*/

#region DatabaseGeneratedOption.None - ValueGeneratedNever
//Bir kolonda değer üretilmeyecekse none ile belirtilir.
//EF Core, default olarak PK kolonlarda getirdiği Identity özelliğini kaldırmak için kullanılır.
//Fluent API kısmında kullanmak için de ValueGeneratedNever kullanılır.
#endregion

#region DatabaseGeneratedOption.Identity - ValueGeneratedOnAdd
//Herhangi bir kolona ardışık değerler vermek için kullanılır.
//Eğer ki Identity özelliği sayısal olan bir kolonda kullanılacaksa PK olan kolonda Identity özelliğinin None olması gerekir.
//Eğer ki Identity özelliği sayısal olmayan bir kolonda kullanılacaksa PK olan kolonda Identity özelliğinin kaldırılmasına gerek yoktur.
#endregion

#region DatabaseGeneratedOption.Computed - ValueGeneratedOnAddOrUpdate
//EF Core üzerinde bir kolon ComputerColumn ise, istersek belirlenebilir, istersek de belirlemeden kullanabiliriz.
#endregion
#endregion

#endregion


class Person
{
	//[DatabaseGenerated(DatabaseGeneratedOption.None)] // Primary Key olmasına rağmen Identity olmasın.
													  // Fakat burada PK değerinin elle verilmesi gerekmektedir. 
	public int PersonId { get; set; }
	public string Name { get; set; }
	public string Surname { get; set; }
	public int Premium { get; set; }
	public int Salary { get; set; }
	public int TotalGain { get; set; }

	[DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Primary Key olmasa da Identity olsun.
	public int PersonCode { get; set; }
	//public Guid PersonCode { get; set; } 
}

class ApplicationDbContext : DbContext
{
	public DbSet<Person> Persons { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;database=ApplicationDbGV;integrated security=true;");
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		//Default Values
		//modelBuilder.Entity<Person>()
		//	.Property(p => p.Salary)
		//	.HasDefaultValue(1000);

		//Default Values SQL
		modelBuilder.Entity<Person>()
			.Property(p => p.Salary)
			.HasDefaultValueSql("FLOOR(RAND() * 1000)");

		//Computed Columns
		modelBuilder.Entity<Person>()
			.Property(p => p.TotalGain)
			.HasComputedColumnSql("([Premium] + [Salary]) * 10");

		//None - ValueGeneratedNever
		modelBuilder.Entity<Person>()
			.Property(p => p.PersonId)
			.ValueGeneratedNever();

		//modelBuilder.Entity<Person>()
		//	.Property(p => p.PersonCode)
		//	.HasDefaultValueSql("NEW_ID()");
		
		
	}

}